using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HealthBar : MonoBehaviour
{
    Enemy enemyHealth;
    int healthBarScale;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponentInParent<Enemy>();
        healthBarScale = enemyHealth.HealthBarHealth;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
