using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruckController : MonoBehaviour
{
    //Rev counter UI
    public Slider revCounterUI;

    public Text gearTxt;

    //Truck rigidbody
    public Rigidbody truckRb;

    //Truck Pivot
    public GameObject truckPivot;

    //Where player will spawn when leaving truck
    public GameObject truckExit;

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
    float maxSpeed = 20f;

    //Minimum revs
    float minRev = 500f;
    //Maximum revs
    float maxRev = 2500f;

    //Time player has been holding the accelerator
    float timeHeld;

    //When in process of changing gear
    bool gearChanging;

    //Different force values for different stages of revs
    int lowForce = 15;
    int highForce = 18;

    //Trucks current speed
    float speed;

    //Up vector
    Vector3 upVec = new Vector3(0, 1, 0);

    //Truck last pos
    Vector3 lastPosition;

    //Engine sound
    public AudioClip idleAud;
    public AudioClip ignitionAud;

    public AudioSource engineAud;

    //If truck is running
    bool isOn;

    public GameObject truckStopMsg;
    
    // Start is called before the first frame update
    void Start()
    {
        //Is the truck running
        isOn = false;

        //Reset
        timeHeld = 0;
        //Set starting revs and gear
        revCount = minRev;
        gear = 0;

        //Get Rigidbody
        truckRb = GetComponent<Rigidbody>();

    }

    void shiftUp() 
    {
        //Shit up a gear
        if(gear < maxGear) 
        {
            gear += 1;
            revCount = minRev + 500f;
            //Debug.Log(gear);
            StartCoroutine(gearChangeWait());
            engineAud.pitch = 0.95f;


        }
       
    }

    void shiftDown() 
    {
        //Shit down a gear
        if(gear > minGear) 
        {
            gear -= 1;
            if(gear != 0)
            revCount = maxRev - 500f;
            //Debug.Log(gear);
            StartCoroutine(gearChangeWait());
            engineAud.pitch = 1.15f;
        }

    }

    IEnumerator gearChangeWait() 
    {
        gearChanging = true;
        

        yield return new WaitForSeconds(1.5f);
        gearChanging = false;

    }


    void FixedUpdate()
    {
        //Debug.Log(revCount);

        //Work out current speed
        speed = (transform.position - lastPosition).magnitude / Time.deltaTime;


        //Check player is in truck
        if (PlayerController.isDriving && isOn)
        {
            if (!engineAud.isPlaying) 
            {
                engineAud.clip = idleAud;
                engineAud.Play();
            }

            if (gearChanging && speed > 2) 
            {
                if(gear < 3) 
                {
                    truckRb.AddForce(truckRb.transform.forward * (7 * gear));
                }
                else 
                {
                    truckRb.AddForce(truckRb.transform.forward * (5 * gear));
                }
                
            }

            //Acceleration

            //Reverse gear
            if(Input.GetKey("s") && speed == 0 && gear <= 0) 
            {
                gear = -1;
                truckRb.AddForce(truckRb.transform.forward * -400);
            }

            else if (Input.GetKey("w") && !gearChanging && speed < maxSpeed)
            {
                if(gear > 0) 
                {
                    //Start counting
                    timeHeld += 1 * Time.deltaTime;

                    if (revCount < maxRev - Random.Range(0,500))
                    {
                        if (timeHeld <= 3f)
                        {
                           
                            revCount += 2f;
                            truckRb.AddForce(truckRb.transform.forward * lowForce);

                        }
                        else if (timeHeld > 4f)
                        {
                            revCount += 10f;
                            truckRb.AddForce(truckRb.transform.forward * highForce);

                        }
                        engineAud.pitch += 0.001f;
                    }
                    else if(gear < maxGear)
                    {
                        shiftUp();
                    }
                    else 
                    {
                        truckRb.AddForce(truckRb.transform.forward * highForce);

                    }


                }
                else 
                {
                    shiftUp();
                }


            }
            else if (!Input.GetKey("w") && revCount > minRev+1)
            {
               
                if (Input.GetKey("s"))
                {
                    //Start counting
                    timeHeld += 1 * Time.deltaTime;

                    revCount -= 40f;
                    truckRb.AddForce(-(truckRb.transform.forward * lowForce));
                }
                else 
                {
                    revCount -= 5f;
              
                    if(gear < 4) 
                    {
                        truckRb.AddForce(truckRb.transform.forward * (6 * gear));
                    }
                    else 
                    {
                        truckRb.AddForce(truckRb.transform.forward * (2 * gear));
                    }
                    

                }
                engineAud.pitch -= 0.001f;
            }
            if(gear > minGear + 1 && revCount <= minRev + Random.Range(0, 500))
            {
                shiftDown();
            }

            else if (Input.GetKeyUp("w")) 
            {
                timeHeld = 0;

            }

            

            //Steering

            //Turn amount
            double left = 0;


            //Right turn
            if (Input.GetKey("d")) 
            {
               
                    if (speed < 1)
                    {
                        left = .05f;

                    }

                    else 
                    {
                        left = .3f;

                    }
                   
                
               
               
            }

            //Left turn
            if (Input.GetKey("a"))
            {
               
                    if (speed < 1)
                    {
                        left = -.05f;

                    }

                    else
                    {
                        left = -.3f;

                    }
                
                
               
            }

            transform.Rotate(0, (float)left, 0);

            Debug.Log("Current speed: " + speed);

            lastPosition = transform.position;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        revCounterUI.value = revCount;

        if(gear != 0 && gear != -1) 
        {
            gearTxt.text = gear.ToString();

        }
        else if(gear == 0) 
        {
            gearTxt.text = "N";
        }
        else if(gear == -1) 
        {
            gearTxt.text = "R";
        }

        if (Input.GetKey("e") &&!isOn && !engineAud.isPlaying) 
        {

            engineAud.clip = ignitionAud;
            engineAud.Play();
            StartCoroutine(startTruck());
        }



        if (Input.GetKey("e") && PlayerController.isDriving && speed <=0)
        {
            Camera truckCam = GetComponentInChildren<Camera>();
            truckCam.gameObject.SetActive(false);
            PlayerController.playerModel.SetActive(true);
            PlayerController.playerModel.transform.position = truckExit.transform.position;
            engineAud.Stop();
            StartCoroutine(wait());
        }
        else if(Input.GetKey("e") && PlayerController.isDriving && speed > 5)
        {
            StartCoroutine(stopTruck());
        }


    }

    IEnumerator startTruck() 
    {


        yield return new WaitForSeconds(engineAud.clip.length);
        isOn = true;

    }


    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        PlayerController.isDriving = false;

    }

    IEnumerator stopTruck() 
    {
        truckStopMsg.SetActive(true);
        yield return new WaitForSeconds(3f);
        truckStopMsg.SetActive(false);
    }
}
