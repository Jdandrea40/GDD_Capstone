using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemDrop : MonoBehaviour
{
    enum DroppedItem { SINGLE_FIRE, RAPID_FIRE, CANNON, SHORT, MEDIUM, LONG, STANDARD, SLOW, INCENDIARY }

    [Tooltip("Standard, Rapid, Cannon")]
    [SerializeField] Sprite[] item = new Sprite[9];

    SpriteRenderer sr;
    int itemToDrop;
    DroppedItem itemType;

    ItemCollectedEvent itemCollectedEvent;
    public void AddItemCollectedListener(UnityAction<int> listener)
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
    }

    void DropItem(int itemToDrop)
    {
        sr.sprite = item[itemToDrop];
        switch(itemToDrop)
        {
            case (int)DroppedItem.SINGLE_FIRE:
                {
                    itemType = DroppedItem.SINGLE_FIRE;
                    break;
                }
            case (int)DroppedItem.RAPID_FIRE:
                {
                    itemType = DroppedItem.RAPID_FIRE;
                    break;
                }
            case (int)DroppedItem.CANNON:
                {
                    itemType = DroppedItem.CANNON;
                    break;
                }
            case (int)DroppedItem.SHORT:
                {
                    itemType = DroppedItem.SHORT;
                    sr.color = Color.red;
                    break;
                }
            case (int)DroppedItem.MEDIUM:
                {
                    itemType = DroppedItem.MEDIUM;
                    sr.color = Color.yellow;
                    break;
                }
            case (int)DroppedItem.LONG:
                {
                    itemType = DroppedItem.LONG;
                    sr.color = Color.green;
                    break;
                }
            case (int)DroppedItem.STANDARD:
                {
                    itemType = DroppedItem.STANDARD;
                    break;
                }
            case (int)DroppedItem.SLOW:
                {
                    itemType = DroppedItem.SLOW;
                    sr.color = Color.blue;
                    break;
                }
            case (int)DroppedItem.INCENDIARY:
                {
                    itemType = DroppedItem.INCENDIARY;
                    break;
                }
        }
    }

    private void OnMouseDown()
    {
        switch (itemType)
        {
            case DroppedItem.SINGLE_FIRE:
                {
                    PiecesCollectedManager.Instance.standardTurretTop++;
                    break;
                }
            case DroppedItem.RAPID_FIRE:
                {
                    PiecesCollectedManager.Instance.rapidFireTop++;
                    break;
                }
            case DroppedItem.CANNON:
                {
                    PiecesCollectedManager.Instance.rocketTop++;
                    break;
                }
            case DroppedItem.SHORT:
                {
                    PiecesCollectedManager.Instance.shortRangeBase++;
                    break;
                }
            case DroppedItem.MEDIUM:
                {
                    PiecesCollectedManager.Instance.mediumRangeBase++;
                    break;
                }
            case DroppedItem.LONG:
                {
                    PiecesCollectedManager.Instance.longRangeBase++;
                    break;
                }
            case DroppedItem.STANDARD:
                {
                    PiecesCollectedManager.Instance.standardAmmo++;
                    break;
                }
            case DroppedItem.SLOW:
                {
                    PiecesCollectedManager.Instance.slowAmmo++;
                    break;
                }
            case DroppedItem.INCENDIARY:
                {
                    PiecesCollectedManager.Instance.fireAmmo++;
                    break;
                }
        }
        itemCollectedEvent.Invoke((int)itemType);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
