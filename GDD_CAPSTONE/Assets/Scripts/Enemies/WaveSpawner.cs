using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    List<GameObject> spawnedEnemies = new List<GameObject>();
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.AddWaveSpawnListener(InvokeSpawner);
    }

    void InvokeSpawner()
    {
        
        GameplayManager.WaveInProgress = true;
        StartCoroutine(SpawnEnemies());
        
    }

    IEnumerator SpawnEnemies()
    {
        
        i = 0;
        while (i <= enemies.Count - 1)
        {
            Instantiate(enemies[i], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            i++;
        }
        GameplayManager.CurWaveCount++;
        GameplayManager.WaveInProgress = false;
    }
}
