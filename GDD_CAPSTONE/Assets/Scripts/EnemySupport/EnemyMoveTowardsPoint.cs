using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// https://www.youtube.com/watch?v=aFxucZQ_5E4&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0&index=3&t=0s
/// Used for pathfinding
/// </summary>
public class EnemyMoveTowardsPoint : MonoBehaviour
{
    int currentPoint = 0;
    GameObject spawnPoint;
    Transform[] path;
    private Transform target;
    float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        FindSpawnSide();
        Debug.Log(spawnPoint.name);
        // Sets the target to the first point in the array
        path = spawnPoint.GetComponent<EnemyPathFinder>().points;
        Debug.Log(path.Length);
        target = path[0];
        moveSpeed = GetComponent<Enemy>().CurrentSpeed;
    }

    void FindSpawnSide()
    {
        float shortestDist = Mathf.Infinity;
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("EnemyPath");
        GameObject spawnerToPick = spawners[0];
        Debug.Log(spawners.Length);
        foreach (GameObject spawner in spawners)
        {
            float distance = Vector2.Distance(spawner.transform.position, gameObject.transform.position);
            if (distance < shortestDist)
            {
                shortestDist = distance;
                spawnerToPick = spawner;
            }
        }

        spawnPoint = spawnerToPick;
    }
    // Update is called once per frame
    void Update()
    {
        // returns out when paused
        if (!GameplayManager.Instance.IsPaused)
        {
            Move();
        }
    }

    // Moves the enemy
    void Move()
    {
        // Gets the distance of the current point of travel
        Vector3 dir = target.position - transform.position;
        // moves the enemy
        transform.Translate(dir.normalized * GetComponent<Enemy>().CurrentSpeed * Time.deltaTime, Space.World);

        // detects if it has reach its destination (with buffer)
        if (Vector3.Distance(transform.position, target.position) <= .2f)
        {
            // increments the waypoint
            GetNextWaypoint();
        }
    }

    /// <summary>
    ///  Used to increment waypoint and rotate based on its position
    /// </summary>
    void GetNextWaypoint()
    {
        // checks if its current point is the end
        if (currentPoint >= path.Length - 1)
        {  
            // KYS
            Destroy(gameObject);
            return;
        }

        // Increment and set
        currentPoint++;
        target = path[currentPoint];

        // Rotates the sprite to face to right direction
        Vector2 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
    }
}
