using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Being used for showing how have been collected in the HUD
/// </summary>
public class PiecesCollectedManager : Singleton<PiecesCollectedManager>
{
    #region TOP PIECES
    public int standardTurretTop = 0;
    public int rapidFireTop = 0;
    public int cannonTop = 0;
    #endregion

    #region BASE PIECES

    public int shortRangeBase = 0;
    public int mediumRangeBase = 0;
    public int longRangeBase = 0;
    #endregion

    #region AMMO PIECES

    public int standardAmmo = 0;
    public int fireAmmo = 0;
    public int slowAmmo = 0;
    #endregion
}
