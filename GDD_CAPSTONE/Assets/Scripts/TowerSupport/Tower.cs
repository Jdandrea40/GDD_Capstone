using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{
    TOWERCREATIONTESTER tct;
    Sprite projSPR;
    #region FIELDS
    // The towers targeting list
    public List<GameObject> enemyTargets;
    

    // The Projectile that is shot
    // Used in order to send the targets Transform to Projectile Script
    [SerializeField] GameObject projectile;
    
    // TODO: make this affect the range of the turret
    //CircleCollider2D cc2d;

    // Coroutine for tower shooting
    IEnumerator fireCoroutine;

    // Idea for target acquisition
    //public Dictionary<int, GameObject> enemies; 

    // Used for tower rotation to target
    bool firing = false;

    #endregion

    #region PROPERTIES

    // The current target the tower is shooting at
    GameObject targetToShoot;
    public GameObject TargetToShoot
    {
        get { return targetToShoot; }
    }

    #endregion

    #region TOWER STATS

    // damage modifiers
    protected int Damage;             // Ammo + TurretTop
    protected float FireRate;         // TurretTop + TurretBase
    protected int DoTAmount;          // Ammo
    protected int Range;              // Base
    
    // visuals
    protected Color tColor;           // Ammo
    protected Sprite projSpr;         // Ammo

    // status effects
    protected bool SplashDamage;      // TurretTop
    protected bool Slow;              // Ammo  
    protected bool DamageOverTime;    // Ammo  

    #endregion

    //protected TowerFireEvent towerFireEvent = new TowerFireEvent();
    //public void AddTowerFireListener(UnityAction<Transform> listener)
    //{
    //    towerFireEvent.AddListener(listener);
    //}

    #region UNITY METHODS

    // Called before Start()
    private void Awake()
    {
        // Needs to be set to a variable in order to StopCoroutine()
        fireCoroutine = Fire(); 
        enemyTargets = new List<GameObject>();
        tct = GetComponent<TOWERCREATIONTESTER>();
        projSPR = tct.ProjSprite;

        // cc2d = GetComponent<CircleCollider2D>();     
        //enemies = new Dictionary<int, GameObject>();       
    }

    // Start is called before the first frame update
    void Start()
    {
        // EventManager.AddEnemyTargetListener(AddEnemyTarget);
        // EventManager.RemoveEnemyTargetListener(RemoveEnemyTarget);
        // Not Currently using
        // EventManager.EnemeyDequeueListener(DequeueEnemy);
        // EventManager.TowerFireInvoker(this);
        // range = 2;//cc2d.radius;
        // InvokeRepeating("UpdateTarget", 0f, .5f); 
    }

    // Called once a frame
    void Update()
    {
        // Check for a non empty List (List<GO> enemyTarget)
        if (enemyTargets.Count > 0)
        {
            // sets the current target to the fist one in the List
            targetToShoot = enemyTargets[0];
            // Makes sure Fire() is only called if it isn't currently shooting
            if(!firing)
            {
                // Stop the ability to call Fire/call Fire()
                firing = true;
                StartCoroutine(fireCoroutine);              
            }
        }
        // Nothing to shoot at
        else if (enemyTargets.Count < 1)
        {
            // Stop shooting
            StopCoroutine(fireCoroutine);
            firing = false;
        }

        // If it has a target
        if (firing)
        {
            // get a line from tower to target
            Vector2 direction = targetToShoot.transform.position - transform.position;
            // find the angle of that line
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            // rotate the tower to always face the target
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        }
    }

    // Target acquired
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log(collision.name + " " + collision.gameObject.GetInstanceID());
        if (collision.gameObject.layer == (int)CollisionLayers.ENEMIES)
        {
            //if (enemies.ContainsKey(collision.GetInstanceID()))
            //{
            //enemies.Add(collision.gameObject.GetInstanceID(), collision.gameObject);
            //}
            enemyTargets.Add(collision.gameObject);
        }
    }

    // Target unacquired
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)CollisionLayers.ENEMIES)
        {
            //if (enemies.ContainsKey(collision.GetInstanceID()))
            //{
            //    enemies.Remove(collision.GetInstanceID());
            //}

            enemyTargets.Remove(collision.gameObject);
        }
    }
    #endregion

    #region CUSTOM METHODS

    // Fire Coroutine
    IEnumerator Fire()
    {
        // TODO: REMOVE HARDCODED VALUES
        // towerFireEvent.Invoke(target.transform);
        while (firing)
        {
            // instatiate a bullet
            Projectile proj = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
            // call the method inside Projectile to travel towards target
            proj.MoveToEnemy(targetToShoot);
            // COOLDOWN
            yield return new WaitForSeconds(1);
        }
        
    }

    void AddEnemyTarget(int targetID, GameObject enemy)
    {
        //if (!enemies.ContainsKey(targetID))
        //{
        //    enemies.Add(targetID, enemy);
        //}
    }
    void RemoveEnemyTarget(int targetID, GameObject enemy)
    {
        //enemies.Remove(targetID);
    }
    void CreateTower()
    {

    }

    #endregion
}
