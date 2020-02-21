using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BossPlane1 : Enemy
{
    public override void Awake()
    {
        base.Awake();
        Health = eStat.BOSS_PLANE_1_HEALTH;
        moveSpeed = eStat.BOSS_PLANE_1_MOVE_SPEED;
    }
}
