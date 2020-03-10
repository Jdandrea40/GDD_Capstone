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
    public int CurrWaveText { get; set; }
    public bool WaveInProgress { get; set; }
    // Checks if the pause menu (or any pop up canvas) is currently active
    public bool IsPaused { get; set; }
    // Used to only allow the PauseMenu to be activated while in a game
    public bool InGame { get; set; } = false;
    public List<GameObject> SpawnedEnemies { get; set; }

    GameObject pauseMenu;
    GameObject winMenu;
    GameObject loseMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = Resources.Load<GameObject>("PopUpCanvases/PauseCanvas");
        winMenu = Resources.Load<GameObject>("PopUpCanvases/WinCanvas");
        loseMenu = Resources.Load<GameObject>("PopUpCanvases/LoseCanvas");
        MaxWaveCount = WaveSpawner.TotalWaves;
        WaveInProgress = false;
        IsPaused = false;
        currWave = CurWaveCount;
        LoadScriptableObjects();
    }

    // Update is called once per frame
    void Update()
    {
        // Used to pause the game
        if (InGame && !IsPaused && Input.GetKey(KeyCode.Escape))
        {
            Instantiate(pauseMenu);
        }

        // Used to increase the Health Modifier of Enemies
        if (currWave != CurWaveCount)
        {
            EnemyHealthModifier += CurWaveCount;
            currWave = CurWaveCount;
        }

        // LOSE CONDITION
        if (BaseHealth <= 0)
        {
            Instantiate(loseMenu);
        }
        // WIN CONDITION
        if (CurWaveCount == MaxWaveCount && SpawnedEnemies.Count < 1)
        {
            Instantiate(winMenu);
        }
    }

    /// <summary>
    /// Handles loading in all the pieces from the Resources folder
    /// into the PCM
    /// TODO: Is this the best way to handle this?
    /// </summary>
    public void LoadScriptableObjects()
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
