﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BasicAirplane : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        Health = eStat.BASIC_AIRPLANE_HEALTH;
        moveSpeed = eStat.BASIC_AIRPLANE_MOVE_SPEED;
        scrapGiven = 10;
    }
}
