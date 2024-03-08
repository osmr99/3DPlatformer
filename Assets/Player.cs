using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 10, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float jumpMovement = Input.GetAxis("Jump") * speed;
        Vector3 movementAmountV = Vector3.up * jumpMovement;
        transform.position += movementAmountV * Time.deltaTime;

        float xMovement = Input.GetAxis("Horizontal") * speed;
        Vector3 xAmountH = Vector3.left * xMovement;
        transform.position += xAmountH * Time.deltaTime;

        float zMovement = Input.GetAxis("Vertical") * speed;
        Vector3 zAmount = Vector3.back * zMovement;
        transform.position += zAmount * Time.deltaTime;
    }
}
