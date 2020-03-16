using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_TestAirplane : Enemy
{
    public override void Awake()
    {
        base.Awake();
        Health = 500;
        moveSpeed = eStat.BASIC_AIRPLANE_MOVE_SPEED;
    }
}
