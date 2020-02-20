using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AmmoType : ScriptableObject
{
    public string Name;
    public int ImpactDamage;
    public bool DamageOverTime;
    public int DoTAmount;
    public bool Slow;
    public Sprite AmmoSprite;
    public Color color;

}
