using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScene : MonoBehaviour
{
    public void BackToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
        AudioManager.Instance.PlayButtonClick(AudioManager.Sounds.BUTTON_CLICK);
    }
}
