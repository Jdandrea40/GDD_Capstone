using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Text enemiesKilledText;

    // Start is called before the first frame update
    void Start()
    {
        enemiesKilledText.text = " Enemies Killed: " + GameplayManager.EnemiesKilled;
    }

    // Update is called once per frame
    void Update()
    {
        enemiesKilledText.text = " Enemies Killed: " + GameplayManager.EnemiesKilled;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
