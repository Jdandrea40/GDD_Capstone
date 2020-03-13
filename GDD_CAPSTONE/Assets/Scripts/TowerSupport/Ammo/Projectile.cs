using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Used to handle to functionality of Projectiles
/// </summary>
public class Projectile : MonoBehaviour
{
    #region EVENTS

    EnemyDamageEvent enemyDamageEvent;
    CircleCollider2D cc2d;
    [SerializeField] GameObject explosion;

    public void AddEnemyDamageListener(UnityAction<int, bool, int, bool> listener)
    {
        enemyDamageEvent.AddListener(listener);
    }

    #endregion

    int projDamage;
    bool projDoT;
    int projDotAmount;
    bool projSlow;

    bool projSplash;
    Color projColor;
    Sprite projSprite;

    BoxCollider2D bc2d;
    
    Transform targetToHit;
     
    SpriteRenderer sr;

    float projectileMoveSpeed;
    ParticleSystem particle;

    public int ProjDamage { get => projDamage; set => projDamage = value; }
    public bool ProjDoT { get => projDoT; set => projDoT = value; }
    public int ProjDotAmount { get => projDotAmount; set => projDotAmount = value; }
    public bool ProjSlow { get => projSlow; set => projSlow = value; }

    // Called before first frame update
    private void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        cc2d = GetComponent<CircleCollider2D>();
       
        //targetToHit = tower.TargetToShoot.transform;
        projectileMoveSpeed = ConstantsManager.PROJECTILE_MOVE_SPEED;

        //enemyDamageEvent = new EnemyDamageEvent();
        //EventManager.AddEnemyDamageInvoker(this);

        //EventManager.TowerFireListener(SetStats);
        sr.color = projColor;
        sr.sprite = projSprite;

    }

    public void SetStats(int damage, bool dot, int dotAmount, bool slow, bool AoE, Color sprColor, Sprite sprite)
    {
        projDamage = damage;
        projDoT = dot;
        projDotAmount = dotAmount;
        projSlow = slow;
        projColor = sprColor;
        projSprite = sprite;
        projSplash = AoE;

    }
    // Update is called once per frame
    void Update()
    {
        if (!GameplayManager.Instance.IsPaused)
        {
            // so long as there is a target to hit, go to it
            if (targetToHit != null)
            {
                // moves the projectile towards the saved enemy
                transform.position = Vector2.MoveTowards(transform.position,
                  targetToHit.position, projectileMoveSpeed * Time.deltaTime);

                // get a line from tower to target
                Vector2 direction = targetToHit.transform.position - transform.position;
                // find the angle of that line
                float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                // rotate the tower to always face the target
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                // Gets the first enemy in the list and stores it
                //EventManager.TowerFireListener(MoveToEnemy);
            }

            else
            {
                // if the target is detroyed before this proj hits
                // CASE: two towers are shooting at the same target
                Destroy(gameObject);
            }
        }
    }

    // used to indetify the target to move towards
    public void MoveToEnemy(GameObject target)
    {
        targetToHit = target.transform;
    }

    // Collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == (int)CollisionLayers.ENEMIES)
        {
           
            if (projSplash)
            {
                if (projSlow)
                {
                    Instantiate(ParticleEffectManager.Instance.particleDictionary[ParticleEffectManager.ParticleToPlay.CRYO_EXPLODE], transform.position, Quaternion.identity);
                    AudioManager.Instance.PlaySFX(AudioManager.Sounds.CRYO_EXPLODE2);

                }
                else if (projDoT)
                {
                    Instantiate(ParticleEffectManager.Instance.particleDictionary[ParticleEffectManager.ParticleToPlay.INCENDIARY_EXPLODE], transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(ParticleEffectManager.Instance.particleDictionary[ParticleEffectManager.ParticleToPlay.NORMAL_EXPLODE], transform.position, Quaternion.identity);
                    
                }
                cc2d.radius = 1;
                StartCoroutine(Explode());
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }

    /// <summary>
    /// Only called on a rocket projectile
    /// TODO: Handle Explosion Animation
    /// </summary>
    /// <returns></returns>
    IEnumerator Explode()
    { 
        Instantiate(explosion, transform.position, Quaternion.identity);

        // Waits for a second so that Splach damage can occur
        yield return new WaitForSeconds(.1f);
        Destroy(gameObject);
    }
}
