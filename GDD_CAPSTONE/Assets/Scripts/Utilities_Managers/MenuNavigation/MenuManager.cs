using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{ 

    public void PlayGame()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void HelpMenu()
    {

        SceneManager.LoadScene("HelpMenu");
    }
}
