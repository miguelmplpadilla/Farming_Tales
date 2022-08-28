using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    public string tipo = "madera";
    public int cant = 5;
    public Sprite spr;
    public IDictionary<string, ObjetoInventario> sprites;

    public int life = 3;

    private InventarioController inventarioController;

    private void Awake()
    {
        if (tipo == "madera")
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(2.8f, 1);
            cant = 10;
            life = 5;
        } else if (tipo == "roca")
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.8f, 1);
            cant = 5;
            life = 3;
        } else if (tipo == "oro")
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.8f, 1);
            cant = 2;
            life = 2;
        } else if (tipo == "rocaHierro")
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.8f, 1);
            cant = 3;
            life = 3;
        }
    }

    private void Start()
    {
        inventarioController = GameObject.Find("ToolBar").GetComponent<InventarioController>();

        sprites = inventarioController.infoObjetos;

        spr = sprites[tipo].sprite;
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

    public void temblar()
    {
        StartCoroutine("tremble");
    }
    
    IEnumerator tremble() {
        for ( int i = 0; i < 5; i++)
        {
            transform.localPosition += new Vector3(0.05f, 0, 0);
            yield return new WaitForSeconds(0.01f);
            transform.localPosition -= new Vector3(0.05f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
