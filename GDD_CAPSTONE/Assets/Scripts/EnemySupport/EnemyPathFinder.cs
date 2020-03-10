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
    public static Transform[] points;

    /// <summary>
    /// Finds all POI the enemy must move to
    /// </summary>
    private void Awake()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}

