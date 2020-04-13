using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_SellUpgradePanel : MonoBehaviour
{
    [SerializeField] GameObject sellPanel;
    [SerializeField] Image mainPanelImage;
    [SerializeField] Button sellButton;

    // Start is called before the first frame update
    void Start()
    {
        mainPanelImage.raycastTarget = false;
        EventManager.ActivateTowerPanelListener(OpenPanel);

    }

    /// <summary>
    /// Sells the tower and reverts all necessary BA statuses
    /// </summary>
    public void SellTower()
    {
        if (GameplayManager.Instance.TowerToSell != null && !GameplayManager.Instance.IsPaused)
        {
            // Sets the Buildable area back to a normal Unoccupiead Area
            // bc2d needs to be reneabled due to it being disabled to avoid double triggering of
            // ba and tower
            BuildableArea ba = GameplayManager.Instance.TowerToSell.GetComponentInParent<BuildableArea>();
            ba.Occupied = false;
            ba.bc2d.enabled = true;

            Destroy(GameplayManager.Instance.TowerToSell);
            CloseMenu();
        }
    }

    IEnumerator ButtonBuffer()
    {
        yield return new WaitForSeconds(.5f);
        sellButton.interactable = true;
    }
    public void CloseMenu()
    {
        if (!GameplayManager.Instance.IsPaused)
        {
            mainPanelImage.raycastTarget = false;
            sellPanel.SetActive(false);
            GameplayManager.Instance.TowerToSell = null;
        }
    }

    void OpenPanel()
    {
        sellButton.interactable = false;
        StartCoroutine(ButtonBuffer());
        mainPanelImage.raycastTarget = true;
        sellPanel.SetActive(true);
    }
}
