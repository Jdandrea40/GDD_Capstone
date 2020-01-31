using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Used to acquire/unacquire Enemy targets
/// </summary>
public class TargetAcquisition : Tower
{
   //List<GameObject> targets = new List<GameObject>();
  
    public float range;

    //private void Start()
    //{
    //    range = cc2d.radius;
    //    InvokeRepeating("UpdateTarget", 0f, .5f);
    //}

    //protected override void UpdateTarget()
    //{
    //    base.UpdateTarget();

    //    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    //    float shortestDistance = Mathf.Infinity;
    //    GameObject nearestEnemy = null;


    //    foreach (GameObject enemy in enemies)
    //    {
    //        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
    //        if (distanceToEnemy < shortestDistance)
    //        {
    //            shortestDistance = distanceToEnemy;
    //            nearestEnemy = enemy;

    //        }
    //    }

    //    if (nearestEnemy != null && shortestDistance <= range)
    //    {
    //        target = nearestEnemy.transform;

    //    }
    //}

    //private void Update()
    //{
    //    if (target == null)
    //    {
    //        return;
    //    }
    //}

    // Target acquired
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)CollisionLayers.ENEMIES)
        {
            //enemyTargets.Add(collision.gameObject);
        }
    }

    // Target unacquired
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)CollisionLayers.ENEMIES)
        {
            //enemyTargets.Remove(collision.gameObject);
        }
    }
}

