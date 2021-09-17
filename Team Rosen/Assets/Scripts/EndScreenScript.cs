using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void quiteGame() 
    {
        //this.gameObject.SetActive(false);

        Application.Quit();
    }

    public void restartGame() 
    {
        this.gameObject.SetActive(false);

        SceneManager.LoadScene("Main");
    }
}
