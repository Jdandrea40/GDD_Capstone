using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableArea : MonoBehaviour
{
    SpriteRenderer sr;
    public Color hoverColor;
    public Color startColor;
    [SerializeField]
    GameObject tower;

    BoxCollider2D bc2d;
    Vector2 center;

    bool hovering = false;
    bool occupied = false;
    

    private void Start()
    { 
        sr = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();
        center = bc2d.size / 2;
        startColor = sr.material.color;
        hoverColor = Color.gray;
        
    }
    private void OnMouseEnter()
    {
        sr.material.color = hoverColor;
        hovering = true;
    }

    private void OnMouseExit()
    {
        sr.material.color = startColor;
        hovering = false;
    }

    private void OnMouseDown()
    {
        if (hovering && !occupied)
        {
            Instantiate(tower, transform.position, Quaternion.identity);
            occupied = true;
        }
    }
}
