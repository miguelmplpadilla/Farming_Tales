using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranjaController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private GameObject posicionadorItem;
    public int puntoPosicionado;

    public string id = "";

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        posicionadorItem = GameObject.Find("PosicionadorItem");
    }

    public void setId(string ide)
    {
        id = ide;
    }

    void Update()
    {
        
    }

    public void inter()
    {
        
    }

    public void mostrarInter()
    {
        GetComponentInChildren<InteractuarUIController>().visibleDerecho();
    }

    public void esconderInter()
    {
        GetComponentInChildren<InteractuarUIController>().invisibleDerecho();
    }
}
