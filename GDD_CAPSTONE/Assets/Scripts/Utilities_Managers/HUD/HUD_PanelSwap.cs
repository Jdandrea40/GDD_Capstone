using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_PanelSwap : MonoBehaviour
{
    [SerializeField] GameObject buildPanel;
    [SerializeField] GameObject buyPanel;
    // Start is called before the first frame update
    void Start()
    {
        buildPanel.SetActive(true);
        buyPanel.SetActive(false);
    }

    public void BuildActive()
    {
        buildPanel.SetActive(true);
        buyPanel.SetActive(false);
    }

    public void BuyActive()
    {
        buildPanel.SetActive(false);
        buyPanel.SetActive(true);
        buyPanel.GetComponent<HUD_BuyPanel>().UpdateCountText();
    }
}
