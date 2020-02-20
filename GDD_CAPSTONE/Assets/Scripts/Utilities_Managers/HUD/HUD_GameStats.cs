using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_GameStats : MonoBehaviour
{
    HUDManager hud;
    
    // Start is called before the first frame update
    void Start()
    {
        hud = HUDManager.Instance;
        
        hud.EnemiesKilledText.text = " Enemies Killed: " + GameplayManager.EnemiesKilled;
        hud.BaseHealthText.text = " Base Health: " + GameplayManager.BaseHealth;
        hud.WaveCountText.text = " Wave: " + GameplayManager.CurWaveCount + " / " + GameplayManager.MaxWaveCount;
    }

    // Update is called once per frame
    void Update()
    {
        hud.EnemiesKilledText.text = " Enemies Killed: " + GameplayManager.EnemiesKilled;
        hud.BaseHealthText.text = " Base Health: " + GameplayManager.BaseHealth;
        hud.WaveCountText.text = " Wave: " + GameplayManager.CurWaveCount + " / " + GameplayManager.MaxWaveCount;
    }
}
