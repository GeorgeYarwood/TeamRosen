using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanSpawn : MonoBehaviour
{
    bool isCollected;
    public GameObject[] TrashCans;

    bool ready = false;
    int randomIndex;

    public 
    // Start is called before the first frame update
    void Start()
    {
        /*TrashCan.SetActive(false);
        if (Random.value < SpawnChance)
        {
            TrashCan.SetActive(true);
        }*/
        
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
            

            
            TrashCans[randomIndex].SetActive(true);
            if(Input.GetKeyDown(KeyCode.LeftControl) == true)
            {
                TrashCans[randomIndex].SetActive(false);
                randomIndex = Random.Range(0, TrashCans.Length);
                TrashCans[randomIndex].SetActive(true);
                //print("Key works");
            }
        }
        
        




    }
}
