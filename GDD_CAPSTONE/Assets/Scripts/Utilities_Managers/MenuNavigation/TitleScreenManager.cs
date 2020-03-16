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
    }
    public void PlayGame()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sounds.GUN_SHOT);
        SceneManager.LoadScene("LevelSelect");
        
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Sounds.GUN_SHOT);

        Application.Quit();
    }
    
    public void HelpMenu()
    {

        SceneManager.LoadScene("HelpMenu");
    }
}
