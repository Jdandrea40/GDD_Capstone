using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to show the range of the turret
/// </summary>
public class TurretRadiusDrawer : MonoBehaviour
{
    SpriteRenderer sr;
    Tower radiusSupport;
    float radius;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        // gets the cc2d radius from the parent (range of base)
        radiusSupport = GetComponentInParent<Tower>();
        // the scale of the sprite is equal to 2 * radius of cc2d
        radius = radiusSupport.TurretRadius * 2;
        // sprite scale edit
        sr.transform.localScale = new Vector3(radius, radius);
    }
}
