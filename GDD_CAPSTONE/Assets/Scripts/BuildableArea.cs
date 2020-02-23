using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildableArea : MonoBehaviour
{
    SpriteRenderer sr;

    // Hover/Dehover Color support
    public Color hoverColor;
    public Color startColor;
    bool hovering = false;
    bool occupied = false;                      // used to disallow reclicking a occupied tile

    [SerializeField] GameObject tower;


    BoxCollider2D bc2d;
    Vector2 center;

    #region EVENTS



    #endregion
    private void Start()
    {


        // Components
        sr = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();

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
            if (PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)HUD_CraftingUI.Instance.SelectedTop] > 0 &&
                PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)HUD_CraftingUI.Instance.SelectedBot + 3] > 0 &&
                PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)HUD_CraftingUI.Instance.SelectedAmmo + 6] > 0)
            {
                occupied = true;
                Instantiate(tower, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("CANT");
            }
            

        }
    }
}
