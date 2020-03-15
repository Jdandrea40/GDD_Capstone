using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // Starts the "queue" at 0
    int currentWave = 0;
    int resumeFrom = 0;
    // A list of EnemyWave (ScriptableObjects)
    [SerializeField] public List<EnemyWave> Wave = new List<EnemyWave>();


    // The total number of waves in the List, is passe dto the HUD for unpdating text
    private static int totalWaves;   
    public static int TotalWaves { get => totalWaves; set => totalWaves = value; }


    // called before first frame
    // Need this so that game gets the Max before it checks win con
    private void Awake()
    {
        totalWaves = Wave.Count;
        EventManager.AddWaveSpawnListener(InvokeSpawner);
    }

    /// <summary>
    /// Needed this since you cannot (well maybe) incoke a coroutine 
    /// thorugh the event
    /// </summary>
    void InvokeSpawner()
    {
        // boolean so that you dont Spam Spawn enemies
        // waits till the last enemy is spawned
        GameplayManager.Instance.WaveInProgress = true;

        // Wont spawn more than the max wave amount
        if (currentWave < Wave.Count)
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    /// <summary>
    /// Coroutine for spawning enemies
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnEnemies()
    {
        GameplayManager.Instance.CurrWaveText++;
        // Loops through the current wave and spawns its corresponding enemy
        for (int enemy = 0; enemy < Wave[currentWave].Enemies.Length; enemy++)
        {
            // so long as the game is not paused, do it
            if (!GameplayManager.Instance.IsPaused)
            {
                Instantiate(Wave[currentWave].Enemies[enemy], transform.position, Quaternion.identity);
                yield return new WaitForSeconds(.3f);

            }
            // else, wait until its not paused anymore
            else
            {
                yield return new WaitUntil(() => !GameplayManager.Instance.IsPaused);
                yield return new WaitForSeconds(.3f);
            }
        }
        currentWave++;
        GameplayManager.Instance.CurWaveCount++;
        GameplayManager.Instance.WaveInProgress = false;
    }

}
