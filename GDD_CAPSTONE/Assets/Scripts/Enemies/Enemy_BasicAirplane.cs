using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BasicAirplane : Enemy
{
    public override void Awake()
    {
        base.Awake();
        Health = eStat.BASIC_AIRPLANE_HEALTH;
        moveSpeed = eStat.BASIC_AIRPLANE_MOVE_SPEED;
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

    }
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Health <= 0)
        {
            Debug.Log(Health);
            Instantiate(item, transform.position, Quaternion.identity);
            GameplayManager.EnemiesKilled++;
            GameplayManager.SpawnEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
    public override void TakeDamage(int amount, bool dot, int dotAmount, bool slow)
    {
        base.TakeDamage(amount, dot, dotAmount, slow);
        Health -= amount;
        if (dot)
        {
            StartCoroutine(TakeDamageOverTime(dotAmount));
            Debug.Log("DOT");
        }
        if (slow && !Slowed)
        {
            StartCoroutine(SlowEnemy());
            Slowed = true;
            Debug.Log("SLOW");
        }
    }
    public override IEnumerator TakeDamageOverTime(int amount)
    {
        return base.TakeDamageOverTime(amount);
    }
    public override IEnumerator SlowEnemy()
    {
        return base.SlowEnemy();
    }
}
