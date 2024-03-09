using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] AudioSource SoundEffectController;
    [SerializeField] AudioClip coinAudioEffect;
    [SerializeField] AudioClip hurtAudioEffect;
    [SerializeField] PlayerStats playerStatsFile;
    [SerializeField] GameObject player;
    [SerializeField] Animator anim;
    [SerializeField] CinemachineVirtualCamera playerCam;
    [SerializeField] CinemachineVirtualCamera mapCam;
    [SerializeField] Transform cam;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator animObject;
    [SerializeField] gameOverScript gameOverFile;

    //float moveSpeed = 10;
    float moveSpeed = 3;
    //float jumpPower = 7.5f;
    float jumpPower = 5;
    bool onGround = false;
    float groundCheckDistance = 0.5f;
    float forwardMovementInput;
    float rightMovementInput;
    int loadStuff = 0;
    public int coinsPickedUp = 0;

    Scene currentScene;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentScene.buildIndex == 1)
        {
            if(loadStuff == 0) // This will only get called once, basically replace the Start() method
            {
                transform.position = new Vector3(0, 1, 0);
                playerCam.Priority = 1;
                mapCam.Priority = 0;
                loadStuff = 1;
            }
            onGround = (Physics.Raycast(transform.position, Vector3.up * -1, groundCheckDistance));
            Debug.DrawLine(transform.position, transform.position + transform.up * -groundCheckDistance, Color.red);
            if (Input.GetButtonDown("Jump") && onGround) // Jumping method
            {
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                anim.SetTrigger("jump"); // Will run the jumping animation
            }
            if (Input.GetKey(KeyCode.LeftShift) && (((playerStatsFile.healthRegen >= 0.1f && playerStatsFile.currentHealth != playerStatsFile.maxHealth)) || playerStatsFile.currentHealth == playerStatsFile.maxHealth))
            { // Player will start running. If hurt, running it's disabled for half a second
                moveSpeed = 6.5f;
                if (onGround && (Input.GetButton("Horizontal2") || Input.GetButton("Vertical2")))
                    anim.SetTrigger("isSprinting"); // This will do the running animation if moving on ground
                                                    // otherwise, it will just increase the move speed 
            }
            else if (playerStatsFile.healthRegen < 0.1f && playerStatsFile.currentHealth != playerStatsFile.maxHealth)
                moveSpeed = 1; // When the player gets hurt, their speed is penalized for half a second
            else
            {
                moveSpeed = 3;
                jumpPower = 5;
            }



            if (transform.position.y >= 0.45f && onGround) // This controls the falling animation
                anim.SetBool("onGround", true);
            else
                anim.SetBool("onGround", false);

            if (Input.GetKey(KeyCode.M) && onGround) // Controls the map
            {
                playerCam.Priority = 0;
                mapCam.Priority = 1;
                moveSpeed = 0;
                jumpPower = 0;
            }
            else
            {
                playerCam.Priority = 1;
                mapCam.Priority = 0;
            }



            rightMovementInput = Input.GetAxis("Horizontal2") * moveSpeed; // I created my custom Axis here.

            forwardMovementInput = Input.GetAxis("Vertical2") * moveSpeed; // I created my custom Axis here.

            // This below controls the player movement and wherever the cameras faces,
            // the movement input will be correct.

            Vector3 camForward = cam.forward;
            Vector3 camRight = cam.right;

            camForward.y = 0;
            camForward.Normalize();

            camRight.y = 0;
            camRight.Normalize();

            Vector3 forwardRelative = forwardMovementInput * camForward;
            Vector3 rightRelative = rightMovementInput * camRight;

            Vector3 movementVector = (forwardRelative + rightRelative).normalized * moveSpeed;

            movementVector.y = rb.velocity.y;

            rb.velocity = movementVector;

            anim.SetFloat("speed", movementVector.magnitude);

            movementVector.y = 0;                    // This makes the player face the
            anim.transform.forward = movementVector; // direction they are walking.

            if (playerStatsFile.currentHealth <= 0) // Game Over Scene WIP
            {
                anim.SetTrigger("gameOver");
                playerStatsFile.gameOver();
                waitTheGameOver();
                enabled = false;
            }
            if (transform.position.y < -50)     // The player fell out of the world
            {
                playerStatsFile.fellOutOfWorld();
                gameObject.transform.localScale = Vector3.zero;
                waitTheGameOverTwo();           
                enabled = false;                // Disables the Update() method here, which controls the player movement and animations
            }

            if(coinsPickedUp == 100)
            {
                playerStatsFile.win();
                waitTheWin();
                enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin Coin)) // Coin collecting method
        {
            GameObject.Destroy(other.gameObject);
            SoundEffectController.volume = 0.25f;
            //SoundEffectController.volume = 1f;
            SoundEffectController.PlayOneShot(coinAudioEffect);
            playerStatsFile.pickingCoin();
            coinsPickedUp++;
        }
        if (other.TryGetComponent(out Hazard Hazard)) // Taking damage from hazards method
        {
            playerStatsFile.takingDamage();
            anim.SetTrigger("hurt"); // Will run the hurt animation
            if (playerStatsFile.currentHealth > 0)
            {
                SoundEffectController.volume = 0.1f;
                //SoundEffectController.volume = 2f;
                SoundEffectController.PlayOneShot(hurtAudioEffect);
            }
        }
    }

    void waitTheGameOver()
    {
        StartCoroutine(theGameOver());
    }

    private IEnumerator theGameOver()
    {
        float countDown = 8f;
        while (countDown >= 0)
        {
            countDown -= Time.deltaTime;
            //Debug.Log(countDown);
            yield return null;
        }
        SceneManager.LoadScene(2);
    }

    void waitTheGameOverTwo()
    {
        StartCoroutine(theGameOverTwo());
    }

    private IEnumerator theGameOverTwo()
    {
        float countDown = 6.5f;
        while (countDown >= 0)
        {
            countDown -= Time.deltaTime;
            //Debug.Log(countDown);
            yield return null;
        }
        SceneManager.LoadScene(2);
    }

    void waitTheWin()
    {
        StartCoroutine(theWin());
    }

    private IEnumerator theWin()
    {
        float countDown = 5f;
        while (countDown >= 0)
        {
            countDown -= Time.deltaTime;
            //Debug.Log(countDown);
            yield return null;
        }
        SceneManager.LoadScene(3);
    }
}
