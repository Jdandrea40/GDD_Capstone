﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(Explode());
        
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }
}
