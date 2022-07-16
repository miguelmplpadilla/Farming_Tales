using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenderoController : MonoBehaviour
{

    private GameObject player;
    private GameObject creadorProductos;
    private GameObject tienda;
    private GameObject inventario;
    
    private bool interactuar = true;

    public List<String> productosComprar = new List<string>();

    void Start()
    {
        player = GameObject.Find("Player");
        creadorProductos = GameObject.Find("CreadorProductos");
        tienda = GameObject.Find("TiendaController");
        inventario = GameObject.Find("ToolBar");
    }

    
    void Update()
    {
    }

    public void inter()
    {
        player.GetComponent<PlayerController>().mov = false;
        creadorProductos.GetComponent<CreadorProductosController>().setTendero(gameObject);
        creadorProductos.GetComponent<CreadorProductosController>().listaComprar();
        tienda.GetComponent<TiendaController>().mostrarTienda();

        inventario.GetComponent<InventarioController>().mostrar = false;
    }

    public void mostrarInter()
    {
        
    }

    public void esconderInter()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactuar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactuar = false;
        }
    }
}
