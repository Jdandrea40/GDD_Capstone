using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Overarching Enemy class
/// </summary>
public class Enemy : MonoBehaviour
{
    CircleCollider2D cc2d;
    protected SpriteRenderer sr;

    [SerializeField] Image healthBar;
    // This is hat the enemy drops on death
    [SerializeField] protected GameObject item;

    // Was being used for dictionary target acquisition
    int instanceID;

    #region ENEMY STATS

    protected ENEMY_STATS eStat;
    protected float Health = 1;
    float fullHealth;
    protected float moveSpeed;

    int damage;
    protected bool TakingDamage = false;
    protected bool Slowed = false;
    EnemyMoveTowardsPoint enemyMove;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float HealthBarHealth { get => Health; }
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
        // Gets the mov script to disable during SLOW
        enemyMove = GetComponent<EnemyMoveTowardsPoint>();
        
        // Adds itself to the overall in game enemy list
        // being used to to determine if the level has been won
        GameplayManager.Instance.SpawnedEnemies.Add(gameObject);

        // Will Increase the base Health PLus the Modifier (EHM += CurrWave)
        Health += GameplayManager.Instance.EnemyHealthModifier;
        fullHealth = Health;

        #region UNUSED STUFF
        //Debug.Log(Health);
        //addEnemyTarget = new AddEnemyTargetEvent();
        //EventManager.AddEnemyTargetInvoker(this);
        //removeEnemyTarget = new RemoveEnemyTargetEvent();
        //EventManager.RemoveEnemyTargetInvoker(this);

        #endregion

    }

    public virtual void Update()
    {
        if (Health > 0)
        {
            healthBar.fillAmount = Health / fullHealth;
        }
            // Life Checking
        if (Health <= 0)
        {
            if (Random.Range(0, 5) > 1)
            {
                // Spawns collectable item
                Instantiate(item, transform.position, Quaternion.identity);
            }
            
            GameplayManager.Instance.EnemiesKilled++;
            GameplayManager.Instance.SpawnedEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Collision Checking
    /// </summary>
    /// <param name="collision"></param>
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // Used to take damage from projectile
        if (collision.gameObject.layer == (int)CollisionLayers.PROJECTILE)
        {
            // Gets the stats from the Projectile inorder to take proper damage
            Projectile proj = collision.gameObject.GetComponent<Projectile>();
            TakeDamage(proj.ProjDamage, proj.ProjDoT, proj.ProjDotAmount, proj.ProjSlow);
        }
        // Removes itself from the the "In=Play" list (GAMEMANAGER)
        if (collision.gameObject.layer == (int)CollisionLayers.HOME_BASE)
        {
            GameplayManager.Instance.SpawnedEnemies.Remove(gameObject);
        }
    }
    #endregion

    #region CUSTOM METHODS

    /// <summary>
    /// Damages the enemy
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="dot"></param>
    /// <param name="dotAmount"></param>
    /// <param name="slow"></param>
    public virtual void TakeDamage(int amount, bool dot, int dotAmount, bool slow)
    {        
        Health -= amount;
        if (dot && !TakingDamage)
        {
            TakingDamage = true;
            StartCoroutine(TakeDamageOverTime(dotAmount));
        }
        if (slow && !Slowed)
        {
            Slowed = true;
            StartCoroutine(SlowEnemy());
            
        }
    }

    /// <summary>
    /// Takes Damage Over Time
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public virtual IEnumerator TakeDamageOverTime(int amount)
    {
        
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

    // Slows enemy
    public virtual IEnumerator SlowEnemy()
    {
        sr.color = Color.blue;
        moveSpeed *= .5f;
        //enemyMove.enabled = false;
        yield return new WaitForSeconds(2);
        Slowed = false;
        moveSpeed /= .5f;
        //enemyMove.enabled = true;
        sr.color = Color.white;

    }
    #endregion
}
