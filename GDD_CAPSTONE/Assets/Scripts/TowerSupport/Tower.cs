using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    CircleCollider2D cc2d;
    // damage modifiers
    protected int Damage;             // Ammo + TurretTop
    protected float FireRate;         // TurretTop + TurretBase
    protected int DoTAmount;          // Ammo
    protected int Range;              // Base
    // visuals
    protected Color tColor;           // Ammo

    // status effects
    protected bool SplashDamage;      // TurretTop
    protected bool Slow;              // Ammo  
    protected bool DamageOverTime;    // Ammo
    

    private void Awake()
    {
        cc2d = GetComponent<CircleCollider2D>(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        EventManager.EnemeyDequeueListener(DequeueEnemy);
           
    }

    protected virtual void CreateTower()
    {

    }

    protected virtual void DequeueEnemy()
    {

    }
}
