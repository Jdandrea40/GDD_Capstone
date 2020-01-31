using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{
    protected CircleCollider2D cc2d;
    public List<GameObject> enemyTargets;
    [SerializeField] GameObject projectile;

    //public Dictionary<int, GameObject> enemies; 

    bool canFire = false;
    bool firing = false;
    // protected Transform target;
    float range;

    GameObject targetToShoot;
    public GameObject TargetToShoot
    {
        get { return targetToShoot; }
    }

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
        cc2d = GetComponent<CircleCollider2D>();
        //enemies = new Dictionary<int, GameObject>();
        enemyTargets = new List<GameObject>();
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

    void Update()
    {
        if (enemyTargets.Count > 0)
        {
            canFire = true;
            targetToShoot = enemyTargets[0];
            if(!firing)
            {
                StartCoroutine(Fire());
                firing = true;
            }
        }       
        else if (enemyTargets.Count < 1)
        {
            StopCoroutine(Fire());
            firing = false;
            canFire = false;
            return;
        }

        if (firing)
        {
            Vector2 direction = targetToShoot.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
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

    IEnumerator Fire()
    {
        // TODO: REMOVE HARDCODED VALUES
        // towerFireEvent.Invoke(target.transform);
        Projectile proj = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
        proj.MoveToEnemy(targetToShoot);
        yield return new WaitForSeconds(1);
        firing = false;
        
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
