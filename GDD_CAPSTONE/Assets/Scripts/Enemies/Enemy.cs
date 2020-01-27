using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 2;
    CircleCollider2D cc2d;

    #region EVENTS

    EnemyDequeueEvent enemyDequeue;
    public void AddEnemyDequeueListener(UnityAction listener)
    {
        enemyDequeue.AddListener(listener);
    }

    #endregion
    private void Awake()
    {
        cc2d = GetComponent<CircleCollider2D>();

    }
    // Start is called before the first frame update
    void Start()
    {
        enemyDequeue = new EnemyDequeueEvent();
        EventManager.EnemyDequeueInvoker(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)CollisionLayers.TOWER)
        {
            enemyDequeue.Invoke();
        }
    }
}
