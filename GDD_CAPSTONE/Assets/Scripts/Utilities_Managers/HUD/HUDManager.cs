using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HUDManager : Singleton<HUDManager>
{
    [SerializeField] Text enemiesKilledText;
    [SerializeField] Text waveCountText;
    [SerializeField] Text baseHealthText;

    [SerializeField] Button spawnButton;
    
    

    WaveSpawnEvent waveSpawnEvent;

    #region PROPERTIES

    // GAME STAT TEXT SUPPORT
    public Text EnemiesKilledText { get => enemiesKilledText; set => enemiesKilledText = value; }
    public Text WaveCountText { get => waveCountText; set => waveCountText = value; }
    public Text BaseHealthText { get => baseHealthText; set => baseHealthText = value; }



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


        GameplayManager.EnemiesKilled = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameplayManager.WaveInProgress)
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
    }

    public void SpawnWave()
    {
        if (!GameplayManager.WaveInProgress && (GameplayManager.CurWaveCount < GameplayManager.MaxWaveCount))
        {
            waveSpawnEvent.Invoke();
        }
    }
}
