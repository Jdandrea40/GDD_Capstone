using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAcquisition : Tower
{
    List<GameObject> targets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(targets.Count);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == (int)CollisionLayers.ENEMIES)
        {
            targets.Add(collision.gameObject);
        }
    }

    protected override void DequeueEnemy()
    {
        base.DequeueEnemy();
        //targets.Remove();
    }


}
