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
    bool hovering = false;
    bool occupied = false;                      // used to disallow reclicking a occupied tile

    [SerializeField] GameObject tower;

    // Support for the BuildArea range indicator
    [SerializeField] GameObject rangeIndicator;
    float currTowerRange;
    Color rangeColor;

    BoxCollider2D bc2d;
    Vector2 center;

    // Used to revert to unoccupied when a tower is removed (Tower.cs)
    public bool Occupied { get => occupied; set => occupied = value; }
    // Used to pass the current selected range to the BA range Indicator (CHILD)
    public float CurrTowerRange { get => currTowerRange;  }
    // Used to indicate whether the player can play the turret
    public Color RangeColor { get => rangeColor; }

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

        rangeIndicator.SetActive(false);
        cantPlaceCanvas.alpha = 0;
        cantPlaceCanvas.interactable = false;
        cantPlaceCanvas.blocksRaycasts = false;
    }

    // Mouse Enter support
    private void OnMouseEnter()
    {
        // TODO: make this better?
        // In order to access the range of the current tower to build
        // Must get a ScriptableObject (TowerBase) to store the current select piece
        // then set it equal to the array of bases in PCM.pcBases of the HUD_CUI currently selected
        TowerBase selectedBase = PiecesCollectedManager.Instance.pcBase[HUD_CraftingUI.SelectedBot];
        currTowerRange = selectedBase.Range * 2;
        rangeIndicator.transform.localScale = new Vector2(currTowerRange, currTowerRange);

        // Prevents placement during paused and inssuffiecnt piece inventory
        if (!occupied && !GameplayManager.Instance.IsPaused)
        {
            
            // Checks the currently selected components
            if (PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)HUD_CraftingUI.SelectedTop] > 0 &&
                PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)HUD_CraftingUI.SelectedBot + 3] > 0 &&
                PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)HUD_CraftingUI.SelectedAmmo + 6] > 0)
            {
                // Can place color
                sr.material.color = hoverColor;
                rangeColor = hoverColor;
                hovering = true;
                rangeIndicator.SetActive(true);
            }
            // Hover color will be red
            else
            {
                sr.material.color = cantPlaceColor;
                rangeColor = cantPlaceColor;
            }
            
        }
    }

    // Mouse Exit support
    private void OnMouseExit()
    {
        // reverts to the original tile color
        sr.material.color = startColor;
        hovering = false;
        rangeIndicator.SetActive(false);
        // Canvas group removal
        cantPlaceCanvas.alpha = 0;
        cantPlaceCanvas.interactable = false;
        cantPlaceCanvas.blocksRaycasts = false;
    }

    // Click support
    private void OnMouseDown()
    {
        // Dissallows placement during Paused
        if (!GameplayManager.Instance.IsPaused)
        {
            // will only instantiate a tower when it is
            // hovered and not occupied
            if (hovering && !occupied)
            {
                occupied = true;
                AudioManager.Instance.PlaySFX(AudioManager.Sounds.TOGGLE_CLICK);
                // Instantiates the Tower, and sets the Area to its Parent
                Instantiate(tower, transform.position, Quaternion.identity, transform);

            }
            // needed this to prevent showing canvas when occupied
            else if (!occupied)
            {
                cantPlaceCanvas.alpha = 1;
                cantPlaceCanvas.interactable = true;
                cantPlaceCanvas.blocksRaycasts = true;
            }
        }
    }
}
