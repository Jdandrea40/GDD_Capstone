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
    [SerializeField] Text enemiesKilledText;
    [SerializeField] Text waveCountText;
    [SerializeField] Text baseHealthText;
    
    // Start is called before the first frame update
    void Start()
    {       
        enemiesKilledText.text = " Enemies Killed: " + GameplayManager.Instance.EnemiesKilled;
        baseHealthText.text = " Base Health: " + GameplayManager.Instance.BaseHealth;
        waveCountText.text = " Wave: " + GameplayManager.Instance.CurWaveCount + " : " + GameplayManager.Instance.MaxWaveCount;
    }

    // Update is called once per frame
    void Update()
    {
        enemiesKilledText.text = " Enemies Killed: " + GameplayManager.Instance.EnemiesKilled;
        baseHealthText.text = " Base Health: " + GameplayManager.Instance.BaseHealth;
        waveCountText.text = " Wave: " + GameplayManager.Instance.CurWaveCount + " : " + GameplayManager.Instance.MaxWaveCount;
    }
}
