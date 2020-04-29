using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemDrop : MonoBehaviour
{
    // TODO: DO THIS BETTER, CASE STATMENT IS A MILE LONG, Condense (Dictionary?)
    enum DroppedItem { SINGLE_FIRE, RAPID_FIRE, CANNON, SHORT, MEDIUM, LONG, STANDARD, SLOW, INCENDIARY }

    [SerializeField] Sprite[] topPiece;
    [SerializeField] Sprite[] basePiece;
    [SerializeField] Sprite[] ammoPiece;

    SpriteRenderer sr;
    
    int topDropChance;
    int baseDropChance;
    int ammoDropChance;

    PiecesCollectedManager.TowerPieceEnum itemType;

    // Event for HUI_CUI updating
    ItemCollectedEvent itemCollectedEvent;
    public void AddItemCollectedListener(UnityAction listener)
    {
        itemCollectedEvent.AddListener(listener);
    }
    // Start is called before the first frame update
    void Start()
    {
        // Event for HUI_CUI updating
        itemCollectedEvent = new ItemCollectedEvent();
        EventManager.AddItemCollectedInvoker(this);

        
        sr = GetComponent<SpriteRenderer>();
        PickPieceToDrop();
        Pulse();
    }

    void PickPieceToDrop()
    {
        topDropChance = Random.Range(0, GameplayManager.Instance.TopPieceMaxRange);
        baseDropChance = Random.Range(0, GameplayManager.Instance.BasePieceMaxRange);
        ammoDropChance = Random.Range(0, GameplayManager.Instance.AmmoPieceMaxRange);

        if (topDropChance > baseDropChance)
        {
            if (topDropChance > ammoDropChance)
            {
                // top is highest
                GameplayManager.Instance.TopPieceMaxRange -= 2;
                DropTopPiece();
            }
            else
            {
                // ammoIsHighest
                GameplayManager.Instance.AmmoPieceMaxRange -= 2;
                DropAmmoPiece();

            }
        }
        else if (baseDropChance > ammoDropChance)
        {
            // base is highest
            GameplayManager.Instance.BasePieceMaxRange -= 2;
            DropBasePiece();
        }
        else
        {
            // ammo is highest
            GameplayManager.Instance.AmmoPieceMaxRange -= 2;
            DropAmmoPiece();
        }

    }
    // Just used to call the pulsing animation
    public void Pulse()
    { }

    void DropTopPiece()
    {
        int itemToDrop = Random.Range(0, 3);
        sr.sprite = topPiece[itemToDrop];
        GameplayManager.Instance.TopDropped = true;
        switch (itemToDrop)
        {
            case 0:
                {
                    itemType = PiecesCollectedManager.TowerPieceEnum.SINGLEFIRE_TOP;
                    break;
                }
            case 1:
                {
                    itemType = PiecesCollectedManager.TowerPieceEnum.RAPIDFIRE_TOP;
                    break;
                }
            case 2:
                {
                    itemType = PiecesCollectedManager.TowerPieceEnum.CANNON_TOP;
                    break;
                }
        }
    }

    void DropBasePiece()
    {
        GameplayManager.Instance.BotDropped = true;
        int itemToDrop = Random.Range(0, 3);
        sr.sprite = basePiece[itemToDrop];

        switch (itemToDrop)
        {
            case 0:
                {
                    itemType = PiecesCollectedManager.TowerPieceEnum.SHORTRANGE_BASE;
                    break;
                }
            case 1:
                {
                    itemType = PiecesCollectedManager.TowerPieceEnum.MIDRANGE_BASE;
                    break;
                }
            case 2:
                {
                    itemType = PiecesCollectedManager.TowerPieceEnum.LONGRANGE_BASE;
                    sr.color = Color.red;
                    break;
                }
        }
    }

    void DropAmmoPiece()
    {
        GameplayManager.Instance.AmmoDropped = true;
        int itemToDrop = Random.Range(0, 3);
        sr.sprite = ammoPiece[itemToDrop];

        switch (itemToDrop)
        {
            case 0:
                {
                    itemType = PiecesCollectedManager.TowerPieceEnum.KINETIC_AMMO;
                    break;
                }
            case 1:
                {
                    itemType = PiecesCollectedManager.TowerPieceEnum.CRYO_AMMO;
                    break;
                }
            case 2:
                {
                    itemType = PiecesCollectedManager.TowerPieceEnum.INCENDIARY_AMMO;
                    break;
                }
        }
    }

    /// <summary>
    /// Collects the Item on Mouse Enter, and Invokes the event
    /// </summary>
    private void OnMouseEnter()
    {
        if (!GameplayManager.Instance.IsPaused)
        {
            // Increments the PCM
            PiecesCollectedManager.Instance.CollectedPieces[itemType]++;
            itemCollectedEvent.Invoke();
            AudioManager.Instance.PlaySFX(AudioManager.Sounds.ITEM_PICKUP);
            Destroy(gameObject);
        }
    }
}
