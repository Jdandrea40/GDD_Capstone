using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerBase : ScriptableObject
{
    [SerializeField] string Name;
    [SerializeField] int Range;
    [SerializeField] float FireRateModifier;
}
