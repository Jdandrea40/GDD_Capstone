using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This class will handle all functionality of the LevelSelect screen
/// </summary>
public class LevelSelect : MonoBehaviour
{
    [SerializeField] GameObject[] panels;

    /// <summary>
    /// Method will take in a value passed in by the corresponding button
    /// 1 = Easy, 2 = Medium, 3 = Hard
    /// and set the appropriate panel to Active while setting the rest to inactive
    /// </summary>
    /// <param name="difficulty"></param>
    public void TabChange(int difficulty)
    {
        // Loops through the list of panels
        // populated in the Inspector
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == difficulty)
            {
                panels[i].SetActive(true);
            }
            else
            {
                panels[i].SetActive(false);
            }
        }
    }
    /// <summary>
    /// Method will take in the appropriate string name and load that Scene
    /// the name is (levelName) is equal to the name created in the Scenes Folder
    /// and passed in by populating the OnClick() event in the inspector
    /// </summary>
    /// <param name="levelName"></param>
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
