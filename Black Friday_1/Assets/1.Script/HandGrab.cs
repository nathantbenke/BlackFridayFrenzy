using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrab : MonoBehaviour
{

    public Animator animator;

    GameObject grabbedObj;
    public Rigidbody rb;
    public int isLeftorRight;
    public bool grabAttempt= false;
    public bool isGrabbing = false;
    private string identifiedObject;

    FixedJoint fj;
    // public CapsuleCollider handCollider;

    public GenerateLevelAndShopList updateShoppingList;

    // Start is called before the first frame update
    void Start()
    {
       // rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
           // grabbedObj = null;
            grabAttempt = true;

        } else if (Input.GetKeyUp(KeyCode.G))
        {
            grabAttempt = false;

            //Destroy(fj);
            //resetGrabbed();

        }

        if (grabbedObj != null && Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Registered");
            fj = grabbedObj.AddComponent<FixedJoint>();
            fj.connectedBody = rb;
            fj.breakForce = 9000;
            isGrabbing = true;

            if (grabbedObj.CompareTag("ShoppingItem"))
            {
                identifiedObject = grabbedObj.GetComponent<Item>().itemName;
                if (identifiedObject != null)
                {
                    //updateShoppingList.gameObject.Find();
                }

            }

        } else if (grabbedObj != null && Input.GetKeyUp(KeyCode.G))
        {
            //If it isn
            Destroy(grabbedObj.GetComponent<FixedJoint>());
            isGrabbing = false;
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            grabbedObj = null;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ShoppingItem"))
        {
            grabbedObj = other.gameObject;
            grabbedObj.GetComponent<Rigidbody>().mass = 0.1f;
            Debug.Log("CONTACT MADE!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //grabbedObj = null;
    }

    void resetGrabbed()
    {
        if (isGrabbing)
        {
            //
            //Debug.Log("grabbing");

        }
        else
        {
           // Debug.Log("Null");
            grabbedObj = null;
        }
    }
}
