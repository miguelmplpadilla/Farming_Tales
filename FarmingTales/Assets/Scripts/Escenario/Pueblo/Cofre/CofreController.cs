using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CofreController : MonoBehaviour
{
    public PosicionInventarioCofre[] posicionInventarioCofres;
    private InventarioCofreController inventarioCofreController;
    
    private bool abierto = false;
    
    private RectTransform rectTransformInventario;
    private RectTransform rectTransformRaton;

    private void Start()
    {
        inventarioCofreController = GameObject.Find("InventarioCofre").GetComponent<InventarioCofreController>();

        posicionInventarioCofres = new PosicionInventarioCofre[inventarioCofreController.posiciones.Length];

        for (int i = 0; i < posicionInventarioCofres.Length; i++)
        {
            posicionInventarioCofres[i] = new PosicionInventarioCofre();
        }
        
        rectTransformInventario = GameObject.Find("Inventario").GetComponent<RectTransform>();
        rectTransformRaton = GameObject.Find("Raton").GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (abierto)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                for (int i = 0; i < posicionInventarioCofres.Length; i++)
                {
                    posicionInventarioCofres[i].item = inventarioCofreController.posiciones[i].GetComponent<PosicionController>().item;
                    posicionInventarioCofres[i].cantidad = inventarioCofreController.posiciones[i].GetComponent<PosicionController>().cantidad;
                    posicionInventarioCofres[i].sprite = inventarioCofreController.posiciones[i].GetComponent<Image>().sprite;
                }
                
                rectTransformInventario.localScale = new Vector3(0, 1, 1);
                rectTransformRaton.localScale = new Vector3(0, 1, 1);
                inventarioCofreController.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0,1,1);
                abierto = false;
            }
        }
    }

    public void inter()
    {
        for (int i = 0; i < posicionInventarioCofres.Length; i++)
        {
            inventarioCofreController.posiciones[i].GetComponent<PosicionController>().item =
                posicionInventarioCofres[i].item;
            inventarioCofreController.posiciones[i].GetComponent<PosicionController>().cantidad =
                posicionInventarioCofres[i].cantidad;
            inventarioCofreController.posiciones[i].GetComponent<Image>().sprite =
                posicionInventarioCofres[i].sprite;
        }
        
        rectTransformInventario.localScale = new Vector3(1, 1, 1);
        rectTransformRaton.localScale = new Vector3(1, 1, 1);
        inventarioCofreController.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        
        abierto = true;
    }
}
