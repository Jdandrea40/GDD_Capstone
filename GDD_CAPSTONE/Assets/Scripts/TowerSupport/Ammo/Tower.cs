using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{
    #region FIELDS
    // Singleton Instances
    PiecesCollectedManager pcm;
    HUD_CraftingUI hudCUI;

    // Components
    CircleCollider2D cc2d;
    SpriteRenderer sr;
    [SerializeField] SpriteRenderer turretBaseSprite;
    //[SerializeField] CanvasGroup sellCanvas;
    [SerializeField] GameObject rangeIndicator;

    // The towers targeting list
    public List<GameObject> enemyTargets;
    GameObject tToShoot;
    
    // The Projectile that is shot
    // Used in order to send the targets Transform to Projectile Script
    [SerializeField] GameObject projectile;
    
    // Coroutine for tower shooting
    IEnumerator fireCoroutine;

    // Idea for target acquisition
    //public Dictionary<int, GameObject> enemies; 

    // Used for tower rotation to target
    bool canFire = true;
    bool targeting = false;
    bool hovering = false;

    #endregion

    #region PROPERTIES

    // The current target the tower is shooting at
    GameObject targetToShoot;
    public GameObject TargetToShoot
    {
        get { return targetToShoot; }
    }

    // Accesor for TurretRangeIndicator
    public float TurretRadius { get => range; }

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
    float range;              // Base

    // Proj visuals
    Color ammoColor;        // Ammo
    Sprite projSpr;         // Ammo
    LineRenderer laserSight;
    Vector3 startPoint;
    [SerializeField] Material laserSightMat;

    // special cases effects
    bool splashDamage;      // TurretTop
    bool slow;              // Ammo  
    bool damageOverTime;    // Ammo  
    int dotAmount;          // Ammo

    #endregion

    #region EVENTS
    // Passes in (damage, damageOverTime, dotAmount, slow)
    //TowerFireEvent towerFireEvent;
    //public void AddTowerFireListener(UnityAction<int, bool, int, bool> listener)
    //{
    //    towerFireEvent.AddListener(listener);
    //}

    // Event used to activate the sell/upgrade Panel
    TowerPanelEvent towerPanelEvent;
    public void AddTowerPanelListener(UnityAction listener)
    {
        towerPanelEvent.AddListener(listener);
    }

    // Event used to decrement from the Pieces Collected Manager
    ScrapUsedEvent scrapUsedEvent;
    public void AddScrapUsedListener(UnityAction listener)
    {
        scrapUsedEvent.AddListener(listener);
    }

    #endregion

    #region UNITY METHODS

    // Called before Start()
    private void Awake()
    {
        // Event for setting the stats of the fired projectile
        // towerFireEvent = new TowerFireEvent();
        // EventManager.TowerFireInvoker(this);
        
        // List of targetable enemies in range
        enemyTargets = new List<GameObject>();
       
        // Component Grabbing
        pcm = PiecesCollectedManager.Instance;
        //hudCUI = HUD_CraftingUI.Instance;
        sr = GetComponent<SpriteRenderer>();
        cc2d = GetComponent<CircleCollider2D>();

        // Turret Initializer
        CreateTower();
        rangeIndicator.SetActive(false); 
    }

    // Start is called before the first frame update
    void Start()
    {
        // EventManager.AddEnemyTargetListener(AddEnemyTarget);
        // EventManager.RemoveEnemyTargetListener(RemoveEnemyTarget);
        // Not Currently using
        // EventManager.EnemeyDequeueListener(DequeueEnemy);
        // EVENTS
        scrapUsedEvent = new ScrapUsedEvent();
        EventManager.ScrapUsedInvoker(this);

        towerPanelEvent = new TowerPanelEvent();
        EventManager.ActivateTowerPanelInvoker(this);

        // TODO: right now, +3/+6 are implemetned to account for the NEW DICTIONARY I AM USING ---> FIX THIS
        PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)HUD_CraftingUI.SelectedTop]--;
        PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)HUD_CraftingUI.SelectedBot + 3]--;
        PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)HUD_CraftingUI.SelectedAmmo + 6]--;

        startPoint = gameObject.transform.position;
        if (laserSight != null)
        {
            laserSight.sortingLayerName = "TOWER_RADIUS";
            
            laserSight.SetPosition(0, startPoint);
            laserSight.SetPosition(1, startPoint);
            laserSight.startWidth = .03f;
            laserSight.startColor = Color.red;
            laserSight.endColor = Color.red;

            laserSight.material = laserSightMat;
        }
        scrapUsedEvent.Invoke();
        InvokeRepeating("UpdateTarget", 0, .5f);
    }

    // Called once a frame
    void Update()
    {
        if (!GameplayManager.Instance.IsPaused)
        {
            if (targetToShoot == null)
            {
                targeting = false;
                if (laserSight != null)
                {
                    laserSight.SetPosition(1, startPoint);
                }
                    return;
            }
            else
            {
                if(canFire)
                {
                    // spawns a Projectile and passes in the necessary attributes:
                    // this is: ImpactDamage, DamageOverTime (bool) -> Amount, Slow (bool), SplashDamge (bool), color, and sprite
                    // as well as the current target it is aiming at so that the proj can MoveTowards this enemy
                    Projectile proj = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
                    proj.SetStats(damage, damageOverTime, dotAmount, slow, splashDamage, ammoColor, projSpr, targetToShoot.GetInstanceID());
                    // call the method inside Projectile to travel towards target
                    proj.MoveToEnemy(targetToShoot);
                    if (!splashDamage)
                    {
                        AudioManager.Instance.PlaySFX(AudioManager.Sounds.GUN_SHOT);
                    }
                    else
                    {
                        AudioManager.Instance.PlaySFX(AudioManager.Sounds.MISSLE_LAUNCH);
                    }
                    canFire = false;
                    StartCoroutine(WaitToFire());

                }
                if (!targeting)
                {
                    targeting = true;
                }

                if (targeting)
                {
                    if (laserSight != null)
                    {
                        laserSight.SetPosition(1, targetToShoot.transform.position);

                    }
                    // get a vector from tower to target
                    Vector2 direction = targetToShoot.transform.position - transform.position;
                    // find the angle of that line
                    float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                    // rotate the projectile to always face the target
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                }
            }
        }
    }

    /// <summary>
    /// When moused over tower, Show its range
    /// </summary>
    private void OnMouseEnter()
    {
        if (!GameplayManager.Instance.IsPaused)
        {
            hovering = true;
            rangeIndicator.SetActive(true);
        }
    }

    // Mouse Exit support
    private void OnMouseExit()
    {
        hovering = false;

        rangeIndicator.SetActive(false);

        //sellCanvas.alpha = 0;
        //sellCanvas.interactable = false;
        //sellCanvas.blocksRaycasts = false;
    }

    // Click support
    private void OnMouseDown()
    {
        if (hovering && !GameplayManager.Instance.IsPaused)
        {
            GameplayManager.Instance.TowerToSell = gameObject;
            towerPanelEvent.Invoke();
            //sellCanvas.alpha = 1;
            //sellCanvas.interactable = true;
            //sellCanvas.blocksRaycasts = true;
        }
    }


    #endregion

    #region CUSTOM METHODS

    /// <summary>
    /// Coroutine used to handle the rate of fire waiting
    /// sets the canfire (Update()) bool to true when done
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitToFire()
    {

        for (int i = 0; i < fireRate; i++)
        {
            if (!GameplayManager.Instance.IsPaused)
            {
                yield return new WaitForSeconds(1);
            }
            else
            {
                yield return new WaitUntil(() => !GameplayManager.Instance.IsPaused);
            }
        }
        canFire = true;
    }

    /// <summary>
    /// Destroys a placed tower
    /// </summary>
    public void RemoveTower()
    {
        // Sets the BuildableArea occupied boolean to false
        BuildableArea ba = GetComponentInParent<BuildableArea>();
        ba.Occupied = false;
        ba.bc2d.enabled = true;
        Destroy(gameObject);
    }

    /// <summary>
    /// https://www.youtube.com/watch?v=QKhn2kl9_8I&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0&index=5&t=1047s
    /// Brackeys tutorial for ClosestTarget Acquisition
    /// </summary>
    void UpdateTarget()
    {
        if (GameplayManager.Instance.SpawnedEnemies.Count > 0)
        {
            //for (int i = 0; i < GameplayManager.Instance.SpawnedEnemies.Count; i++)
            //{
            foreach (GameObject enemy in GameplayManager.Instance.SpawnedEnemies)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                if (targetToShoot == null)
                {
                    
                    if (distanceToEnemy <= range)
                    {
                        targetToShoot = enemy;
                    }
                    else
                    {
                        targetToShoot = null;
                    }
                }
            }
            if (targetToShoot != null)
            {
                if (laserSight != null)
                {
                    laserSight.enabled = true;

                    //laserSight.SetPositions(gameObject.transform.position, targetToShoot.transform.position);
                }
                if (Vector2.Distance(transform.position, targetToShoot.transform.position) > range)
                {
                    targetToShoot = null;
                }

            }
        }
    
        //    float shortestDistace = Mathf.Infinity;
        //    GameObject closestEnemy = null;
        //    foreach (GameObject enemy in GameplayManager.Instance.SpawnedEnemies)
        //    {
        //        float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

        //        if (distanceToEnemy < shortestDistace)
        //        {
        //            shortestDistace = distanceToEnemy;
        //            closestEnemy = enemy;
        //        }
        //    }

        //    if (closestEnemy != null && shortestDistace <= range)
        //    {
        //        targetToShoot = closestEnemy;
        //    }
        //    else
        //    {
        //        targetToShoot = null;
        //    }

        //}

    }

    /// <summary>
    /// Once the tower is instantiated it takes in all the attributes from
    /// the HUD_CUI and takes the Scriptable Objects stats associated with the selected piece
    /// </summary>
    void CreateTower()
    {
        TurretTop tTop = pcm.pcTop[HUD_CraftingUI.SelectedTop];
        TowerBase tBase = pcm.pcBase[HUD_CraftingUI.SelectedBot];
        AmmoType tAmmo = pcm.pcAmmo[HUD_CraftingUI.SelectedAmmo];

        if (tBase.Name == "Laser Sight")
        {
            laserSight = gameObject.AddComponent<LineRenderer>();
        }
        else
        {
            laserSight = null;
        }
        // Visuals
        sr.sprite = tTop.TurretSprite;
        sr.color = tAmmo.color;
        projSpr = tTop.ProjectileSprite;
        //turretBaseSprite.sprite = tBase.BaseSprite;
        turretBaseSprite.color = tBase.BaseColor;

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
