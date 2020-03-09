using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemDrop : MonoBehaviour
{
    enum DroppedItem { SINGLE_FIRE, RAPID_FIRE, CANNON, SHORT, MEDIUM, LONG, STANDARD, SLOW, INCENDIARY }

    // An array of items to drop sprites
    [SerializeField] Sprite[] item = new Sprite[9];

    SpriteRenderer sr;
    int itemToDrop;

    PiecesCollectedManager.TowerPieceEnum itemType;

    ItemCollectedEvent itemCollectedEvent;
    public void AddItemCollectedListener(UnityAction listener)
    {
        itemCollectedEvent.AddListener(listener);
    }
    // Start is called before the first frame update
    void Start()
    {
        itemCollectedEvent = new ItemCollectedEvent();
        EventManager.AddItemCollectedInvoker(this);

        sr = GetComponent<SpriteRenderer>();
        itemToDrop = Random.Range(0, 9);
        DropItem(itemToDrop);
        Pulse();
    }

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
    public void Pulse()
    {

    }
    private void OnMouseEnter()
    {
        PiecesCollectedManager.Instance.CollectedPieces[itemType]++;
        itemCollectedEvent.Invoke();
        Destroy(gameObject);
    }
}
