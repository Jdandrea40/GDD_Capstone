using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    BoxCollider2D bc2d;
    GameObject target;
    SpriteRenderer sr;

    float projectileMoveSpeed;

    private void Awake()
    {
        bc2d = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();

        projectileMoveSpeed = ConstantsManager.Instance.PROJECTILE_MOVE_SPEED;       
    }
    // Update is called once per frame
    void Update()
    {
        // Gets the first enemy in the list and stores it
        EventManager.TowerFireListener(MoveToEnemy);

    }

    void MoveToEnemy(Transform target)
    {
        // moves the projectile towards the saved enemy
        transform.position = Vector2.MoveTowards(transform.position,
           target.transform.position, projectileMoveSpeed * Time.deltaTime);
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
