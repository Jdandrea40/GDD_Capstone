﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLoader : MonoBehaviour
{
    private void Awake()
    {
        AudioManager AM = AudioManager.Instance;
        ParticleEffectManager PM = ParticleEffectManager.Instance;

        if (AM != null && PM != null)
        {
            Destroy(gameObject);
        }
        //AM.PlayLoop(AudioManager.Sounds.BKG_LOOP);
    }

    // Update is called once per frame
    void Update()
    {
        AudioManager AM = AudioManager.Instance;
        ParticleEffectManager PM = ParticleEffectManager.Instance;

        if (AM != null && PM != null)
        {
            Destroy(gameObject);
        }
    }
}

