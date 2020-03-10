﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class will handle all pop canvases
/// responsible for Pausing the game as well as
/// navigating any on screen buttons
/// </summary>
public class PopUpCanvases : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0;
        GameplayManager.Instance.IsPause = true;
    }

    // Resumes the Game
    public void Resume()
    { 
        GameplayManager.Instance.IsPause = false;
        //Time.timeScale = 1;
        Destroy(gameObject);
    }

    /// <summary>
    /// The scene name must be passed in through the buttons OnClick() 
    /// method in the Inspector (name must match exactly)
    /// </summary>
    /// <param name="sceneName"></param>
    public void GoToScene(string sceneName)
    {
        GameplayManager.Instance.InGame = false;
        GameplayManager.Instance.IsPause = false;
        //Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Restarts the current Level
    /// </summary>
    public void RestartCurrentLevel()
    {
        GameplayManager.Instance.IsPause = false;
        //Time.timeScale = 1;
        Scene thisLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisLevel.name);
    }

}
