using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanSpawn : MonoBehaviour
{
    bool isCollected;
    public GameObject[] TrashCans;
    Vector3[] originalPos;
    public static bool ready = false;
    int randomIndex;

    public 
    // Start is called before the first frame update
    void Start()
    {

        for (int i=0; i<TrashCans.Length; i++)
        {
            TrashCans[i].SetActive(false);
        }
        randomIndex = Random.Range(0, TrashCans.Length);
        //Only start changing them when they have all been disabled
        ready = true;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            //Select half of the arrays contents to be spawned randomly
            for(int i = 0; i< (TrashCans.Length /2); i++) 
            {
                randomIndex = Random.Range(0, TrashCans.Length);


                //Check if it has been picked up already
                if (TrashCans[randomIndex].GetComponentInChildren<TrashBag>().pickedUp != true) 
                {
                    //If not, spawn it
                    TrashCans[randomIndex].SetActive(true);
                }
                else 
                {
                    //Otherwise, go back and try again
                    i--;
                }
                
            }

            ready = false;
            

            if (Input.GetKeyDown(KeyCode.LeftControl) == true)
            {
                TrashCans[randomIndex].gameObject.SetActive(false);
                randomIndex = Random.Range(0, TrashCans.Length);
                TrashCans[randomIndex].gameObject.SetActive(true);
                //print("Key works");
            }
        }

    }
}
