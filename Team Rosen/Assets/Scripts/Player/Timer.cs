using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float TimeRemaining;
    public bool timerIsRunning = false;
    public Text timeText;

    public GameObject endScreen;

    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;

        TimeRemaining = 200f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (TimeRemaining > 0)
            {
                TimeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Ran out of time");
                TimeRemaining = 0;
                timerIsRunning = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                endScreen.SetActive(true);
            }
            DisplayTime(TimeRemaining);
        }

        if(TruckController.health <= 0) 
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            endScreen.SetActive(true);

        }

    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
