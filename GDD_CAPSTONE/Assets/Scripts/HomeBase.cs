using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBase : MonoBehaviour
{
    private void Start()
    {
        GameplayManager.Instance.BaseHealth = 100;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == (int)CollisionLayers.ENEMIES)
        {
            GameplayManager.Instance.BaseHealth -= 10;
        }
    }
}
