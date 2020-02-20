using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableArea : MonoBehaviour
{
    SpriteRenderer sr;

    // Hover/Dehover Color support
    public Color hoverColor;
    public Color startColor;
    bool hovering = false;
    bool occupied = false;                      // used to disallow reclicking a occupied tile

    [SerializeField] GameObject tower;

    PiecesCollectedManager pcm;
    HUD_CraftingUI hudCUI;
    
    BoxCollider2D bc2d;
    Vector2 center;   

    private void Start()
    { 
        sr = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();

        pcm = PiecesCollectedManager.Instance;
        hudCUI = HUD_CraftingUI.Instance;


        // The center of the tile
        center = bc2d.size / 2;

        // Dehovering support
        startColor = sr.material.color;
        hoverColor = Color.gray;      
    }

    // Mouse Enter support
    private void OnMouseEnter()
    {
        if (!occupied)
        {
            // TODO: Invoke creation HUD
            sr.material.color = hoverColor;
            hovering = true;
        }
    }

    // Mouse Exit support
    private void OnMouseExit()
    {
        // reverts to the original tile color
        sr.material.color = startColor;
        hovering = false;
    }

    // Click support
    private void OnMouseDown()
    {
        if (hovering && !occupied)
        {
            Instantiate(tower, transform.position, Quaternion.identity);
            //Instantiate(tower, transform.position, Quaternion.identity);
            occupied = true;
        }
    }

    public void CreateTower()
    {
        TurretTop tTop = pcm.pcTop[hudCUI.SelectedTop];
        TowerBase tBase = pcm.pcBase[hudCUI.SelectedBot];
        AmmoType tAmmo = pcm.pcAmmo[hudCUI.SelectedAmmo];

        //Tower tower = new Tower(tTop.TurretSprite, tBase.BaseSprite, (tTop.Damage + tAmmo.ImpactDamage), (tTop.FireRate + tBase.FireRateModifier), tBase.Range, tAmmo.color, tAmmo.AmmoSprite, tTop.SplashDamage, tAmmo.Slow, tAmmo.DamageOverTime, tAmmo.DoTAmount);

        
    }
}
