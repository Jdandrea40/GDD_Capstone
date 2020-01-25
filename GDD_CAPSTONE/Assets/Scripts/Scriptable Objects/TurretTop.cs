using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurretTop : ScriptableObject
{
    [SerializeField] string Name;
    [SerializeField] int Damage;
    [SerializeField] float FireRate;
    [SerializeField] bool SplashDamage;
    [SerializeField] Sprite TurretSprite;

}
