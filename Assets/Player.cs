using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float moveSpeed = 10;
    float jumpPower = 7.5f;
    bool onGround = false;
    float groundCheckDistance = 0.5f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 10, 0);
        rb = GetComponent<Rigidbody>();
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
        


        float xMovement = Input.GetAxis("Horizontal") * moveSpeed;
        Vector3 xAmountH = Vector3.left * xMovement;
        transform.position += xAmountH * Time.deltaTime;

        float zMovement = Input.GetAxis("Vertical") * moveSpeed;
        Vector3 zAmount = Vector3.back * zMovement;
        transform.position += zAmount * Time.deltaTime;
    }
}
