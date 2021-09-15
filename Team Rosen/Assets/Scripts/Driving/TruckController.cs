using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    //Engine rev counter
    float revCount;

    //Current gear
                //-1 - Reverse
    int gear;   //0 - Neutral
                //1
                //2
                //3
                //4
                //5
                //6

    int minGear = -1;
    int maxGear = 7;

    //Max speed
    float maxSpeed = 1000f;

    //Minimum revs
    float minRev = 500f;
    //Maximum revs
    float maxRev = 2500f;

    //Time player has been holding the accelerator
    float timeHeld;

    // Start is called before the first frame update
    void Start()
    {
        //Reset
        timeHeld = 0;
        //Set starting revs and gear
        revCount = minRev;
        gear = 0;

    }

    void shiftUp() 
    {
        //Shit up a gear
        if(gear < maxGear) 
        {
            gear += 1;
            revCount = minRev + 100f;
            Debug.Log(gear);
        }
       
    }

    void shiftDown() 
    {
        //Shit down a gear
        if(gear > minGear) 
        {
            gear -= 1;
            if(gear != 0)
            revCount = maxRev - 100f;
            Debug.Log(gear);
        }
        
    }

    void drive() 
    {
        
    }


    void FixedUpdate()
    {
        Debug.Log(revCount);

        //Check player is in truck
        if (PlayerController.isDriving)
        {
            if (Input.GetKey("w"))
            {
                if(gear > 0) 
                {
                    //Start counting
                    timeHeld += 1 * Time.deltaTime;

                    if (revCount < maxRev - 1)
                    {
                        if (timeHeld <= 3f)
                        {
                            revCount += 2f;
                        }
                        else if (timeHeld > 2f)
                        {
                            revCount += 10f;
                        }
                    }
                    else
                    {
                        shiftUp();
                    }


                }
                else 
                {
                    shiftUp();
                }


            }
            else if (revCount > minRev+1)
            {

                if (Input.GetKey("s"))
                {
                    revCount -= 10f;
                }
                else 
                {
                    revCount -= 2f;

                }
            }
            else if(gear > minGear + 1)
            {
                shiftDown();
            }

        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
