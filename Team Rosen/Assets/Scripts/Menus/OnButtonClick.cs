using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnButtonClick : MonoBehaviour
{
    public GameObject Settings;
    public GameObject Credits;
    public GameObject MainMenu;
    public Scrollbar VolumeBar;
   

    public void NextScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void CanvasSettings()
    {
        MainMenu.SetActive(false);
        Settings.SetActive(true);
    }

    public void CanvasCredits()
    {
        Credits.SetActive(true);
            MainMenu.SetActive(false);
    }

    public void GoBack()
    {
        MainMenu.SetActive(true);
        Credits.SetActive(false);
        Settings.SetActive(false);
    }

    public void volumeController()
    {
        
       
    }
}
