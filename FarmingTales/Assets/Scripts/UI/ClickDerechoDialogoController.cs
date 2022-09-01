using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickDerechoDialogoController : MonoBehaviour
{
    private GameObject interfazTienda;

    private void Start()
    {
        interfazTienda = GameObject.Find("InterfazTienda");
    }

    void Update()
    {
        if (interfazTienda.transform.localScale.x > 0)
        {
            GetComponent<Image>().enabled = false;
        }
        else
        {
            GetComponent<Image>().enabled = true;
        }
    }
}
