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
    [SerializeField] GameObject item;
    protected int instanceID;
    bool hasTriggered = false;
    //public bool HasTriggered
    //{
    //    get { return hasTriggered; }
    //}

    #region ENEMY STATS

    protected int Health = 2;
    protected float MoveSpeed;
    protected int Damage;

    #endregion

    #region EVENTS

 
    AddEnemyTargetEvent addEnemyTarget;
    public void AddEnemyTargetListener(UnityAction<int, GameObject> listener)
    {
        addEnemyTarget.AddListener(listener);
    }

    protected RemoveEnemyTargetEvent removeEnemyTarget;
    public void RemoveEnemyTargetListener(UnityAction<int, GameObject> listener)
    {
        removeEnemyTarget.AddListener(listener);
    }

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        cc2d = GetComponent<CircleCollider2D>();
        MoveSpeed = ConstantsManager.Instance.ENEMY_MOVE_SPEED;
        instanceID = gameObject.GetInstanceID();

    }
    // Start is called before the first frame update
    void Start()
    {
        addEnemyTarget = new AddEnemyTargetEvent();
        EventManager.AddEnemyTargetInvoker(this);
        removeEnemyTarget = new RemoveEnemyTargetEvent();
        EventManager.RemoveEnemyTargetInvoker(this);

        
    }
    private void Update()
    {
        if (Health <= 0)
        {
            Instantiate(item, transform.position, Quaternion.identity);
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
            TakeDamage(1);
        }
    }
    #endregion

    #region CUSTOM METHODS


    void TakeDamage(int amount)
    {
        Health -= amount;

    }

    #endregion
}
