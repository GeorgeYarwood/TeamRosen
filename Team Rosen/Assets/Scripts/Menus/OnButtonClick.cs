using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnButtonClick : MonoBehaviour
{
    public GameObject Settings;
    public GameObject Credits;
    public GameObject MainMenu;
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
}
