using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimColllision : MonoBehaviour
{

    public PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindAnyObjectByType<PlayerController>().GetComponent<PlayerController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        controller.isGrounded = true;
    }
}
