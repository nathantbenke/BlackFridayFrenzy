using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grab : MonoBehaviour
{

    public Animator animator;

    GameObject grabbedObj;
    public Rigidbody rb;
    public int isLeftorRight;
    public bool isGrabbing = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(isLeftorRight))
        {
            if (isLeftorRight == 0)
            {
                // Play animation
                //animator.SetBool()
               // animator.SetBool("isLeftHandUp", true);
            }
            else if (isLeftorRight == 1)
            {
                //animator.SetBool("isRightHandUp", true);
                //animator.SetBool("isRightHandUp", true);
            }



            if (grabbedObj != null)
            {
                FixedJoint fj = grabbedObj.AddComponent<FixedJoint>();
                fj.connectedBody = rb;
                fj.breakForce = 9000;
            }

        }
        else if (Input.GetMouseButtonUp(isLeftorRight))
        {
            if (isLeftorRight == 0)
            {
                // Play animation
                //animator.SetBool()
               //  animator.SetBool("isLeftHandUp", false);

            }
            else if (isLeftorRight == 1)
            {
                //animator.SetBool("isRightHandUp, false);
               // animator.SetBool("isRightHandUp", false);

            }

            if (grabbedObj != null)
            {
                //If it isn
                Destroy(grabbedObj.GetComponent<FixedJoint>());
            }
            grabbedObj = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ShoppingItem"))
        {
            grabbedObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        grabbedObj = null;
    }
}
