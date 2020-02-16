using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemDrop : MonoBehaviour
{
    enum DroppedItem { STANDARD, RAPID_FIRE, CANNON }

    [Tooltip("Standard, Rapid, Cannon")]
    [SerializeField] Sprite[] item = new Sprite[3];

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
        itemToDrop = Random.Range(0, 3);
        DropItem(itemToDrop);
    }

    void DropItem(int itemToDrop)
    {
        sr.sprite = item[itemToDrop];
        switch(itemToDrop)
        {
            case (int)DroppedItem.STANDARD:
                {
                    itemType = DroppedItem.STANDARD;
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
        }
    }

    private void OnMouseDown()
    {
        switch (itemType)
        {
            case DroppedItem.STANDARD:
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
        }
        itemCollectedEvent.Invoke((int)itemType);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
