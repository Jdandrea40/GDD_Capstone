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
    // Checks if the pause menu (or any pop up canvas) is currently active
    public bool IsPause { get; set; }
    // Used to only allow the PauseMenu to be activated while in a game
    public bool InGame { get; set; } = false;
    public List<GameObject> SpawnedEnemies { get; set; }

    GameObject pauseMenu;


    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = Resources.Load<GameObject>("PopUpCanvases/PauseCanvas");
        MaxWaveCount = WaveSpawner.TotalWaves;
        WaveInProgress = false;
        IsPause = false;
        currWave = CurWaveCount;
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (InGame && !IsPause && Input.GetKey(KeyCode.Escape))
        {
            Instantiate(pauseMenu);
        }
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
            SceneManager.LoadScene("TitleScreen");

            LoadGame();
        }
    }

    public void LoadGame()
    {
        PiecesCollectedManager.Instance.pcTop = new TurretTop[] {
            Resources.Load<TurretTop>("Tower Pieces/SO_TurretPieces/SingleFireTurret"),
            Resources.Load<TurretTop>("Tower Pieces/SO_TurretPieces/RapidFireTurret"),
            Resources.Load<TurretTop>("Tower Pieces/SO_TurretPieces/RocketTurret")
        };
        PiecesCollectedManager.Instance.pcBase = new TowerBase[] {
            Resources.Load<TowerBase>("Tower Pieces/SO_TurretBase/ShortRange"),
            Resources.Load<TowerBase>("Tower Pieces/SO_TurretBase/StandardRange"),
            Resources.Load<TowerBase>("Tower Pieces/SO_TurretBase/LongRange")
        };
        PiecesCollectedManager.Instance.pcAmmo = new AmmoType[] {
            Resources.Load<AmmoType>("Tower Pieces/SO_AmmoTypes/ArmorPiercingAmmo"),
            Resources.Load<AmmoType>("Tower Pieces/SO_AmmoTypes/CryoAmmo"),
            Resources.Load<AmmoType>("Tower Pieces/SO_AmmoTypes/IncendiaryAmmo"),
        };
    }
}
