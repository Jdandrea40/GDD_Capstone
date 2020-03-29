using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is repsonsible for updating
/// all HUD text located at the top of the screen
/// </summary>
public class HUD_GameStats : MonoBehaviour
{   
    [SerializeField] Text scrapCollectedText;
    [SerializeField] Text waveCountText;
    [SerializeField] Text baseHealthText;

    
    // Start is called before the first frame update
    void Start()
    {       
        scrapCollectedText.text = " Scrap Collected: " + GameplayManager.Instance.ScrapCollected;
        baseHealthText.text = " Base Health: " + GameplayManager.Instance.BaseHealth;
        waveCountText.text = " Wave: " + GameplayManager.Instance.CurrWaveText + " : " + GameplayManager.Instance.MaxWaveCount;
    }

    // Update is called once per frame
    void Update()
    {
        scrapCollectedText.text = " Scrap Collected: " + GameplayManager.Instance.ScrapCollected;
        baseHealthText.text = " Base Health: " + GameplayManager.Instance.BaseHealth;
        waveCountText.text = " Wave: " + GameplayManager.Instance.CurrWaveText + " : " + GameplayManager.Instance.MaxWaveCount;
    }

}
