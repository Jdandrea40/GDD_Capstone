using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAcquisition : Tower
{
    Queue<GameObject> targets;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == (int)CollisionLayers.ENEMIES)
        {
            targets.Enqueue(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == (int)CollisionLayers.ENEMIES)
        {
            targets.Dequeue();
        }
    }
}
