using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EliminadorObjetosController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    private bool eliminar;
    private PosicionRatonController posicionRatonController;
    private Image image;
    private GameObject inventario;

    public Sprite[] imagenes;

    private void Start()
    {
        posicionRatonController = GameObject.Find("PosRaton").GetComponent<PosicionRatonController>();
        image = GetComponent<Image>();
        inventario = GameObject.Find("Inventario");
    }

    private void Update()
    {
        if (inventario.GetComponent<RectTransform>().localScale.x >= 1)
        {
            GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
        else
        {
            GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eliminar)
        {
            if (!posicionRatonController.item.Equals(""))
            {
                posicionRatonController.item = "";
                posicionRatonController.cantidad = 0;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!posicionRatonController.item.Equals(""))
        {
            image.sprite = imagenes[1];
        }

        eliminar = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = imagenes[0];
        eliminar = false;
    }
}
