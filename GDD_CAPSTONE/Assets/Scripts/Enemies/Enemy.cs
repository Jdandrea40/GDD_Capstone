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

    bool hasTriggered = false;
    public bool HasTriggered
    {
        get { return hasTriggered; }
    }

    #region ENEMY STATS

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == (int)CollisionLayers.TOWER)
        {
            hasTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == (int)CollisionLayers.TOWER)
        {
            hasTriggered = false;
        }
    }

    #endregion

    #region CUSTOM METHODS
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == (int)CollisionLayers.PROJECTILE)
        {
            TakeDamage(1);
        }
    }

    protected virtual void TakeDamage(int amount)
    {

    }

    #endregion
}
