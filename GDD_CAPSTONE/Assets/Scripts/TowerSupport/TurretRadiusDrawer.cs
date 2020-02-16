using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRadiusDrawer : MonoBehaviour
{
    SpriteRenderer sr;
    float radius;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        
        
        //UpdateSpriteSize();
       
    }

    // Start is called before the first frame update
    void Start()
    {
        //sprRadius.localScale.x = cc2d.radius;//new Vector2(cc2d.radius,cc2d.radius) * 2;
         // radius = cc2d.radius;
    }

    private void OnDrawGizmos()
    {
       // Gizmos.color = Color.red;
       // Gizmos.DrawWireSphere(transform.position, radius);
    }
}
