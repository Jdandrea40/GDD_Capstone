using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{
    PiecesCollectedManager pcm;
    HUD_CraftingUI hudCUI;
    CircleCollider2D cc2d;
    SpriteRenderer sr;
    [SerializeField] SpriteRenderer rangeSprite;

    TOWERCREATIONTESTER tct;
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

    // All Towers Need:
    // Visuals
    Sprite turretSprite;
    Sprite bottomSprite;
    Sprite projSprite;
    // damage modifiers
    int damage;             // Ammo + TurretTop
    float fireRate;         // TurretTop + TurretBase
    int range;              // Base

    // Proj visuals
    Color ammoColor;           // Ammo
    Sprite projSpr;         // Ammo

    // special cases effects
    bool splashDamage;      // TurretTop
    bool slow;              // Ammo  
    bool damageOverTime;    // Ammo  
    int dotAmount;          // Ammo

    //public Tower(Sprite turret, Sprite bottom, int damage, float fireRate, int range, Color tColor, Sprite projSpr, bool splashDamage, bool slow, bool damageOverTime, int dotAmount)
    //{
    //    this.turret = turret;
    //    this.bottom = bottom;
    //    this.damage = damage;
    //    this.fireRate = fireRate;
    //    this.range = range;
    //    this.tColor = tColor;
    //    this.projSpr = projSpr;
    //    this.splashDamage = splashDamage;
    //    this.slow = slow;
    //    this.damageOverTime = damageOverTime;
    //    this.dotAmount = dotAmount;
    //}


    #endregion

    #region EVENTS
    // Passes in (damage, damageOverTime, dotAmount, slow)
    TowerFireEvent towerFireEvent;
    public void AddTowerFireListener(UnityAction<int, bool, int, bool> listener)
    {
        towerFireEvent.AddListener(listener);
    }

    #endregion

    #region UNITY METHODS

    // Called before Start()
    private void Awake()
    {
        // Event for setting the stats of the fired projectile
        // towerFireEvent = new TowerFireEvent();
        // EventManager.TowerFireInvoker(this);

        // Needs to be set to a variable in order to StopCoroutine()
        fireCoroutine = Fire(); 
        enemyTargets = new List<GameObject>();
       
        pcm = PiecesCollectedManager.Instance;
        hudCUI = HUD_CraftingUI.Instance;
        sr = GetComponent<SpriteRenderer>();
        cc2d = GetComponent<CircleCollider2D>();

        tct = GetComponent<TOWERCREATIONTESTER>();
        CreateTower();
        cc2d.radius = range;

        //projSPR = tct.ProjSprite;
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
            // get a vector from tower to target
            Vector2 direction = targetToShoot.transform.position - transform.position;
            // find the angle of that line
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            // rotate the projectile to always face the target
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
            //towerFireEvent.Invoke(damage, damageOverTime, dotAmount, slow);
            // instatiate a bullet
            Projectile proj = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
            //towerFireEvent.Invoke
            proj.SetStats(damage, damageOverTime, dotAmount, slow, splashDamage, ammoColor, projSpr);
            // call the method inside Projectile to travel towards target
            proj.MoveToEnemy(targetToShoot);
            // COOLDOWN
            yield return new WaitForSeconds(fireRate);
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
        TurretTop tTop = pcm.pcTop[hudCUI.SelectedTop];
        TowerBase tBase = pcm.pcBase[hudCUI.SelectedBot];
        AmmoType tAmmo = pcm.pcAmmo[hudCUI.SelectedAmmo];

        
        // Visuals
        sr.sprite = tTop.TurretSprite;
        projSpr = tTop.ProjectileSprite;
        rangeSprite.sprite = tBase.BaseSprite;
        rangeSprite.color = tBase.BaseColor;

        // Stat Values
        damage = (tTop.Damage + tAmmo.ImpactDamage);
        fireRate = (tTop.FireRate + tBase.FireRateModifier);
        range = tBase.Range;
        ammoColor = tAmmo.color;
        splashDamage = tTop.SplashDamage;
        slow = tAmmo.Slow;
        damageOverTime = tAmmo.DamageOverTime;
        dotAmount = tAmmo.DoTAmount;
        splashDamage = tTop.SplashDamage;

    }

    #endregion
}
