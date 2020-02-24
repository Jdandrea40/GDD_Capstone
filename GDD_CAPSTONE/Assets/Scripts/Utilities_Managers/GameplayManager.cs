using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : Singleton<GameplayManager>
{
    int currWave;
    public int EnemiesKilled { get; set; }
    public int BaseHealth { get; set; }
    public int EnemyHealthModifier { get; set; }
    public int MaxWaveCount { get; set; }
    public int CurWaveCount { get; set; }
    public bool WaveInProgress { get; set; }
    public List<GameObject> SpawnedEnemies { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        MaxWaveCount = WaveSpawner.Instance.Wave.Count;
        CurWaveCount = 0;
        WaveInProgress = false;
        SpawnedEnemies = new List<GameObject>();
        currWave = CurWaveCount;
        AudioManager.Instance.PlayLoop(AudioManager.Sounds.BKG_LOOP);
    }

    // Update is called once per frame
    void Update()
    {
        // Used to increase the Health Modifier of Enemies
        if (currWave != CurWaveCount)
        {
            EnemyHealthModifier += CurWaveCount;
            currWave = CurWaveCount;
        }
        //Debug.Log(SpawnEnemies.Count);
        if (BaseHealth <= 0 || (CurWaveCount == MaxWaveCount && SpawnedEnemies.Count <= 0))
        {
            CurWaveCount = 0;
            // intialize the dictionary of collected pieces
            for (int i = 0; i < 9; i++)
            {
                PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)i] = 1;
            }

            HUD_CraftingUI.Instance.UpdateItemCount();

            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
