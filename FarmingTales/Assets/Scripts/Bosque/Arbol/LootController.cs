using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    public string tipo = "arbol";
    public int cant = 5;

    public int life = 3;

    private void Awake()
    {
        if (tipo == "arbol")
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(2.8f, 1);
            cant = 10;
            life = 5;
        } else if (tipo == "roca")
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.8f, 1);
            cant = 5;
            life = 3;
        }
    }

    private void Update()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void setDatos(string tipe, Sprite sprite)
    {
        tipo = tipe;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("loot"))
        {
            if (other.transform.position.x > transform.position.x)
            {
                transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
            }
        }

        if (other.CompareTag("limiteGeneracion"))
        {
            Destroy(gameObject);
        }
        
        
    }
}
