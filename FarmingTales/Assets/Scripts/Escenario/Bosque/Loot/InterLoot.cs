using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterLoot : MonoBehaviour
{
    private ToolBarController toolBarController;

    private void Start()
    {
        toolBarController = GameObject.FindWithTag("toolBar").GetComponent<ToolBarController>();
    }

    public void inter() {}
    
    public void mostrarInter()
    {
        if (toolBarController.position == 0)
        {
            GetComponentInChildren<InteractuarUIController>().visible();
        }
    }

    public void esconderInter()
    {
        if (toolBarController.position == 0)
        {
            GetComponentInChildren<InteractuarUIController>().esconder();
        }
    }
}
