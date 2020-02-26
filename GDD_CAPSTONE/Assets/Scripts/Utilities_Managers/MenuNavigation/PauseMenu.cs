﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        GameplayManager.Instance.IsPause = true;
    }

    public void Resume()
    {
        ;
        GameplayManager.Instance.IsPause = false;
        Time.timeScale = 1;
        Destroy(gameObject);
    }

}