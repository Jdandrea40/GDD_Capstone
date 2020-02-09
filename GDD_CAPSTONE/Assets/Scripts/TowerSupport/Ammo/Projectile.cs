using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to handle to functionality of Projectiles
/// </summary>
public class Projectile : MonoBehaviour
{
    BoxCollider2D bc2d;
    
    Transform targetToHit;
     
    SpriteRenderer sr;

    float projectileMoveSpeed;

    // Called before first frame update
    private void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        //targetToHit = tower.TargetToShoot.transform;
        projectileMoveSpeed = ConstantsManager.Instance.PROJECTILE_MOVE_SPEED;       
    }
    // Update is called once per frame
    void Update()
    {
        // so long as there is a target to hit, go to it
        if (targetToHit != null)
        {
            // moves the projectile towards the saved enemy
            transform.position = Vector2.MoveTowards(transform.position,
              targetToHit.position, projectileMoveSpeed * Time.deltaTime);
            // Gets the first enemy in the list and stores it
            //EventManager.TowerFireListener(MoveToEnemy);
        }

        else
        {
            // if the target is detroyed before this proj hits
            // CASE: two towers are shooting at the same target
            Destroy(gameObject);
        }
    }

    // used to indetify the target to move towards
    public void MoveToEnemy(GameObject target)
    {
        targetToHit = target.transform;
    }

    // Collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == (int)CollisionLayers.ENEMIES)
        {
            Destroy(gameObject);
        }
    }
}
