using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_CraftingUI : Singleton<HUD_CraftingUI>
{
    enum Piece { TOP, BOT, AMMO };

    // being used to select the index of the current selected piece
    int selectedTop;
    int selectedBot;
    int selectedAmmo;

    public int SelectedTop { get => selectedTop; }
    public int SelectedBot { get => selectedBot; }
    public int SelectedAmmo { get => selectedAmmo; }

    //[SerializeField] GameObject tower;

    [SerializeField] Toggle[] toggleTops;
    [SerializeField] Toggle[] toggleBots;
    [SerializeField] Toggle[] toggleAmmo;

    //[SerializeField] ToggleGroup topGroup;

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
        EventManager.ScrapUsedListener(UpdateItemCount);
        UpdateItemCount();
    }

    void UpdateItemCount()
    {
        
        for (int i = 0; i < PiecesCollectedManager.Instance.CollectedPieces.Count; i++)
        {Debug.Log("POOP");
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
            }
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

    // 0 = Single Fire, 1 = Rapid Fire, 2 = Cannon"
    public void TopPieceSelected(int pieceSelected)
    {
        selectedTop = pieceSelected;
        TowerUIUpdate(selectedTop, (int)Piece.TOP);
        
    }
    public void BotPieceSelected(int pieceSelected)
    {
        selectedBot = pieceSelected;
        TowerUIUpdate(selectedBot, (int)Piece.BOT);

    }
    public void AmmoPieceSelected(int pieceSelected)
    {
        selectedAmmo = pieceSelected;
        TowerUIUpdate(selectedAmmo, (int)Piece.AMMO);
    }

}
