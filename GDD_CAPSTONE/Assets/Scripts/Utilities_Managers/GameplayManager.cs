using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : Singleton<GameplayManager>
{
    public static int EnemiesKilled { get; set; }
    public static int BaseHealth { get; set; }
    public static int MaxWaveCount { get; set; }
    public static int CurWaveCount { get; set; }
    public static bool WaveInProgress { get; set; }
    public static List<GameObject> SpawnEnemies { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        MaxWaveCount = 5;
        CurWaveCount = 0;
        WaveInProgress = false;
        SpawnEnemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(SpawnEnemies.Count);
        if (BaseHealth <= 0 || (CurWaveCount == MaxWaveCount && SpawnEnemies.Count <= 0))
        {
            CurWaveCount = 0;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
