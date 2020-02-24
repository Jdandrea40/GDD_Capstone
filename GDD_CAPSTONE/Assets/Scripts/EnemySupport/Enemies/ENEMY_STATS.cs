using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_STATS : Singleton<ENEMY_STATS>
{
    #region BASIC AIRPLANE

    public int BASIC_AIRPLANE_HEALTH = 50;
    public int BASIC_AIRPLANE_MOVE_SPEED = 2;

    #endregion

    #region BASIC TROOPER

    public int BASIC_TROOPER_HEALTH = 40;
    public int BASIC_TROOPER_MOVE_SPEED = 10;

    #endregion

    #region BOSS PLANE 1
    
    public int BOSS_PLANE_1_HEALTH = 150;
    public int BOSS_PLANE_1_MOVE_SPEED = 2;
    public Color BOSS_PLANE_1_COLOR = new Color(53, 53, 53);
    #endregion
}
