using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Player's rigidbody
    Rigidbody playerRB;

    public static GameObject playerModel;

    //Speed player moves at
    float moveSpeed = 20f;

    //Force applied when jumping
    float jumpSpeed = 400f;

    //Jump cooldown timer
    float jumpCooldown = 1f;

    //Up vector
    Vector3 upVec = new Vector3(0, 1, 0);

    //Overwritten with values
    float rotationX = 0;
    float rotationY = 0;

    //Camera sensitivity
    float sens = 3f;

    //Last mosue position
    Vector3 lastMouse;

    //If player is in the truck
    static public bool isDriving;

    //Cooldown for jump
    bool canJump;

    public Text enterTruckTxt;

    public GameObject truckCam;

    // Start is called before the first frame update
    void Start()
    {
        playerModel = this.gameObject;

        //Get player rigidbody
        playerRB = GetComponent<Rigidbody>();

        //Save last mouse pos
        lastMouse = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0) - lastMouse;


        //Hide and lock cursor
        Cursor.lockState = CursorLockMode.Locked;

        canJump = true;
    }

    void FixedUpdate() 
    {
        if (!isDriving) 
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
                playerRB.AddForce(Vector3.Cross(playerRB.transform.forward, upVec.normalized) * moveSpeed);

            }
            if (Input.GetKey("d"))
            {

                //Get right vector
                playerRB.AddForce(-(Vector3.Cross(playerRB.transform.forward, upVec.normalized) * moveSpeed));
            }

            //Jump
            if (Input.GetKey("space"))
            {
                //If jump off cooldown
                if (canJump)
                {
                    playerRB.AddForce(playerRB.transform.up * jumpSpeed);
                    StartCoroutine(jumpTimer());
                }

            }

            ///Mouse movements
            ///


            //Get mouse movements
            float horizontal = sens * Input.GetAxis("Mouse X");
            float vertical = sens * Input.GetAxis("Mouse Y");


            //Assign and invert Y axis
            rotationX += Input.GetAxis("Mouse Y") * -sens;
            rotationY += Input.GetAxis("Mouse X") * sens;

            //Apply rotation to rigidbody
            playerRB.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        }

        

    }

    IEnumerator jumpTimer() 
    {
        canJump = false;
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {

        //Check is player is looking at the truck
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //If we hit
        if(Physics.Raycast(ray, out hit, 10f)) 
        {
            if(hit.transform.tag == "Truck" && !isDriving) 
            {
                enterTruckTxt.gameObject.SetActive(true);
            }
           
        }
        else
        {
            enterTruckTxt.gameObject.SetActive(false);

        }
        if (enterTruckTxt.gameObject.activeInHierarchy) 
        {
            if (Input.GetKey("e")) 
            {
                StartCoroutine(wait());
                truckCam.SetActive(true);

            }
        }
    }

    IEnumerator wait() 
    {
        yield return new WaitForSeconds(.5f);
        playerModel.SetActive(false);
        enterTruckTxt.gameObject.SetActive(false);
        isDriving = true;
    }
}
