using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarAI : MonoBehaviour
{

    //Different states for our AI
    enum states {idle, following}


    //Distance AI has to be before starting to follow the player
    float followdist = 20f;


    //Car will drive between these two points while idle
    public GameObject Goal1;
    public GameObject Goal2;

    //Our player
    GameObject player;

    //This car
    NavMeshAgent car;

    //Current state
    states currState;

    //Distance from player
    float distToPlayer;

    //Cars rigidbody
    Rigidbody carRb;

    //Force applied when hitting player
    float bounceForce = 300;

    bool driving;

    bool waiting;

    // Start is called before the first frame update
    void Start()
    {
        //Start as idle
        currState = states.idle;

        player = GameObject.FindGameObjectWithTag("Truck");

        car = GetComponent<NavMeshAgent>();

        carRb = GetComponent<Rigidbody>();

    }

    IEnumerator wait() 
    {
        driving = true;
        yield return new WaitForSeconds(10f);
        driving = false;
    }

    IEnumerator waitToHitAgain() 
    {
        waiting = true;
        car.destination = car.transform.position;
        yield return new WaitForSeconds(5f);
        waiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState) 
        {
            case states.idle:

                //Dist between us and goal 1
                float goal1Dist = Vector3.Distance(transform.position, Goal1.transform.position);
                //Dist between us and goal 2
                float goal2Dist = Vector3.Distance(transform.position, Goal2.transform.position);

                //Distance between us and the player
                distToPlayer = Vector3.Distance(transform.position, player.transform.position);

                

                if(distToPlayer > followdist) 
                {
                    if (!driving) 
                    {

                        if (goal1Dist < goal2Dist)
                        {
                            car.destination = Goal2.transform.position;
                        }
                        else if (goal2Dist < goal1Dist)
                        {
                            car.destination = Goal1.transform.position;

                        }

                        StartCoroutine(wait());
                    }
                    
                }
                else if(PlayerController.isDriving)
                {
                    currState = states.following;
                }

           




                break;

            case states.following:


                if (!PlayerController.isDriving) 
                {
                    currState = states.idle;
                }

                if(distToPlayer < followdist) 
                {
                    if (!waiting) 
                    {
                        //Target the player
                        car.destination = player.transform.position;
                    }
                    
                }
                else 
                {
                    currState = states.idle;
                }


                break;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {


        if(collision.gameObject.tag == "Truck") 
        {
            //Take health from truck
            TruckController.health -= 5;
            carRb.AddForce(carRb.gameObject.transform.forward * -bounceForce);

            //Wait to hit again
            StartCoroutine(waitToHitAgain());
        }

    }
}
