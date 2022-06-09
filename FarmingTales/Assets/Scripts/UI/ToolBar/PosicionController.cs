using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PosicionController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    public string item = "";
    public int cantidad = 0;

    private RectTransform rectTransform;

    private GameObject posicionRaton;
    private GameObject raton;
    
    private TextMeshProUGUI texto;

    public Color color;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        posicionRaton = GameObject.Find("PosRaton");
        raton = GameObject.Find("Raton");
    }

    private void Start()
    {
        texto = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (cantidad > 0)
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

    private void LateUpdate()
    {
        if (cantidad > 0)
        {
            texto.text = cantidad.ToString();
        }
        else
        {
            texto.text = "";
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PosicionRatonController posicionRatonController = posicionRaton.GetComponent<PosicionRatonController>();
        Debug.Log("He pulsado");
        if (item != "" && raton.GetComponent<RectTransform>().localScale.x != 0)
        {
            if (posicionRatonController.item == "")
            {
                posicionRatonController.item = item;
                posicionRatonController.cantidad = cantidad;
                posicionRaton.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
                item = "";
                cantidad = 0;
            }
            else
            {
                if (posicionRatonController.item == item)
                {
                    if ((cantidad+posicionRatonController.cantidad) <= 128)
                    {
                        cantidad = cantidad + posicionRatonController.cantidad;
                        posicionRatonController.item = "";
                        posicionRatonController.cantidad = 0;
                    }
                    else
                    {
                        int cantAnterior = posicionRatonController.cantidad;
                        posicionRatonController.cantidad = (cantAnterior + cantidad) - 128;
                        cantidad = 128;
                        posicionRatonController.item = "";
                    }
                }
            }
            
        } else if (item == "")
        {
            item = posicionRatonController.item;
            cantidad = posicionRatonController.cantidad;
            GetComponent<Image>().sprite = posicionRaton.GetComponent<Image>().sprite;
            posicionRatonController.item = "";
            posicionRatonController.cantidad = 0;
            posicionRaton.GetComponent<Image>().sprite = null;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        //transform.position = new Vector2(transform.position.x, eventData.position.y);
    }
}
