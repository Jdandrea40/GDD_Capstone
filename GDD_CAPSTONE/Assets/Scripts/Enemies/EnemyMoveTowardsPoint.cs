using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveTowardsPoint : Enemy
{
    [SerializeField] private Transform[] movePoints;
    //[SerializeField] GameObject test;
    int currentPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = movePoints[currentPoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (currentPoint <= movePoints.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                movePoints[currentPoint].transform.position, moveSpeed * Time.deltaTime);
            if(transform.position == movePoints[currentPoint].transform.position)
            {
                // Instantiate(test, transform.position, Quaternion.identity);
                currentPoint++;
                if (currentPoint == movePoints.Length)
                {
                    
                    Debug.Log("BOOM");
                    Destroy(gameObject);                   
                }
            }
        }

        //Debug.Log("GO " + gameObject.transform.position + " MT: " + movePoints[currentPoint].position + " CP " + currentPoint);
    }
}
