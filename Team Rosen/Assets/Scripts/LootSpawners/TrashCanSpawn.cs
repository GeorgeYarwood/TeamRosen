using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanSpawn : MonoBehaviour
{
    bool isCollected;
    public GameObject TrashCan;
    public float SpawnRate = 1f;
    public float SpawnChance = 1f;

    public 
    // Start is called before the first frame update
    void Start()
    {
        TrashCan.SetActive(false);
        if (Random.value < SpawnChance)
        {
            TrashCan.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
