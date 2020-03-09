using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Button spawnButton;
    [SerializeField] Text countDownText;
    [SerializeField] Sprite fastForwardImg;

    bool firstClick = true;
    WaveSpawnEvent waveSpawnEvent;
    int spawnTimer = 15;
    IEnumerator countdownCoRout;
    
    #region PROPERTIES

    #endregion

    public void AddWaveSpawnListener(UnityAction listener)
    {
        waveSpawnEvent.AddListener(listener);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Event used to spawn a new wave (WaveSpawner.cs listener)
        waveSpawnEvent = new WaveSpawnEvent();
        EventManager.AddWaveSpawnInvoker(this);

        // Tracks the amount ofenemies killed for text changing
        GameplayManager.Instance.EnemiesKilled = 0;

        // The Countdown text located on the HUD
        countDownText.text = " ";
        countdownCoRout = CountDown();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameplayManager.Instance.WaveInProgress || GameplayManager.Instance.IsPause)
        {
            spawnButton.interactable = false;
        }
        else
        {

            spawnButton.interactable = true;
        }

    }
    /// <summary>
    /// Coroutine used to decrement the spawn timer
    /// </summary>
    /// <returns></returns>
    IEnumerator CountDown()
    {
        countDownText.text = spawnTimer.ToString();
        spawnTimer = 15;
        yield return new WaitForSeconds(2);
        while (spawnTimer > 0)
        {
            spawnTimer-= 1;
            countDownText.text = spawnTimer.ToString();
            yield return new WaitForSeconds(1);
        }
        SpawnWave();
    }

    /// <summary>
    /// Method used to spawn a new wave (WaveSpawner.cs Listener)
    /// When pressed, it will stop the current countdown coroutine and restart it
    /// by resetting the spawn timer
    /// </summary>
    public void SpawnWave()
    {
        
        if (firstClick)
        {
            // Changes the initial image of the Play image to Fast forward image
            spawnButton.image.sprite = fastForwardImg;
        }


        // A sanity check to insure that the button cannot be mashed and there is still more waves to be spawned
        if (!GameplayManager.Instance.WaveInProgress && (GameplayManager.Instance.CurWaveCount < GameplayManager.Instance.MaxWaveCount))
        {
            // Coroutine start/reset handling
            StopCoroutine(countdownCoRout);
            spawnTimer = 0;
            countdownCoRout = CountDown();
            StartCoroutine(countdownCoRout);
            waveSpawnEvent.Invoke();

        }
    }
}
