using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BA_RangeIndicator : MonoBehaviour
{
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        float range = GetComponentInParent<BuildableArea>().CurrTowerRange;
        Color color = GetComponentInParent<BuildableArea>().RangeColor;
        sr.material.color = color;
        sr.transform.localScale = new Vector2(range, range);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
