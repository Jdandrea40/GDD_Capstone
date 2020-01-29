using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Tower
{
    BoxCollider2D bc2d;
    Transform targetToHit;
    SpriteRenderer sr;

    float projectileMoveSpeed;

    private void Awake()
    {
        bc2d = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        targetToHit = target;
        projectileMoveSpeed = ConstantsManager.Instance.PROJECTILE_MOVE_SPEED;       
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Debug.Log(enemyTargets.Count);
        // moves the projectile towards the saved enemy
        transform.position = Vector2.MoveTowards(transform.position,
           targetToHit.transform.position, projectileMoveSpeed * Time.deltaTime);
        ;
        // Gets the first enemy in the list and stores it
        //EventManager.TowerFireListener(MoveToEnemy);
    }

    void MoveToEnemy(Transform target)
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == (int)CollisionLayers.ENEMIES)
        {
            Destroy(gameObject);
            Debug.Log("Hit");
        }
    }
}
