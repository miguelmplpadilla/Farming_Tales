using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IncubadoraController : MonoBehaviour, IPointerDownHandler
{
    private GameObject posicionRaton;
    private GameObject toolBar;
    public GranjaIncubadoraController granjaIncubadora;
    private GameObject textoIncubando;

    public Color color;

    public bool incubando = false;
    private void Start()
    {
        posicionRaton = GameObject.Find("PosRaton");
        toolBar = GameObject.Find("ToolBar");
        textoIncubando = GameObject.Find("TextoIncubadora");
    }

    private void LateUpdate()
    {
        GetComponent<Image>().sprite = toolBar.GetComponent<InventarioController>().sprites["huevoFecundado"];

        if (granjaIncubadora != null)
        {
            if (granjaIncubadora.cantidad > 0)
            {
                Color tempColor = Color.white;
                tempColor.a = 1;
                GetComponent<Image>().color = tempColor;
            }
            else
            {
                Color tempColor = color;
                GetComponent<Image>().color = tempColor;
            }
        }

        if (incubando)
        {
            textoIncubando.GetComponent<TextMeshProUGUI>().text = "<- Incubando";
        }
        else
        {
            textoIncubando.GetComponent<TextMeshProUGUI>().text = "<- Vacio";
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PosicionRatonController posicionRatonController = posicionRaton.GetComponent<PosicionRatonController>();
        
        if (posicionRatonController.item != "")
        {
            if (posicionRatonController.item == "huevoFecundado")
            {
                if (granjaIncubadora.item == "")
                {
                    if (granjaIncubadora.gameObject.GetComponent<GranjaController>().cantidadAnimales[0] + granjaIncubadora.gameObject.GetComponent<GranjaController>().cantidadAnimales[1] < 6)
                    {
                        granjaIncubadora.item = "huevoFecundado";
                        granjaIncubadora.cantidad = 1;

                        incubando = true;

                        granjaIncubadora.startIncubar();

                        posicionRatonController.cantidad = posicionRatonController.cantidad - 1;

                        if (posicionRatonController.cantidad <= 0)
                        {
                            posicionRatonController.item = "";
                            posicionRatonController.cantidad = 0;
                        }
                    }
                }
                else
                {
                    if (posicionRatonController.cantidad + 1 <= 128)
                    {
                        posicionRatonController.cantidad = posicionRatonController.cantidad + 1;
                        granjaIncubadora.item = "";
                        granjaIncubadora.cantidad = 0;
                        granjaIncubadora.tiempoIncubacionTranscurrido = 0;
                        incubando = false;
                        granjaIncubadora.stopIncubar();
                    }
                }
            }else if (posicionRatonController.item == "")
            {
                posicionRatonController.item = "huevoController";
                posicionRatonController.cantidad = 1;
                
                granjaIncubadora.item = "";
                granjaIncubadora.cantidad = 0;
                granjaIncubadora.tiempoIncubacionTranscurrido = 0;
                incubando = false;
                granjaIncubadora.stopIncubar();
            }
        }
    }
}
