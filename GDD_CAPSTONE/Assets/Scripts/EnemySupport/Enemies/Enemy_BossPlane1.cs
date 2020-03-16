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
    protected override void SpawnItem()
    {
        float yOffset = 0;
        float xOffset = 0;

        for (int i = 0; i < 3; i++)
        {
            switch(i)
            {
                case 0:
                    yOffset = .3f;
                    xOffset = 0;
                    break;
                case 1:
                    yOffset = 0;
                    xOffset = -.3f;
                    break;
                case 2:
                    yOffset = 0;
                    xOffset = .3f;
                    break;
            
        }
            Instantiate(item, (transform.position + new Vector3(xOffset, yOffset, 0)), Quaternion.identity);
        }
    }
}
