using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildableArea : MonoBehaviour
{
    SpriteRenderer sr;

    // Hover/Dehover Color support
    Color hoverColor;
    Color cantPlaceColor;
    Color startColor;

    [SerializeField] CanvasGroup cantPlaceCanvas;
    bool cantPlaceBool = false;
    bool hovering = false;
    bool occupied = false;                      // used to disallow reclicking a occupied tile

    [SerializeField] GameObject tower;


    BoxCollider2D bc2d;
    Vector2 center;

    public bool Occupied { get => occupied; set => occupied = value; }

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
        startColor = Color.white;
        hoverColor = Color.gray;
        cantPlaceColor = Color.red;

        cantPlaceCanvas.alpha = 0;
        cantPlaceCanvas.interactable = false;
        cantPlaceCanvas.blocksRaycasts = false;
    }

    // Mouse Enter support
    private void OnMouseEnter()
    {
        if (!occupied)
        {
            if (PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)HUD_CraftingUI.Instance.SelectedTop] > 0 &&
                PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)HUD_CraftingUI.Instance.SelectedBot + 3] > 0 &&
                PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)HUD_CraftingUI.Instance.SelectedAmmo + 6] > 0)
            {
                sr.material.color = hoverColor;
                hovering = true;
            }
            else
            {
                sr.material.color = cantPlaceColor;
                cantPlaceBool = true;
            }
        }
    }

    // Mouse Exit support
    private void OnMouseExit()
    {
        // reverts to the original tile color
        sr.material.color = startColor;
        hovering = false;

        cantPlaceCanvas.alpha = 0;
        cantPlaceCanvas.interactable = false;
        cantPlaceCanvas.blocksRaycasts = false;
    }

    // Click support
    private void OnMouseDown()
    {
        if (hovering && !occupied)
        {
            occupied = true;

            // Instantiates the Tower, and sets the Area to its Parent
            Instantiate(tower, transform.position, Quaternion.identity, transform);

        }
        else
        {
            cantPlaceCanvas.alpha = 1;
            cantPlaceCanvas.interactable = true;
            cantPlaceCanvas.blocksRaycasts = true;
        }
            

        
    }
}
