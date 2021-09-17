using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBag : MonoBehaviour
{
    public bool pickedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Truck")
        {
            //Add time to the timer
            Timer.TimeRemaining += 10f;

            //Destroy bag
            this.gameObject.SetActive(false);
            DestroyImmediate(this.gameObject); 
            //Update trash bags
            TrashCanSpawn.ready = true;
        }
    }
}
