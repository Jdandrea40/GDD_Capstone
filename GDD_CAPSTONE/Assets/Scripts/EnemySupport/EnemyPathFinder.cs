using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// https://www.youtube.com/watch?v=aFxucZQ_5E4&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0&index=3&t=0s
/// Used for pathfinding
/// Attach to in Scene waypoint GameObject
/// </summary>
public class EnemyPathFinder : MonoBehaviour
{
    public Transform[] points;

    /// <summary>
    /// Finds all POI the enemy must move to
    /// </summary>
    private void Awake()
    {
        points = FindSpawnSide();
        Debug.Log(points.Length);
        for (int i = 0; i < points.Length - 1; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
    
    Transform[] FindSpawnSide()
    {
        float shortestDist = Mathf.Infinity;
        GameObject[] ePaths = GameObject.FindGameObjectsWithTag("EnemyPath");
        GameObject pathToPick = ePaths[0];
        Transform[] points;

        foreach (GameObject path in ePaths)
        {
            float distance = Vector2.Distance(path.transform.position, gameObject.transform.position);
            if (distance < shortestDist)
            {
                shortestDist = distance;
                pathToPick = path;
            }
        }
        
        points = pathToPick.GetComponentsInChildren<Transform>();

        return points;
    }
}

