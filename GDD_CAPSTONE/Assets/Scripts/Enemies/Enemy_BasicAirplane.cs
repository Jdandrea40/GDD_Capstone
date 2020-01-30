using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BasicAirplane : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        Health = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        Health -= amount;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
