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
    float moveSpeed = 3.5f;
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
        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            anim.SetTrigger("jump");
        }
        if (Input.GetKey(KeyCode.LeftShift) && (((playerStatsFile.healthRegen >= 0.1f && playerStatsFile.currentHealth != playerStatsFile.maxHealth)) || playerStatsFile.currentHealth == playerStatsFile.maxHealth))
        {
            moveSpeed = 6.5f;
            if (onGround)
                anim.SetTrigger("isSprinting");
        }
        else if (playerStatsFile.healthRegen < 0.1f && playerStatsFile.currentHealth != playerStatsFile.maxHealth)
            moveSpeed = 1;
        else
           moveSpeed = 3.5f;


        if (transform.position.y >= 0.45f && onGround)
            anim.SetBool("onGround", true);
        else
            anim.SetBool("onGround", false);


        rightMovementInput = Input.GetAxis("Horizontal2") * moveSpeed;
        //Vector3 zAmount = Vector3.back * zMovement;
        //transform.position += zAmount * Time.deltaTime;

        forwardMovementInput = Input.GetAxis("Vertical2") * moveSpeed;
        //Vector3 xAmountH = Vector3.left * xMovement;
        //transform.position += xAmountH * Time.deltaTime;

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camForward.Normalize();

        camRight.y = 0;
        camRight.Normalize();

        Vector3 forwardRelative = forwardMovementInput * camForward;
        Vector3 rightRelative = rightMovementInput * camRight;

        Vector3 movementVector = (forwardRelative + rightRelative).normalized * moveSpeed;
        //Vector3 movementVector = (forwardRelative + rightRelative) * moveSpeed;

        movementVector.y = rb.velocity.y;

        rb.velocity = movementVector;

        //rb.AddForce(movementVector * moveSpeed, ForceMode.Force);

        if (playerStatsFile.currentHealth <= 0)
        {
            SoundEffectController.volume = 0.1f;
            SoundEffectController.PlayOneShot(hurtAudioEffect);
            GameObject.Destroy(player);
        }
        if(transform.position.y < -200)
        {
            SoundEffectController.volume = 0.1f;
            SoundEffectController.PlayOneShot(hurtAudioEffect);
            GameObject.Destroy(player);
        }


        anim.SetFloat("speed", movementVector.magnitude);

        movementVector.y = 0;
        anim.transform.forward = movementVector; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin Coin))
        {
            GameObject.Destroy(other.gameObject);
            SoundEffectController.volume = 0.25f;
            //SoundEffectController.volume = 1f;
            SoundEffectController.PlayOneShot(coinAudioEffect);
            playerStatsFile.pickingCoin();
        }
        if (other.TryGetComponent(out Hazard Hazard))
        {
            SoundEffectController.volume = 0.1f;
            //SoundEffectController.volume = 2f;
            SoundEffectController.PlayOneShot(hurtAudioEffect);
            playerStatsFile.takingDamage();
            anim.SetTrigger("hurt");


        }
    }
}
