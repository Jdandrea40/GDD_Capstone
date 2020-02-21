using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_GameStats : MonoBehaviour
{   
    [SerializeField] Text enemiesKilledText;
    [SerializeField] Text waveCountText;
    [SerializeField] Text baseHealthText;
    
    // Start is called before the first frame update
    void Start()
    {       
        enemiesKilledText.text = " Enemies Killed: " + GameplayManager.EnemiesKilled;
        baseHealthText.text = " Base Health: " + GameplayManager.BaseHealth;
        waveCountText.text = " Wave: " + GameplayManager.CurWaveCount + " / " + GameplayManager.MaxWaveCount;
    }

    // Update is called once per frame
    void Update()
    {
        enemiesKilledText.text = " Enemies Killed: " + GameplayManager.EnemiesKilled;
        baseHealthText.text = " Base Health: " + GameplayManager.BaseHealth;
        waveCountText.text = " Wave: " + GameplayManager.CurWaveCount + " / " + GameplayManager.MaxWaveCount;
    }
}
