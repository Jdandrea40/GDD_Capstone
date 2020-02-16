using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_CraftingUI : Singleton<HUD_CraftingUI>
{
    enum Piece { TOP, BOT, AMMO };

    HUDManager hud;

    // being used to select the index of the current selected piece
    int selectedTop;
    int selectedBot;
    int selectedAmmo;

    public int SelectedTop { get => selectedTop; }
    public int SelectedBot { get => selectedBot; }
    public int SelectedAmmo { get => selectedAmmo; }

    [SerializeField] GameObject tower;

    [SerializeField] Toggle[] tops;
    [SerializeField] Toggle[] bots;
    [SerializeField] Toggle[] ammo;

    [SerializeField] ToggleGroup topGroup;

    Toggle topSelected;
    Toggle botSelected;
    Toggle ammoSelected;

    [SerializeField] Image[] createdTowerUI;
    [SerializeField] Text[] createdTowerText;

    [SerializeField] Text[] topAmountText;
    [SerializeField] Text[] botAmountText;
    [SerializeField] Text[] ammoAmountText;

    [SerializeField] TurretTop[] tTop;
    [SerializeField] TowerBase[] tBase;
    [SerializeField] AmmoType[] tAmmo;



    // Start is called before the first frame update
    void Start()
    {
        hud = HUDManager.Instance;

        PiecesCollectedManager.Instance.standardTurretTop = 0;
        PiecesCollectedManager.Instance.rapidFireTop = 0;
        PiecesCollectedManager.Instance.rocketTop = 0;
    }

    // Responsible for updating the UI image and Text in the Crafting window
    public void TowerUIUpdate(int index, int piece)
    {
        switch (piece)
        {
            case (int)Piece.TOP:
                // Sprite
                createdTowerUI[0].sprite = tTop[index].TurretSprite;

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

                // Text
                createdTowerText[2].text = tAmmo[index].Name;
                break;
        }

    }
    public GameObject CreateTower(TurretTop top, TowerBase bottom, AmmoType ammo)
    {
        
        return tower;
    }

    // 0 = Single Fire, 1 = Rapid Fire, 2 = Cannon"
    public void TopPieceSelected(int pieceSelected)
    {
        //switch (pieceSelected)
        //{
        //    case 0:
        //        Debug.Log(pieceSelected);
        //        break;
        //    case 1:
        //        Debug.Log(pieceSelected);
        //        break;
        //    case 2:
        //        Debug.Log(pieceSelected);
        //        break;
        //}

        selectedTop = pieceSelected;
        TowerUIUpdate(selectedTop, (int)Piece.TOP);
        
    }
    public void BotPieceSelected(int pieceSelected)
    {
        //switch (pieceSelected)
        //{
        //    case 0:
        //        Debug.Log(pieceSelected);
        //        break;
        //    case 1:
        //        Debug.Log(pieceSelected);
        //        break;
        //    case 2:
        //        Debug.Log(pieceSelected);
        //        break;
        //}

        selectedBot = pieceSelected;
        TowerUIUpdate(selectedBot, (int)Piece.BOT);

    }
    public void AmmoPieceSelected(int pieceSelected)
    {
        //switch (pieceSelected)
        //{
        //    case 0:
        //        Debug.Log(pieceSelected);
        //        break;
        //    case 1:
        //        Debug.Log(pieceSelected);
        //        break;
        //    case 2:
        //        Debug.Log(pieceSelected);
        //        break;
        //}

        selectedAmmo = pieceSelected;
        TowerUIUpdate(selectedAmmo, (int)Piece.AMMO);

    }

}
