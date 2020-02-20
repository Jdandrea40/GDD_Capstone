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
    SpriteRenderer sr;

    // This is hat the enemy drops on death
    [SerializeField] protected GameObject item;

    // Was being used for dictionary target acquisition
    int instanceID;

    bool hasTriggered = false;
    //public bool HasTriggered
    //{
    //    get { return hasTriggered; }
    //}

    #region ENEMY STATS

    protected ENEMY_STATS eStat;
    protected int Health;
    protected float moveSpeed;

    int damage;
    protected bool TakingDamage = false;
    protected bool Slowed = false;
    EnemyMoveTowardsPoint enemyMove;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

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

    public virtual void Awake()
    {
        cc2d = GetComponent<CircleCollider2D>();
        eStat = ENEMY_STATS.Instance;
        //EventManager.AddEnemyDamageListener(TakeDamage);

    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        enemyMove = GetComponent<EnemyMoveTowardsPoint>();
        //Debug.Log(Health);
        //addEnemyTarget = new AddEnemyTargetEvent();
        //EventManager.AddEnemyTargetInvoker(this);
        //removeEnemyTarget = new RemoveEnemyTargetEvent();
        //EventManager.RemoveEnemyTargetInvoker(this);

        // Adds itself to the overall in game enemy list
        // being used to to determine if the level has been won
        GameplayManager.SpawnEnemies.Add(gameObject);
    }

    public virtual void Update()
    {

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

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == (int)CollisionLayers.PROJECTILE)
        {
            Projectile proj = collision.gameObject.GetComponent<Projectile>();
            TakeDamage(proj.ProjDamage, proj.ProjDoT, proj.ProjDotAmount, proj.ProjSlow);
        }
        if (collision.gameObject.layer == (int)CollisionLayers.HOME_BASE)
        {
            GameplayManager.SpawnEnemies.Remove(gameObject);
        }
    }
    #endregion

    #region CUSTOM METHODS


    public virtual void TakeDamage(int amount, bool dot, int dotAmount, bool slow)
    {

    }

    public virtual IEnumerator TakeDamageOverTime(int amount)
    {
        TakingDamage = true;
        sr.color = Color.red;
        while (TakingDamage)
        {
            Health -= amount;
            yield return new WaitForSeconds(1);
            Health -= amount;
            yield return new WaitForSeconds(1);
            Health -= amount;

            TakingDamage = false;
            sr.color = Color.white;
        }
    }

    public virtual IEnumerator SlowEnemy()
    {
        sr.color = Color.blue;
        enemyMove.enabled = false;
        yield return new WaitForSeconds(2);
        Slowed = false;
        enemyMove.enabled = true;
        sr.color = Color.white;

    }
    #endregion
}
