using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    
    public void Start()
    {
        if (!AudioManager.Instance.MusicSource.isPlaying)
        {
            AudioManager.Instance.PlayMusic(AudioManager.Sounds.BKG_LOOP, true);
        }
        ArtManager.Instance.SwapCursor(ArtManager.CursorToUse.NORMAL);
    }
    public void PlayGame()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sounds.BUTTON_CLICK);
        SceneManager.LoadScene("HelpMenu");
        
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlayButtonClick(AudioManager.Sounds.BUTTON_CLICK);
        Application.Quit();
    }
    
    public void HelpMenu()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sounds.BUTTON_CLICK);
        SceneManager.LoadScene("Credits");
    }
}
