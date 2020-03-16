using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MouseHoverEffect : MonoBehaviour
{
    SpriteRenderer sr;
    Color startColor;
    Color hoverColor;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        startColor = sr.material.color;
        hoverColor = Color.black;
        
    }
    private void OnMouseEnter()
    {
        sr.material.color = hoverColor;      
    }

    private void OnMouseExit()
    {
        sr.material.color = startColor;
    }
}
