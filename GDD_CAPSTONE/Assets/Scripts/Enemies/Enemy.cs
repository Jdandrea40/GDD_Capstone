using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Overarching Enemy class
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    CircleCollider2D cc2d;

    #region EVENTS

    /// <summary>
    /// NOT CURRENTLY USING
    /// </summary>
    EnemyDequeueEvent enemyDequeue;
    public void AddEnemyDequeueListener(UnityAction listener)
    {
        enemyDequeue.AddListener(listener);
    }

    #endregion

    private void Awake()
    {
        cc2d = GetComponent<CircleCollider2D>();
        moveSpeed = ConstantsManager.Instance.ENEMY_MOVE_SPEED;

    }
    // Start is called before the first frame update
    void Start()
    {
        // Not currently using
        enemyDequeue = new EnemyDequeueEvent();
        EventManager.EnemyDequeueInvoker(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
