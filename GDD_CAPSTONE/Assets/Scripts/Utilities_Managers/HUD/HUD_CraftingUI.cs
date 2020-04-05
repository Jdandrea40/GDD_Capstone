using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_CraftingUI : MonoBehaviour
{
    enum Piece { TOP, BOT, AMMO };

    // being used to select the index of the current selected piece
    static int selectedTop;
    static int selectedBot;
    static int selectedAmmo;
    public static int SelectedTop { get => selectedTop; set => selectedTop = value; }
    public static int SelectedBot { get => selectedBot; set => selectedBot = value; }
    public static int SelectedAmmo { get => selectedAmmo; set => selectedAmmo = value; }

    // Collection of toggles to swap interactable
    [SerializeField] Toggle[] toggleTops;
    [SerializeField] Toggle[] toggleBots;
    [SerializeField] Toggle[] toggleAmmo;

    [SerializeField] Toggle buildAreaToggle;
    [SerializeField] Text buildAreaText;
    [SerializeField] Image buildAreaImg;
    // An array of the BKG images so that they can be swapped to red/normal based on amount
    [SerializeField] Image[] imageBKGS;

    Toggle topSelected;
    Toggle botSelected;
    Toggle ammoSelected;

    [SerializeField] Image[] createdTowerUI;
    [SerializeField] Text[] createdTowerText;

    //[SerializeField] Text[] topAmountText;
    //[SerializeField] Text[] botAmountText;
    //[SerializeField] Text[] ammoAmountText;

    [SerializeField] TurretTop[] tTop;
    [SerializeField] TowerBase[] tBase;
    [SerializeField] AmmoType[] tAmmo;
    
    
    [SerializeField] Text[] itemCountText;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.AddItemCollectedListener(UpdateItemCount);
        EventManager.AddItemBoughtListener(UpdateItemCount);
        EventManager.ScrapUsedListener(UpdateItemCount);
        
        // Game Start Initialization
        UpdateItemCount();
        TowerUIUpdate(0, 0);
        TowerUIUpdate(0, 1);
        TowerUIUpdate(0, 2);

    }
    /// <summary>
    /// called every frame
    /// </summary>
    private void Update()
    {
        // dissalows the toggle swapping during pause
        if (GameplayManager.Instance.IsPaused)
        {
            for (int i = 0; i < 3; i++)
            {
                toggleTops[i].interactable = false;
                toggleBots[i].interactable = false;
                toggleAmmo[i].interactable = false;
            }
            buildAreaToggle.interactable = false;
        }
        else
        {
            UpdateItemCount();
            // Checks the current state of the Toggle
            // will activate GPM-BuildArea
            // and Swap Cursor
            if (buildAreaToggle.isOn == true)
            {
                GameplayManager.Instance.CanBuildArea = true;
                ArtManager.Instance.SwapCursor(ArtManager.CursorToUse.BUILD_AREA);
                GameplayManager.Instance.CursorSwapReset = true;
            }
            else
            {
                GameplayManager.Instance.CanBuildArea = false;
                if (GameplayManager.Instance.CursorSwapReset)
                {
                    GameplayManager.Instance.CursorSwapReset = false;
                    ArtManager.Instance.SwapCursor(ArtManager.CursorToUse.NORMAL);
                }

            }
        }

        //Debug.Log(buildAreaToggle.isOn);
    }

    /// <summary>
    /// Updates the amount of scrap pieces collected
    /// uses the Dictionary in PCM as well as the TPE type casting
    /// -3/6 accounts for the different arrays of Scriptable objects currently impemented
    /// dictionary has 9 elements, so must subtract to get proper index
    /// </summary>
    public void UpdateItemCount()
    {
        for (int i = 0; i < PiecesCollectedManager.Instance.CollectedPieces.Count; i++)
        {
            itemCountText[i].text = PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)i].ToString();
            if (PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)i] <= 0)
            {
                if (i < 3)
                {
                    toggleTops[i].interactable = false;

                }
                else if (i > 2 && i <= 5)
                {
                    toggleBots[i - 3].interactable = false;
                }
                else
                {
                    toggleAmmo[i - 6].interactable = false;
                }
                imageBKGS[i].color = Color.red;
            }
            else
            {
                if (i < 3)
                {
                    toggleTops[i].interactable = true;
                }
                else if (i > 2 && i <= 5)
                {
                    toggleBots[i - 3].interactable = true;
                }
                else
                {
                    toggleAmmo[i - 6].interactable = true;
                }
                imageBKGS[i].color = Color.white;
            }
        }
        if (GameplayManager.Instance.ScrapCollected < 25)
        {
            buildAreaToggle.interactable = false;
            buildAreaToggle.isOn = false;
            buildAreaText.color = Color.red;
            buildAreaImg.color = Color.red;
        }
        else
        {
            buildAreaToggle.interactable = true;
            buildAreaText.color = Color.white;
            buildAreaImg.color = Color.white;
        }
    }
    // Responsible for updating the UI image and Text in the Crafting window
    public void TowerUIUpdate(int index, int piece)
    {
        switch (piece)
        {
            case (int)Piece.TOP:
                // Sprite
                createdTowerUI[0].sprite = tTop[index].TurretSprite;
                createdTowerUI[0].color = Color.white;

                // Text
                createdTowerText[0].text = tTop[index].Name;
                break;
            case (int)Piece.BOT:
                // Sprite
                createdTowerUI[1].sprite = tBase[index].BaseSprite;
                createdTowerUI[1].color = tBase[index].BaseColor;

                // Text
                createdTowerText[1].text = tBase[index].Name;
                break;
            case (int)Piece.AMMO:
                // Sprite
                createdTowerUI[2].sprite = tAmmo[index].AmmoSprite;
                createdTowerUI[2].color = Color.white;
                // Text
                createdTowerText[2].text = tAmmo[index].Name;
                break;
        }

    }
    /// <summary>
    /// Below handles all swapping between toggles in the crafting panel
    /// </summary>

    // 0 = Single Fire, 1 = Rapid Fire, 2 = Cannon"
    public void TopPieceSelected(int pieceSelected)
    {
        if (!GameplayManager.Instance.IsPaused)
        {
            selectedTop = pieceSelected;
            TowerUIUpdate(selectedTop, (int)Piece.TOP);
            AudioManager.Instance.PlaySFX(AudioManager.Sounds.TOGGLE_CLICK);
        }
    }
    public void BotPieceSelected(int pieceSelected)
    {
        if (!GameplayManager.Instance.IsPaused)
        {
            selectedBot = pieceSelected;
            TowerUIUpdate(selectedBot, (int)Piece.BOT);
            AudioManager.Instance.PlaySFX(AudioManager.Sounds.TOGGLE_CLICK);
        }

    }
    public void AmmoPieceSelected(int pieceSelected)
    {
        if (!GameplayManager.Instance.IsPaused)
        {
            selectedAmmo = pieceSelected;
            TowerUIUpdate(selectedAmmo, (int)Piece.AMMO);
            AudioManager.Instance.PlaySFX(AudioManager.Sounds.TOGGLE_CLICK);

        }
    }
}
