using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // Starts the "queue" at 0
    int currentWave = 0;
    // A list of EnemyWave (ScriptableObjects)
    [SerializeField] public List<EnemyWave> Wave = new List<EnemyWave>();


    // The total number of waves in the List, is passe dto the HUD for unpdating text
    private static int totalWaves;   
    public static int TotalWaves { get => totalWaves; set => totalWaves = value; }

    // Start is called before the first frame update
    void Awake()
    {

    }
    private void Start()
    {
        totalWaves = Wave.Count;
        EventManager.AddWaveSpawnListener(InvokeSpawner);
    }
    void InvokeSpawner()
    {
        GameplayManager.Instance.WaveInProgress = true;
        if (currentWave < Wave.Count)
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies()
    {
       
        for (int enemy = 0; enemy < Wave[currentWave].Enemies.Length; enemy++)
        {
            Instantiate(Wave[currentWave].Enemies[enemy], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(.3f);
        }
        currentWave++;
        GameplayManager.Instance.CurWaveCount++;
        GameplayManager.Instance.WaveInProgress = false;
    }
}
