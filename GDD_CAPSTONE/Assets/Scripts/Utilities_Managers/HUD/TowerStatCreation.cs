using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStatCreation : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] SpriteRenderer srChild;

    [SerializeField] Sprite[] tTop = new Sprite[3];
    [SerializeField] Sprite[] tBase = new Sprite[3];
    [SerializeField] Sprite[] tAmmo = new Sprite[3];

    [SerializeField] GameObject tower;
    //[SerializeField] TurretTop[] tTop;
    //[SerializeField] TowerBase[] tBase;
    //[SerializeField] AmmoType[] tAmmo;


    public Sprite ProjSprite { get; set; }
    int r_Top, r_Base, r_Ammo;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        srChild = GetComponentInChildren<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

        ProjSprite = tAmmo[r_Top];
        //CreateTower(r_Top, r_Base, r_Ammo);

        // Debug.Log("Damage: " + Damage + " RoF: " + FireRate + " Range " + Range + " SD " + SplashDamage + " Slow " + Slow + " DoT " + DamageOverTime + " DoTA " + DoTAmount);

    }

    //public void CreateTower(int top, int bot, int ammo)
    //{
    //    sr.sprite = tTop[top];
    //    srChild.sprite = tBase[bot];
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
}
