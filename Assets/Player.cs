using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] AudioSource SoundEffectController;
    [SerializeField] AudioClip coinAudioEffect;
    [SerializeField] AudioClip hurtAudioEffect;
    [SerializeField] PlayerStats playerStatsFile;
    [SerializeField] GameObject player;
    [SerializeField] Animator anim;

    //float moveSpeed = 10;
    float moveSpeed = 3;
    //float jumpPower = 7.5f;
    float jumpPower = 5;
    bool onGround = false;
    float groundCheckDistance = 0.5f;
    float forwardMovementInput;
    float rightMovementInput;
    Rigidbody rb;
    Animator animObject;
    

    Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 1, 0);
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        animObject = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

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
           moveSpeed = 3;


        if (transform.position.y >= 0.45f && onGround) // This controls the falling animation
            anim.SetBool("onGround", true);
        else
            anim.SetBool("onGround", false);


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


        if (playerStatsFile.currentHealth <= 0) // Game Over Scene WIP
        {
            SoundEffectController.volume = 0.1f;
            SoundEffectController.PlayOneShot(hurtAudioEffect);
            GameObject.Destroy(player);
        }
        if(transform.position.y < -100) // The player fell out of the world
        {
            SoundEffectController.volume = 0.1f;
            SoundEffectController.PlayOneShot(hurtAudioEffect);
            GameObject.Destroy(player);
        }


        anim.SetFloat("speed", movementVector.magnitude);

        movementVector.y = 0;                    // This makes the player face the
        anim.transform.forward = movementVector; // direction they are walking.
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
        }
        if (other.TryGetComponent(out Hazard Hazard)) // Taking damage from hazards method
        {
            SoundEffectController.volume = 0.1f;
            //SoundEffectController.volume = 2f;
            SoundEffectController.PlayOneShot(hurtAudioEffect);
            playerStatsFile.takingDamage();
            anim.SetTrigger("hurt"); // Will run the hurt animation


        }
    }
}
