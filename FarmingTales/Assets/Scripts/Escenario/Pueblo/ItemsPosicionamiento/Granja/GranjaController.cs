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
        //spriteRenderer.size = new Vector2(spriteRenderer.size.x * 3, spriteRenderer.size.y);
    }

    public void setId(string ide)
    {
        id = ide;
    }

    void Update()
    {
        /*float distancia = Vector3.Distance(transform.position, posicionadorItem.transform.position);
        
        Debug.Log("Distancia: "+distancia);
        
        if (transform.position.x < posicionadorItem.transform.position.x)
        {
            if (Vector3.Distance(transform.position, posicionadorItem.transform.position) == 0)
            {
                spriteRenderer.size = new Vector2(0.8f, 0.45f);
            }
            else
            {
                float tamanoX = (float)Math.Round(0.8f * distancia, 2);
                spriteRenderer.size = new Vector2(tamanoX*2, 0.45f);
            }
        }
        else
        {
            spriteRenderer.size = new Vector2(0.8f, 0.45f);
        }*/
        
    }
}
