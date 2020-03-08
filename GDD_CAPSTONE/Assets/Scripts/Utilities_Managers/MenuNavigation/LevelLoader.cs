using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class must be put in all levels to properly start a game
/// </summary>
public class LevelLoader : MonoBehaviour
{   
    private void Awake()
    {
        // Starting stats
        GameplayManager.Instance.BaseHealth = 100;
        GameplayManager.Instance.CurWaveCount = 0;
        GameplayManager.Instance.SpawnedEnemies = new List<GameObject>();
        

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
        Destroy(gameObject);
    }

}
