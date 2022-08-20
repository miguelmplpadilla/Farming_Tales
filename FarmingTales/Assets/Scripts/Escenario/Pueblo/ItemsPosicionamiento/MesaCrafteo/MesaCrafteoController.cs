using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesaCrafteoController : MonoBehaviour
{

    public string id = "";
    
    private GameObject interfazMesaCrafteo;
    private GameObject player;
    
    private InventarioController inventarioController;

    private void Start()
    {
        interfazMesaCrafteo = GameObject.Find("InterfazMesaCrafteo");
        player = GameObject.Find("Player");
        inventarioController = GameObject.Find("ToolBar").GetComponent<InventarioController>();
    }

    public void inter()
    {
        inventarioController.mostrar = false;
        player.GetComponent<PlayerController>().mov = false;
        interfazMesaCrafteo.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
    
    public void mostrarInter()
    {
        GetComponentInChildren<InteractuarUIController>().visibleDerecho();
    }

    public void esconderInter()
    {
        GetComponentInChildren<InteractuarUIController>().invisibleDerecho();
    }

    public void setId(string ide)
    {
        id = ide;
    }
}
