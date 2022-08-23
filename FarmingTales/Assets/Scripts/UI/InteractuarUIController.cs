using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuarUIController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void visibleDerecho()
    {
        transform.localScale = new Vector3(-1, 1, 1);
        spriteRenderer.enabled = true;
    }
    
    public void visible()
    {
        spriteRenderer.enabled = true;
    }

    public void esconder()
    {
        spriteRenderer.enabled = false;
    }
    
    public void visibleIzquierdo()
    {
        transform.localScale = new Vector3(1, 1, 1);
        spriteRenderer.enabled = true;
    }
    
    public void invisibleDerecho()
    {
        spriteRenderer.enabled = false;
    }
    
    public void invisibleIzquierdo()
    {
        spriteRenderer.enabled = false;
    }
}
