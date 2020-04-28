using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu_Details : MonoBehaviour
{
    [SerializeField] GameObject[] itemPanels;
    [SerializeField] Button[] itemButtons;
    // Start is called before the first frame update
    void Start()
    {
        itemPanels[0].SetActive(true);
        itemPanels[1].SetActive(false);
        itemPanels[2].SetActive(false);

        itemButtons[0].interactable = false;
        itemButtons[1].interactable = true;
        itemButtons[2].interactable = true;

    }

    public void PanelSelected(int panelNum)
    {
        AudioManager.Instance.PlayButtonClick(AudioManager.Sounds.BUTTON_CLICK);
        for (int i = 0; i < itemPanels.Length; i++)
        {
            if (i == panelNum)
            {
                itemPanels[i].SetActive(true);
                itemButtons[i].interactable = false;
            }
            else
            {
                itemPanels[i].SetActive(false);
                itemButtons[i].interactable = true;
            }
        }
    }
}
