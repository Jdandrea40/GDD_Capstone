using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class to handle all things on the Help Menu Screen
/// </summary>
public class HelpMenu : MonoBehaviour
{
    [SerializeField] GameObject detailPanel;

    /// <summary>
    /// Method to go to the levelSelected
    /// Scene name must be passed in through Inspector Button OnClick()
    /// </summary>
    /// <param name="levelSelected"></param>
    public void GoTo(string levelSelected)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sounds.BUTTON_CLICK);
        SceneManager.LoadScene(levelSelected);
    }
   public void OpenDetails()
   {
        detailPanel.SetActive(true);
   }

    public void CloseDetails()
    {
        detailPanel.SetActive(false);

    }
}
