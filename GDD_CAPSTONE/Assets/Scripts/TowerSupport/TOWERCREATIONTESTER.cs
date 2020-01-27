using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOWERCREATIONTESTER : Tower
{


    SpriteRenderer sr;

    [SerializeField] TurretTop[] tTop;
    [SerializeField] TowerBase[] tBase;
    [SerializeField] AmmoType[] tAmmo;

    int r_Top, r_Base, r_Ammo;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        r_Top = Random.Range(0, tTop.Length);
        r_Base = Random.Range(0, tBase.Length);
        r_Ammo = Random.Range(0, tAmmo.Length);
        
        //CreateTower(r_Top, r_Base, r_Ammo);

        Debug.Log("Damage: " + Damage + " RoF: " + FireRate + " Range " + Range + " SD " + SplashDamage + " Slow " + Slow + " DoT " + DamageOverTime + " DoTA " + DoTAmount);

    }

    //protected override void CreateTower(int top, int bot, int ammo)
    //{
    //    base.CreateTower();
    //    sr.sprite = tTop[top].TurretSprite;
    //    Damage = tTop[top].Damage + tAmmo[ammo].ImpactDamage;
    //    FireRate = tTop[top].FireRate + tBase[bot].FireRateModifier;
    //    Range = tBase[bot].Range;

    //    SplashDamage = tTop[top].SplashDamage;
    //    Slow = tAmmo[ammo].Slow;
        
    //    DamageOverTime = tAmmo[ammo].DamageOverTime;
    //    if (DamageOverTime)
    //    {
    //        DoTAmount = tAmmo[ammo].DoTAmount;
    //    }
    //    else
    //    {
    //        DoTAmount = 0;
    //    }

    //}
}
