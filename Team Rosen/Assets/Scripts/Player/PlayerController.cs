using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRB;

    

    float moveSpeed = 20f;
    float strafeSpeed = 5f;

    Vector3 upVec = new Vector3(0, 1, 0);

    float rotationX = 0;


    //Camera sensitivity
    float sens = 3f;

    Vector3 lastMouse;

    // Start is called before the first frame update
    void Start()
    {
        //Get player rigidbody
        playerRB = GetComponent<Rigidbody>();

        lastMouse = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0) - lastMouse;


        //Hide and lock cursor
        Cursor.lockState = CursorLockMode.Locked;


    }

    void FixedUpdate() 
    {

        ///Keyboard movements
        ///
            //Forward/Backward
            if (Input.GetKey("w")) 
            {
                playerRB.AddForce(playerRB.transform.forward * moveSpeed);
            }
            if (Input.GetKey("s"))
            {
                playerRB.AddForce(playerRB.transform.forward * -moveSpeed);
            }


            //Left/Right strafe

            if (Input.GetKey("a"))
            {
                //Get right vector
                playerRB.AddForce(Vector3.Cross(playerRB.transform.forward, upVec.normalized)* moveSpeed);
            }
            if (Input.GetKey("d"))
            {
                playerRB.AddForce(-(Vector3.Cross(playerRB.transform.forward, upVec.normalized) * moveSpeed));
        }

        ///Mouse movements
        ///



        float horizontal = sens * Input.GetAxis("Mouse X");
        float vertical = sens * Input.GetAxis("Mouse Y");

        rotationX += Input.GetAxis("Mouse Y") * sens;

        playerRB.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        //playerRB.transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * sens, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
