using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemDrop : MonoBehaviour
{
    // TODO: DO THIS BETTER, CASE STATMENT IS A MILE LONG, Condense (Dictionary?)
    enum DroppedItem { SINGLE_FIRE, RAPID_FIRE, CANNON, SHORT, MEDIUM, LONG, STANDARD, SLOW, INCENDIARY }

    // An array of items to drop sprites
    [SerializeField] Sprite[] item = new Sprite[9];

    SpriteRenderer sr;
    int itemToDrop;

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
        Pulse();
        sr = GetComponent<SpriteRenderer>();
        itemToDrop = Random.Range(0, 9);
        DropItem(itemToDrop);
    }

    // Just used to call the pulsing animation
    public void Pulse()
    { }

    void DropItem(int itemToDrop)
    {
        sr.sprite = item[itemToDrop];

        switch(itemToDrop)
        {
            case (int)DroppedItem.SINGLE_FIRE:
                {
                    itemType = PiecesCollectedManager.TowerPieceEnum.SINGLEFIRE_TOP;
                    break;
                }
            case (int)DroppedItem.RAPID_FIRE:
                {
                    itemType = PiecesCollectedManager.TowerPieceEnum.RAPIDFIRE_TOP;
                    break;
                }
            case (int)DroppedItem.CANNON:
                {
                    itemType = PiecesCollectedManager.TowerPieceEnum.CANNON_TOP;
                    break;
                }
            case (int)DroppedItem.SHORT:
                {
                    itemType = PiecesCollectedManager.TowerPieceEnum.SHORTRANGE_BASE;
                    sr.color = Color.red;
                    break;
                }
            case (int)DroppedItem.MEDIUM:
                {
                    itemType = PiecesCollectedManager.TowerPieceEnum.MIDRANGE_BASE;
                    sr.color = Color.yellow;
                    break;
                }
            case (int)DroppedItem.LONG:
                {
                    itemType = PiecesCollectedManager.TowerPieceEnum.LONGRANGE_BASE;
                    sr.color = Color.green;
                    break;
                }
            case (int)DroppedItem.STANDARD:
                {
                    itemType = PiecesCollectedManager.TowerPieceEnum.KINETIC_AMMO;
                    break;
                }
            case (int)DroppedItem.SLOW:
                {
                    itemType = PiecesCollectedManager.TowerPieceEnum.CRYO_AMMO;
                    sr.color = Color.blue;
                    break;
                }
            case (int)DroppedItem.INCENDIARY:
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
