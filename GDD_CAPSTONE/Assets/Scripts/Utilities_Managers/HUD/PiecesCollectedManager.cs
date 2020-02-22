using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Being used for showing how have been collected in the HUD
/// </summary>
public class PiecesCollectedManager : Singleton<PiecesCollectedManager>
{
    public enum TowerPieceEnum 
    { 
        SINGLEFIRE_TOP, RAPIDFIRE_TOP, CANNON_TOP,
        SHORTRANGE_BASE, MIDRANGE_BASE, LONGRANGE_BASE,
        KINETIC_AMMO, CRYO_AMMO, INCENDIARY_AMMO 
    };

    [SerializeField] public TurretTop[] pcTop;
    [SerializeField] public TowerBase[] pcBase;
    [SerializeField] public AmmoType[] pcAmmo;

    public Dictionary<TowerPieceEnum, int> CollectedPieces = new Dictionary<TowerPieceEnum, int>();

    private void Awake()
    {
        for (int i = 0; i < 9; i++)
        {
            CollectedPieces[(TowerPieceEnum)i] = 1;
        }
        
    }
}
