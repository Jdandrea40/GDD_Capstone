using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurretTop : ScriptableObject
{
    public string Name;
    public int Damage;
    public float FireRate;
    public bool SplashDamage;
    public Sprite TurretSprite;
    public Sprite ProjectileSprite;

}
