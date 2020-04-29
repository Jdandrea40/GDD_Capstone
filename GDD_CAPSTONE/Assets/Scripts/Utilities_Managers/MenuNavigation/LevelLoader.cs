using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class must be put in all levels to properly start a game
/// </summary>
public class LevelLoader : MonoBehaviour
{   
    // Called before Start()
    private void Awake()
    {
        // Starting stats
        GameplayManager.Instance.BaseHealth = 100;
        GameplayManager.Instance.CurWaveCount = 0;
        GameplayManager.Instance.CurrWaveText = 0;
        GameplayManager.Instance.EnemyHealthModifier = 0;
        GameplayManager.Instance.ScrapCollected = 100;
        GameplayManager.Instance.SpawnedEnemies = new List<GameObject>();
        GameplayManager.Instance.InGame = true;
        GameplayManager.Instance.IsPaused = false;
        GameplayManager.Instance.WaveInProgress = false;
        GameplayManager.Instance.ScrapCostToBuild = 25;

        GameplayManager.Instance.TopPieceMaxRange = 10;
        GameplayManager.Instance.BasePieceMaxRange = 10;
        GameplayManager.Instance.AmmoPieceMaxRange = 10;

        // Sets all inventory pieces to 1
        for (int i = 0; i < 9; i++)
        {
            PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)i] = 1;

        }

        // Starting position for toggles
        HUD_CraftingUI.SelectedTop = 0;
        HUD_CraftingUI.SelectedBot = 0;
        HUD_CraftingUI.SelectedAmmo = 0;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        // Destroys itself after running Awake()
        Destroy(gameObject);
    }

}
