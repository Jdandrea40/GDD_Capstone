using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    BoxCollider2D bc2d;
    
    Transform targetToHit;
     
    SpriteRenderer sr;

    float projectileMoveSpeed;

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
        if (targetToHit != null)
        {
            Debug.Log(targetToHit.name);
            // moves the projectile towards the saved enemy
            transform.position = Vector2.MoveTowards(transform.position,
              targetToHit.position, projectileMoveSpeed * Time.deltaTime);
            // Gets the first enemy in the list and stores it
            //EventManager.TowerFireListener(MoveToEnemy);
        }
        else
        {
            Debug.Log("NOTHING");
        }
    }

    public void MoveToEnemy(GameObject target)
    {
        targetToHit = target.transform;
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
