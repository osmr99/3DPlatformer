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
    float moveSpeed = 10;
    //float jumpPower = 7.5f;
    float jumpPower = 5;
    bool onGround = false;
    float groundCheckDistance = 0.5f;
    float forwardMovementInput;
    float rightMovementInput;
    Rigidbody rb;

    Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 5, 0);
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {

        onGround = (Physics.Raycast(transform.position, Vector3.up * -1, groundCheckDistance));
        Debug.DrawLine(transform.position, transform.position + transform.up * -groundCheckDistance, Color.red);
        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }

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

        movementVector.y = rb.velocity.y;

        rb.velocity = movementVector;

        if (playerStatsFile.currentHealth <= 0)
            GameObject.Destroy(player);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin Coin))
        {
            GameObject.Destroy(other.gameObject);
            //SoundEffectController.volume = 0.25f;
            SoundEffectController.volume = 1f;
            SoundEffectController.PlayOneShot(coinAudioEffect);
            playerStatsFile.pickingCoin();
        }
        if (other.TryGetComponent(out Hazard Hazard))
        {
            //SoundEffectController.volume = 0.1f;
            SoundEffectController.volume = 2f;
            SoundEffectController.PlayOneShot(hurtAudioEffect);
            playerStatsFile.takingDamage();

        }
    }
}
