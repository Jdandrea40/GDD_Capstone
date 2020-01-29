using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// https://www.youtube.com/watch?v=aFxucZQ_5E4&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0&index=3&t=0s
/// Used for pathfinding
/// </summary>
public class EnemyMoveTowardsPoint : Enemy
{
    int currentPoint = 0;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the target to the first point in the array
        target = EnemyPathFinder.points[0];
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // Moves the enemy
    void Move()
    {
        // Gets the distance of the current point of travel
        Vector3 dir = target.position - transform.position;
        // moves the enemy
        transform.Translate(dir.normalized * MoveSpeed * Time.deltaTime, Space.World);

        // detects if it has reach its destination (with buffer)
        if (Vector3.Distance(transform.position, target.position) <= .2f)
        {
            // increments the waypoint
            GetNextWaypoint();
        }
        //Debug.Log("GO " + gameObject.transform.position + " MT: " + movePoints[currentPoint].position + " CP " + currentPoint);
    }
    
    // Used to increment waypoint and rotate based on its position
    void GetNextWaypoint()
    {
        if (currentPoint >= EnemyPathFinder.points.Length - 1)
        {
            Destroy(gameObject);
            Debug.Log("Boom");
            return;
        }
        // Increment and set
        currentPoint++;
        target = EnemyPathFinder.points[currentPoint];

        // Rotates
        Vector2 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
    }
}
