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
    [SerializeField] Text[] itemCount;

    WaveSpawnEvent waveSpawnEvent;
    public void AddWaveSpawnListener(UnityAction listener)
    {
        waveSpawnEvent.AddListener(listener);
    }

    // Start is called before the first frame update
    void Start()
    {
        waveSpawnEvent = new WaveSpawnEvent();
        EventManager.AddWaveSpawnInvoker(this);

        EventManager.AddItemCollectedListener(UpdateItemCount);

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

    void UpdateItemCount(int turretPeiceCollected)
    {
        switch(turretPeiceCollected)
        {
            case 0:
                {
                    itemCount[turretPeiceCollected].text = PiecesCollectedManager.Instance.standardTurretTop.ToString();
                    break;
                }
            case 1:
                {
                    itemCount[turretPeiceCollected].text = PiecesCollectedManager.Instance.rapidFireTop.ToString();
                    break;
                }
            case 2:
                {

                    itemCount[turretPeiceCollected].text = PiecesCollectedManager.Instance.cannonTop.ToString();
                    break;
                }
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
