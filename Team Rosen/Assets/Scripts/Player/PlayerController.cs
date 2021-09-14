using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRB;

    Vector3 forwardForce = new Vector3(0, 0, 20f);
    Vector3 backForce = new Vector3(0, 0, -20f);

    Vector3 leftForce = new Vector3(-20f, 0, 0);
    Vector3 rightForce = new Vector3(20f, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        //Get player rigidbody
        playerRB = GetComponent<Rigidbody>();
    }

    void FixedUpdate() 
    {

        //Forward/Backward
        if (Input.GetKey("w")) 
        {
            playerRB.AddForce(forwardForce);
        }
        if (Input.GetKey("s"))
        {
            playerRB.AddForce(backForce);
        }


        //Left/Right strafe

        if (Input.GetKey("a"))
        {
            playerRB.AddForce(leftForce);
        }
        if (Input.GetKey("d"))
        {
            playerRB.AddForce(rightForce);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
