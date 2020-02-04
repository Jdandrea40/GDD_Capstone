using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Text enemiesKilledText;
    [SerializeField] Text baseHealthText;

    // Start is called before the first frame update
    void Start()
    {
        GameplayManager.EnemiesKilled = 0;
        enemiesKilledText.text = " Enemies Killed: " + GameplayManager.EnemiesKilled;
        baseHealthText.text = " Base Health: " + GameplayManager.BaseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        enemiesKilledText.text = " Enemies Killed: " + GameplayManager.EnemiesKilled;
        baseHealthText.text = " Base Health: " + GameplayManager.BaseHealth;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
