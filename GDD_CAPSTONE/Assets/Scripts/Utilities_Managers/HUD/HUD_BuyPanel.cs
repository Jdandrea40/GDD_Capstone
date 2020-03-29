using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class HUD_BuyPanel : MonoBehaviour
{
    ItemCollectedEvent itemBoughtEvent;
    public void AddItemBoughtListener(UnityAction listener)
    {
        itemBoughtEvent.AddListener(listener);
    }
    // Start is called before the first frame update
    void Start()
    {
        itemBoughtEvent = new ItemCollectedEvent();
        EventManager.AddItemBoughtInvoker(this);
    }

    /// <summary>
    /// 0 = single fire
    /// 1 = rapid fire
    /// 2 = rocket
    /// 3 = short range
    /// 4 = med range
    /// 5 = long range
    /// 6 = armor pierce
    /// 7 = cryo
    /// 8 = incendiary
    /// </summary>
    /// <param name="towerToBuy"></param>
    public void Purchase(int towerToBuy)
    {
        if (GameplayManager.Instance.ScrapCollected >= 100)
        {
            GameplayManager.Instance.ScrapCollected -= 100;
            PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)towerToBuy]++;
            itemBoughtEvent.Invoke();
            AudioManager.Instance.PlaySFX(AudioManager.Sounds.ITEM_PICKUP);
        }
        else
        {
            //TODO: ERROR SOUND / Inssufucient Funds
        }
        
    }
}
