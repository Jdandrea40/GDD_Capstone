using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HUDManager : Singleton<HUDManager>
{
    [SerializeField] Button spawnButton;    
    WaveSpawnEvent waveSpawnEvent;

    #region PROPERTIES

    #endregion

    public void AddWaveSpawnListener(UnityAction listener)
    {
        waveSpawnEvent.AddListener(listener);
    }

    // Start is called before the first frame update
    void Start()
    {
        waveSpawnEvent = new WaveSpawnEvent();
        EventManager.AddWaveSpawnInvoker(this);

        GameplayManager.Instance.EnemiesKilled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameplayManager.Instance.WaveInProgress)
        {
            spawnButton.interactable = false;
        }
        else
        {
            spawnButton.interactable = true;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        //SceneManager.LoadScene("TitleScreen");
    }

    public void SpawnWave()
    {
        if (!GameplayManager.Instance.WaveInProgress && (GameplayManager.Instance.CurWaveCount < GameplayManager.Instance.MaxWaveCount))
        {
            waveSpawnEvent.Invoke();
        }
    }
}
