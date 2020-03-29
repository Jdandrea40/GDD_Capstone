using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BasicTrooper : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        Health = eStat.BASIC_TROOPER_HEALTH;
        moveSpeed = eStat.BASIC_TROOPER_MOVE_SPEED;scrapGiven = 5;
    }

}
