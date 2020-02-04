using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Text enemiesKilledText;
    [SerializeField] Text waveCountText;
    [SerializeField] Text baseHealthText;
    [SerializeField] Button spawnButton;

    WaveSpawnEvent spawnEvent;
    public void AddWaveSpawnListener(UnityAction listener)
    {
        spawnEvent.AddListener(listener);
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnEvent = new WaveSpawnEvent();
        EventManager.AddWaveSpawnInvoker(this);
        GameplayManager.EnemiesKilled = 0;
        enemiesKilledText.text = " Enemies Killed: " + GameplayManager.EnemiesKilled;
        baseHealthText.text = " Base Health: " + GameplayManager.BaseHealth;
        waveCountText.text = " Wave: " + GameplayManager.CurWaveCount + " / " + GameplayManager.MaxWaveCount;
    }

    // Update is called once per frame
    void Update()
    {
        enemiesKilledText.text = " Enemies Killed: " + GameplayManager.EnemiesKilled;
        baseHealthText.text = " Base Health: " + GameplayManager.BaseHealth;
        waveCountText.text = " Wave: " + GameplayManager.CurWaveCount + " / " + GameplayManager.MaxWaveCount;


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
            spawnEvent.Invoke();
        }
    }
}
