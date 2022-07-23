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

    public Sprite imagen;
    
    public string hablante;
    
    private bool interactuar = true;

    public List<String> productosComprar = new List<string>();

    void Start()
    {
        player = GameObject.Find("Player");
        creadorProductos = GameObject.Find("CreadorProductos");
        tienda = GameObject.Find("TiendaController");
        inventario = GameObject.Find("ToolBar");
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
        GetComponentInChildren<InteractuarUIController>().visible();
    }

    public void esconderInter()
    {
        GetComponentInChildren<InteractuarUIController>().invisibleDerecho();
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
