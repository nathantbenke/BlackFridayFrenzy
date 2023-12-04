using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; // F-B
    public float strafeSpeed; // L-R
    public float jumpForce;
    private float sprint = 1.5f;


    public Rigidbody hips;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        hips = GetComponent<Rigidbody>();   
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                hips.AddForce(hips.transform.forward * speed * sprint);
            } else
            {
                hips.AddForce(hips.transform.forward * speed);
            }
        } 

        if (Input.GetKey(KeyCode.S))
        {
            hips.AddForce(hips.transform.forward * -speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            hips.AddForce(hips.transform.right * -strafeSpeed);
        }

        if (Input.GetKey (KeyCode.D)) 
        {
            hips.AddForce(hips.transform.right * strafeSpeed);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if(isGrounded)
            {
                hips.AddForce(new Vector3(0, 3*jumpForce, 0)); //To dash forward add to Z
                isGrounded = false;
            }
        }
    }
}
