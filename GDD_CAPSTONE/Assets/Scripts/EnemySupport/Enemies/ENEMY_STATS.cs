using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_STATS : Singleton<ENEMY_STATS>
{
    #region BASIC AIRPLANE

    public int BASIC_AIRPLANE_HEALTH = 40;
    public float BASIC_AIRPLANE_MOVE_SPEED = 2f;

    #endregion

    #region BASIC TROOPER

    public int BASIC_TROOPER_HEALTH = 50;
    public float BASIC_TROOPER_MOVE_SPEED = 1.5f;

    #endregion

    #region BOSS PLANE 1
    
    public int BOSS_PLANE_1_HEALTH = 100;
    public float BOSS_PLANE_1_MOVE_SPEED = 1f;
    public Color BOSS_PLANE_1_COLOR = new Color(53, 53, 53);
    #endregion
}
