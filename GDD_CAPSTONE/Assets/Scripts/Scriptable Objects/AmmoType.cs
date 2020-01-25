using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AmmoType : ScriptableObject
{
    [SerializeField] string Name;
    [SerializeField] int ImpactDamage;
    [SerializeField] bool DamageOverTime;
    [SerializeField] int DoTAmount;
    [SerializeField] bool Slow;
    [SerializeField] Sprite AmmoSprite;

}
