using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{
    protected CircleCollider2D cc2d;
    public List<GameObject> enemyTarget = new List<GameObject>();
    [SerializeField] GameObject projectile;

    protected bool canFire = false;
    public Transform target;
    float range;

    #region TOWER PROPERTIES

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

    protected TowerFireEvent towerFireEvent = new TowerFireEvent();
    public void AddTowerFireListener(UnityAction<Transform> listener)
    {
        towerFireEvent.AddListener(listener);
    }

    // Called before Start()
    private void Awake()
    {
        cc2d = GetComponent<CircleCollider2D>();
       
    }

    // Start is called before the first frame update
    void Start()
    {
        // Not Currently using
        //EventManager.EnemeyDequeueListener(DequeueEnemy);
        EventManager.TowerFireInvoker(this);
        //range = 2;//cc2d.radius;
        //InvokeRepeating("UpdateTarget", 0f, .5f); 
    }

    //void UpdateTarget()
    //{
    //    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    //    float shortestDistance = Mathf.Infinity;
    //    GameObject nearestEnemy = null;


    //    foreach (GameObject enemy in enemies)
    //    {
    //        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
    //        if (distanceToEnemy < shortestDistance)
    //        {
    //            shortestDistance = distanceToEnemy;
    //            nearestEnemy = enemy;
    //        }
    //    }

    //    if (nearestEnemy != null && shortestDistance <= range)
    //    {
    //        target = nearestEnemy.transform;
    //        canFire = true;
    //        StartCoroutine(Fire());

    //    }
    //    else
    //    {
    //        target = null;
    //        canFire = false;
    //    }
    //}

    //IEnumerator Fire()
    //{
    //    while (canFire)
    //    {
    //        towerFireEvent.Invoke(target.transform);
    //        Instantiate(projectile, transform.position, Quaternion.identity);
    //        yield return new WaitForSeconds(2);
    //    }

    //}

    private void Update()
    {
        if (target == null)
        {
            return;
        }
    }
    void CreateTower()
    {

    }
}
