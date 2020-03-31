using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_SellUpgradePanel : MonoBehaviour
{
    [SerializeField] GameObject sellPanel;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.ActivateTowerPanelListener(OpenPanel);
    }

    /// <summary>
    /// Sells the tower and reverts all necessary BA statuses
    /// </summary>
    public void SellTower()
    {
        if (GameplayManager.Instance.TowerToSell != null)
        {
            // Sets the Buildable area back to a normal Unoccupiead Area
            // bc2d needs to be reneabled due to it being disabled to avoid double triggering of
            // ba and tower
            BuildableArea ba = GameplayManager.Instance.TowerToSell.GetComponentInParent<BuildableArea>();
            ba.Occupied = false;
            ba.bc2d.enabled = true;

            Destroy(GameplayManager.Instance.TowerToSell);
            sellPanel.SetActive(false);
            GameplayManager.Instance.TowerToSell = null;
        }
    }
    public void CloseMenu()
    {
        sellPanel.SetActive(false);
        GameplayManager.Instance.TowerToSell = null;
    }

    void OpenPanel()
    {
        sellPanel.SetActive(true);
    }
}
