using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class HUD_BuyPanel : MonoBehaviour
{
    [SerializeField] Text[] pieceCountText;

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
        UpdateCountText();
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
            // HUD_CraftingUI = Listener, Invokes the UpdateItemCount() method
            itemBoughtEvent.Invoke();
            UpdateCountText();
            AudioManager.Instance.PlaySFX(AudioManager.Sounds.ITEM_PICKUP);
        }
        else
        {
            //TODO: ERROR SOUND / Inssufucient Funds
        }
        
    }

    public void UpdateCountText()
    {
        for (int i = 0; i < pieceCountText.Length; i++)
        {
            pieceCountText[i].text = PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)i].ToString();
            if (PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)i] < 1)
            {
                pieceCountText[i].color = Color.red;
            }
            else
            {
                pieceCountText[i].color = Color.white;
            }
        }
    }
}
