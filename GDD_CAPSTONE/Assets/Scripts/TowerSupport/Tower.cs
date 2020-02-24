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
    [SerializeField] CanvasGroup sellCanvas;
    [SerializeField] GameObject rangeIndicator;

    // The towers targeting list
    public List<GameObject> enemyTargets;
    GameObject tToShoot;
    
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
    public int TurretRadius { get => range; }

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
    Color ammoColor;        // Ammo
    Sprite projSpr;         // Ammo

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

        // Needs to be set to a variable in order to StopCoroutine()
        fireCoroutine = Fire();
        
        // List of targetable enemies in range
        enemyTargets = new List<GameObject>();
       
        // Component Grabbing
        pcm = PiecesCollectedManager.Instance;
        hudCUI = HUD_CraftingUI.Instance;
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

        // TODO: right now, +3/+6 are implemetned to account for the NEW DICTIONARY I AM USING ---> FIX THIS
        PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)HUD_CraftingUI.Instance.SelectedTop]--;
        PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)HUD_CraftingUI.Instance.SelectedBot + 3]--;
        PiecesCollectedManager.Instance.CollectedPieces[(PiecesCollectedManager.TowerPieceEnum)HUD_CraftingUI.Instance.SelectedAmmo + 6]--;

        sellCanvas.alpha = 0;
        sellCanvas.interactable = false;
        sellCanvas.blocksRaycasts = false;

        scrapUsedEvent.Invoke();
        InvokeRepeating("UpdateTarget", 0, .5f);
        //Debug.Log(damage);
    }

    // Called once a frame
    void Update()
    {
        if (targetToShoot == null)
        {
            firing = false;
            StopCoroutine(fireCoroutine);
            return;
        }
        else
        {
            if (!firing)
            {
                firing = true;
                StartCoroutine(fireCoroutine);

                //// get a vector from tower to target
                //Vector2 direction = targetToShoot.transform.position - transform.position;
                //// find the angle of that line
                //float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                //// rotate the projectile to always face the target
                //transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
            }

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
    }

    private void OnMouseEnter()
    {
        hovering = true;
        rangeIndicator.SetActive(true);
    }

    // Mouse Exit support
    private void OnMouseExit()
    {
        hovering = false;

        rangeIndicator.SetActive(false);

        sellCanvas.alpha = 0;
        sellCanvas.interactable = false;
        sellCanvas.blocksRaycasts = false;
    }

    // Click support
    private void OnMouseDown()
    {
        if (hovering)
        {
            sellCanvas.alpha = 1;
            sellCanvas.interactable = true;
            sellCanvas.blocksRaycasts = true;
        }
    }

    
    #endregion

    #region CUSTOM METHODS
    public void RemoveTower()
    {
        BuildableArea ba = GetComponentInParent<BuildableArea>();
        ba.Occupied = false;
        Destroy(gameObject);
    }
    /// <summary>
    /// https://www.youtube.com/watch?v=QKhn2kl9_8I&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0&index=5&t=1047s
    /// Brackeys tutorial for ClosestTarget Acquisition
    /// </summary>
    void UpdateTarget()
    {
        float shortestDistace = Mathf.Infinity;
        GameObject closestEnemy = null;
        foreach(GameObject enemy in GameplayManager.Instance.SpawnedEnemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistace)
            {
                shortestDistace = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null && shortestDistace <= range)
        {
            targetToShoot = closestEnemy;
        }
        else
        {
            targetToShoot = null;
        }
    }
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
    void CreateTower()
    {
        TurretTop tTop = pcm.pcTop[hudCUI.SelectedTop];
        TowerBase tBase = pcm.pcBase[hudCUI.SelectedBot];
        AmmoType tAmmo = pcm.pcAmmo[hudCUI.SelectedAmmo];

        
        // Visuals
        sr.sprite = tTop.TurretSprite;
        sr.color = tAmmo.color;
        projSpr = tTop.ProjectileSprite;
        turretBaseSprite.sprite = tBase.BaseSprite;
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
