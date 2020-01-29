using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Overarching Enemy class
/// </summary>
public class Enemy : MonoBehaviour
{
    CircleCollider2D cc2d;

    #region ENEMY PROPERTIES

    protected int Health;
    protected float MoveSpeed;
    protected int Damage;

    #endregion

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

    #region UNITY METHODS

    private void Awake()
    {
        cc2d = GetComponent<CircleCollider2D>();
        MoveSpeed = ConstantsManager.Instance.ENEMY_MOVE_SPEED;

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

    #endregion

    #region CUSTOM METHODS

    protected void TakeDamage(int amount)
    {

    }

    #endregion
}
