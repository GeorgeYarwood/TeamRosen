using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    float throwForce = 600;
    Vector3 objectPos;
    float distance;
    public bool canHold = true;
    public GameObject item;
    public GameObject tempParent;
    public bool isHolding = false;


    public GameObject pickUpTxt;
 
    void Update()
    {
        if (!PlayerController.isDriving) 
        {

            //If player left clicks
            if (Input.GetMouseButton(0))
            {

                //Check is player is looking at a bag
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                //If we hit
                if (Physics.Raycast(ray, out hit,5f))
                {
                    if (hit.transform.tag == "Bag")
                    {
                        //Set the current item to the gameobject we hit
                        item = hit.transform.gameObject;


                        isHolding = true;

                        item.GetComponent<Rigidbody>().useGravity = false;
                        //item.GetComponent<Rigidbody>().detectCollisions = true;


                    }
                   
                }


            }

            //If player releases left click
            if (Input.GetMouseButtonUp(0))
            {
                isHolding = false;
                item.GetComponent<Rigidbody>().useGravity = true;
                item.GetComponent<Rigidbody>().detectCollisions = true;


            }


            if (isHolding == true)
            {
                //item.GetComponent<Rigidbody>().velocity = Vector3.zero;
                //item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                item.transform.SetParent(tempParent.transform);

                //Vector3 modPos = new Vector3(tempParent.transform.position.x +2, tempParent.transform.position.y, tempParent.transform.position.z);
                item.transform.position = tempParent.transform.position;

                if (Input.GetMouseButtonDown(1))
                {
                    //throw
                }
                else
                {

                    objectPos = item.transform.position;
                    item.transform.SetParent(null);
                    item.GetComponent<Rigidbody>().useGravity = true;
                    item.transform.position = objectPos;
                }
            }
        }
       
    }

    

}
