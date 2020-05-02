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
    [SerializeField] GameObject[] completedImage;

    private void Start()
    {
        LevelImageUpdate();
    }

    //TODO: this whol process needs to be better, require too much preknown knowledge to set up
    // LevelCompletionManager -> here -> LevelSelectButton -> GameplayManager -> LCM -> here
    void LevelImageUpdate()
    {
        for (int i = 0; i < LevelCompletionManager.Instance.completeledLevels.Count; i++)
        {
            if(LevelCompletionManager.Instance.completeledLevels[(LevelCompletionManager.LevelNames)i] == true && completedImage[i].activeSelf == false)
            {
                
                completedImage[i].SetActive(true);
            }
        }

    }

    /// <summary>
    /// Method will take in a value passed in by the corresponding button
    /// 1 = Easy, 2 = Medium, 3 = Hard
    /// and set the appropriate panel to Active while setting the rest to inactive
    /// </summary>
    /// <param name="difficulty"></param>
    public void TabChange(int difficulty)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sounds.BUTTON_CLICK);
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
        switch(levelName)
        {
            case ("GalletCity"):
                LevelCompletionManager.Instance.currentLevel = LevelCompletionManager.LevelNames.GALLETCITY;
                break;
            case ("TheForest"):
                LevelCompletionManager.Instance.currentLevel = LevelCompletionManager.LevelNames.THEFOREST;
                break;
            case ("Depot"):
                LevelCompletionManager.Instance.currentLevel = LevelCompletionManager.LevelNames.DEPOT;
                break;
            case ("Intersection"):
                LevelCompletionManager.Instance.currentLevel = LevelCompletionManager.LevelNames.INTERSECTION;
                break;
            default:
                System.Console.WriteLine("NOT A LEVEL");
                break;
        }
        Debug.Log(LevelCompletionManager.Instance.currentLevel);
        AudioManager.Instance.PlaySFX(AudioManager.Sounds.BUTTON_CLICK);
        SceneManager.LoadScene(levelName);
    }
}
