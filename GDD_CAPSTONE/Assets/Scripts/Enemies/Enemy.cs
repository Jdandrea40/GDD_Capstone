﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Overarching Enemy class
/// </summary>
public class Enemy : MonoBehaviour
{
    CircleCollider2D cc2d;
    SpriteRenderer sr;

    // This is hat the enemy drops on death
    [SerializeField] GameObject item;

    // Was being used for dictionary target acquisition
    int instanceID;

    bool hasTriggered = false;
    //public bool HasTriggered
    //{
    //    get { return hasTriggered; }
    //}

    #region ENEMY STATS

    protected int Health = 100;
    float moveSpeed;
    int damage;
    bool takingDamage = false;
    bool slowed = false;

    #endregion

    #region EVENTS

 
    //AddEnemyTargetEvent addEnemyTarget;
    //public void AddEnemyTargetListener(UnityAction<int, GameObject> listener)
    //{
    //    addEnemyTarget.AddListener(listener);
    //}

    //protected RemoveEnemyTargetEvent removeEnemyTarget;
    //public void RemoveEnemyTargetListener(UnityAction<int, GameObject> listener)
    //{
    //    removeEnemyTarget.AddListener(listener);
    //}

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        cc2d = GetComponent<CircleCollider2D>();
        moveSpeed = ConstantsManager.Instance.ENEMY_MOVE_SPEED;
        instanceID = gameObject.GetInstanceID();
        sr.GetComponent<SpriteRenderer>();
        EventManager.AddEnemyDamageListener(TakeDamage);

    }
    // Start is called before the first frame update
    void Start()
    {
        

        //addEnemyTarget = new AddEnemyTargetEvent();
        //EventManager.AddEnemyTargetInvoker(this);
        //removeEnemyTarget = new RemoveEnemyTargetEvent();
        //EventManager.RemoveEnemyTargetInvoker(this);

        // Adds itself to the overall in game enemy list
        // being used to to determine if the level has been won
        GameplayManager.SpawnEnemies.Add(gameObject);


    }
    private void Update()
    {
        if (Health <= 0)
        {
            Instantiate(item, transform.position, Quaternion.identity);
            GameplayManager.EnemiesKilled++;
            GameplayManager.SpawnEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.layer == (int)CollisionLayers.TOWER)
    //    {
    //        addEnemyTarget.Invoke(instanceID, gameObject);
    //        hasTriggered = true;     
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if(collision.gameObject.layer == (int)CollisionLayers.TOWER)
    //    { 
    //        removeEnemyTarget.Invoke(instanceID, gameObject);
    //        hasTriggered = false;
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == (int)CollisionLayers.PROJECTILE)
        {
            //TakeDamage(1);
        }
        if (collision.gameObject.layer == (int)CollisionLayers.HOME_BASE)
        {
            GameplayManager.SpawnEnemies.Remove(gameObject);
        }
    }
    #endregion

    #region CUSTOM METHODS


    void TakeDamage(int amount, bool dot, int dotAmount, bool slow)
    {
        Health -= amount;
        if (dot)
        {
            StartCoroutine(TakeDamageOverTime(dotAmount));
        }
        if (slow && !slowed)
        {
            StartCoroutine(SlowEnemy());
            slowed = true;
        }

    }


    IEnumerator TakeDamageOverTime(int amount)
    {
        takingDamage = true;
        GetComponent<SpriteRenderer>().color = Color.red;
        while (takingDamage)
        {
            Health -= amount;
            yield return new WaitForSeconds(1);
            Health -= amount;
            yield return new WaitForSeconds(1);
            Health -= amount;

            takingDamage = false;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    IEnumerator SlowEnemy()
    {
        sr.color = Color.blue;
        moveSpeed /= .5f;
        yield return new WaitForSeconds(2);
        slowed = false;
        sr.color = Color.white;

    }
    #endregion
}
