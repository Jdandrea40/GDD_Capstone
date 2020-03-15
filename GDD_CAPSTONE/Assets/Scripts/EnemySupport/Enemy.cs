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

    // Enemy slow speed changin
    protected float moveSpeed;
    float slowSpeed;
    float currentSpeed;

    int damage;
    protected bool TakingDamage = false;
    protected bool Slowed = false;
    EnemyMoveTowardsPoint enemyMove;

    public float CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }
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

        currentSpeed = moveSpeed;
        slowSpeed = moveSpeed * .5f;

        #region UNUSED STUFF
        //Debug.Log(Health);
        //addEnemyTarget = new AddEnemyTargetEvent();
        //EventManager.AddEnemyTargetInvoker(this);
        //removeEnemyTarget = new RemoveEnemyTargetEvent();
        //EventManager.RemoveEnemyTargetInvoker(this);

        #endregion

    }

    /// <summary>
    /// Does not need to return from update on pause since this is just health checking
    /// which only changes during a collision
    /// </summary>
    protected virtual void Update()
    {
        if (Health > 0)
        {
            healthBar.fillAmount = Health / fullHealth;
        }

        // Life Checking
        if (Health <= 0)
        {

            SpawnItem();
            
            GameplayManager.Instance.EnemiesKilled++;
            GameplayManager.Instance.SpawnedEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    protected virtual void SpawnItem()
    {
        if (Random.Range(0, 5) > 1)
        {
            // Spawns collectable item
            Instantiate(item, transform.position, Quaternion.identity);
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
        // initial impact damge health loss
        Health -= amount;

        // Follwed by any additional status effects
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
        for (int i = 0; i < ConstantsManager.DOT_TIME; i++)
        {
            if (!GameplayManager.Instance.IsPaused)
            {
                Health -= amount;
                yield return new WaitForSeconds(ConstantsManager.DOT_WAIT_TIME);
            }
            else
            {
                yield return new WaitUntil(() => !GameplayManager.Instance.IsPaused);
                yield return new WaitForSeconds(ConstantsManager.DOT_WAIT_TIME);
            }
        }
            TakingDamage = false;
            sr.color = Color.white;
    }

    /// <summary>
    /// Handles the slow down of the enemy
    /// the for loop is set to the time it is slow for since in the loop
    /// iu use 1 second to wait, this ensure it loops every second
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator SlowEnemy()
    {
        for (int i = 0; i < ConstantsManager.SLOW_TIME; i++)
        {
            if (!GameplayManager.Instance.IsPaused)
            {
                sr.color = Color.blue;
                currentSpeed = slowSpeed;
                yield return new WaitForSeconds(1);
            }
            else
            {
                yield return new WaitUntil(() => !GameplayManager.Instance.IsPaused);
                yield return new WaitForSeconds(1);
            }

        }

        Slowed = false;
        currentSpeed = moveSpeed;
        sr.color = Color.white;

    }
    #endregion
}
