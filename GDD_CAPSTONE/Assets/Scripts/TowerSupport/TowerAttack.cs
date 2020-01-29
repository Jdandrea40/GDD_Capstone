using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : Tower
{
    [SerializeField] GameObject projectile;
    //public Transform target;
    public float range = 2;
    float distance;
    bool firing = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    IEnumerator UpdateTarget()
    {
        yield return new WaitForEndOfFrame();
        if (enemyTarget.Count > 0)
        {
            distance = Vector3.Distance(transform.position,
                enemyTarget[0].transform.position);

            if (distance <= range)
            {
                canFire = true;
                firing = true;
                StartCoroutine(Fire());
            }
            else
            {
                canFire = false;
                firing = false;
                StopCoroutine(Fire());
            }
        }
    }

    IEnumerator Fire()
    {
        while (canFire)
        {
            towerFireEvent.Invoke(target.transform);
            Instantiate(projectile, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2);
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        if (canFire)
        {
            StartCoroutine(Fire());
        }

        if (firing)
        {
            Vector2 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
