using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MesaCrafteoController : MonoBehaviour
{

    public string id = "";
    
    private GameObject interfazMesaCrafteo;
    private GameObject player;
    
    private InventarioController inventarioController;
    private GeneradorPosicionamientoController generadorPosicionamientoController;
    private PosicionadorItemController posicionadorItemController;

    private void Start()
    {
        generadorPosicionamientoController =
            GameObject.Find("GeneradorPosiciones").GetComponent<GeneradorPosicionamientoController>();
        interfazMesaCrafteo = GameObject.Find("InterfazMesaCrafteo");
        player = GameObject.Find("Player");
        inventarioController = GameObject.Find("ToolBar").GetComponent<InventarioController>();
        posicionadorItemController = GameObject.Find("PosicionadorItem").GetComponent<PosicionadorItemController>();
    }

    public void inter()
    {
        inventarioController.mostrar = false;
        player.GetComponent<PlayerController>().mov = false;
        interfazMesaCrafteo.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
    
    public void quitar()
    {
        for (int i = 0; i < generadorPosicionamientoController.puntos.Length; i++)
        {
            string idPosicionGenerado = generadorPosicionamientoController.puntos[i].GetComponent<PuntoGeneradoController>().tipo + (i+1) + SceneManager.GetActiveScene().name;

            if (idPosicionGenerado.Equals(id))
            {
                generadorPosicionamientoController.puntos[i].GetComponent<PuntoGeneradoController>().tipo = "";
                generadorPosicionamientoController.puntos[i].GetComponent<PuntoGeneradoController>().ocupado = false;
                break;
            }
        }
        
        posicionadorItemController.guardarPosicionesItem();

        inventarioController.anadirInventario("mesaCrafteo", 1);
        
        Destroy(gameObject);
    }
    
    public void mostrarInter()
    {
        GetComponentInChildren<InteractuarUIController>().visibleDerecho();
    }

    public void esconderInter()
    {
        GetComponentInChildren<InteractuarUIController>().invisibleDerecho();
        transform.GetChild(1).GetComponent<InteractuarUIController>().esconder();
        transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }
    
    public void mostrarInterQuitar()
    {
        transform.GetChild(1).GetComponent<InteractuarUIController>().visible();
        transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    }

    public void setId(string ide)
    {
        id = ide;
    }
}
