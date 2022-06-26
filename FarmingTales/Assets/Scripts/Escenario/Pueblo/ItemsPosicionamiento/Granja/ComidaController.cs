using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ComidaController : MonoBehaviour, IPointerDownHandler
{

    private GameObject posicionRaton;
    private GameObject barraComida;

    public string[] idComida;
    public int[] numComida;
    
    public IDictionary<string, int> comidaCantidad = new Dictionary<string, int>();

    public Color color;

    private bool cofreAbierto = false;

    public GameObject granja;

    private void Awake()
    {
        posicionRaton = GameObject.Find("PosRaton");
        barraComida = GameObject.Find("BarraComida");

        for (int i = 0; i < idComida.Length; i++)
        {
            comidaCantidad.Add(idComida[i], numComida[i]);
        }
    }

    public void setGranja(GameObject g)
    {
        granja = g;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PosicionRatonController posicionRatonController = posicionRaton.GetComponent<PosicionRatonController>();

        if (granja != null && granja.GetComponent<GranjaController>().porcentageComida < 100)
        {
            if (comidaCantidad[posicionRatonController.item] != 0)
            {

                posicionRatonController.cantidad = posicionRatonController.cantidad - 1;

                if (posicionRatonController.cantidad <= 0)
                {
                    posicionRatonController.item = "";
                    posicionRaton.GetComponent<Image>().sprite = null;
                }

                granja.GetComponent<GranjaController>().porcentageComida = granja.GetComponent<GranjaController>().porcentageComida + comidaCantidad[posicionRatonController.item];

                if (granja.GetComponent<GranjaController>().porcentageComida > 100)
                {
                    granja.GetComponent<GranjaController>().porcentageComida = 100;
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (granja != null)
        {
            float barraComidaX = (52f * granja.GetComponent<GranjaController>().porcentageComida) / 100f;

            barraComida.GetComponent<RectTransform>().sizeDelta = new Vector2(barraComidaX, 7.4995f);
        }
    }
}
