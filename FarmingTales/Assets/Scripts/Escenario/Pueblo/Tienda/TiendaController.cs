using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TiendaController : MonoBehaviour
{
    public ProductoController productoComprar;

    public GameObject panelProductos;
    public GameObject panelElegirComprarVender;
    public GameObject comprar;
    public GameObject vender;

    public InventarioController inventarioController;

    public int cantidadProducto = 0;
    public int precioProducto = 0;
    public int cantidadMaximaProducto = 0;

    private void Start()
    {
        inventarioController = GameObject.Find("ToolBar").GetComponent<InventarioController>();
    }

    public void seleccionarProducto(ProductoController productoController)
    {
        productoComprar = productoController;

        panelProductos.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        panelElegirComprarVender.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        getDisponibilidadProductoComprar(productoController.id);
    }

    public void mostrarCompra()
    {
        panelElegirComprarVender.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        comprar.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
    
    public void mostrarVender()
    {
        panelElegirComprarVender.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        vender.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    public void sumarProducto()
    {
        if (cantidadProducto <= cantidadMaximaProducto && cantidadProducto < 128)
        {
            cantidadProducto++;
            precioProducto = cantidadProducto * productoComprar.getPrecioProducto();
        }
        
        if (precioProducto > int.Parse(inventarioController.oro.text))
        {
            comprar.transform.Find("TextoOroComprar").GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            comprar.transform.Find("TextoOroComprar").GetComponent<TextMeshProUGUI>().color = Color.white;
        }

        if (comprar.GetComponent<RectTransform>().localScale.x > 0)
        {
            comprar.transform.Find("TextoCantidadComprar").GetComponent<TextMeshProUGUI>().text =
                cantidadProducto.ToString();
            comprar.transform.Find("TextoOroComprar").GetComponent<TextMeshProUGUI>().text = precioProducto.ToString();
        } else if (vender.GetComponent<RectTransform>().localScale.x > 0)
        {
            vender.transform.Find("TextoCantidadVender").GetComponent<TextMeshProUGUI>().text =
                cantidadProducto.ToString();
        }
    }

    public void restarProducto()
    {
        if (cantidadProducto > 0)
        {
            cantidadProducto--;
            precioProducto = cantidadProducto * productoComprar.getPrecioProducto();
        }

        if (precioProducto > int.Parse(inventarioController.oro.text))
        {
            comprar.transform.Find("TextoOroComprar").GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            comprar.transform.Find("TextoOroComprar").GetComponent<TextMeshProUGUI>().color = Color.white;
        }

        if (comprar.GetComponent<RectTransform>().localScale.x > 0)
        {
            comprar.transform.Find("TextoCantidadComprar").GetComponent<TextMeshProUGUI>().text =
                cantidadProducto.ToString();
            comprar.transform.Find("TextoOroComprar").GetComponent<TextMeshProUGUI>().text = precioProducto.ToString();
        } else if (vender.GetComponent<RectTransform>().localScale.x > 0)
        {
            vender.transform.Find("TextoCantidadVender").GetComponent<TextMeshProUGUI>().text =
                cantidadProducto.ToString();
        }
    }

    public void comprarProducto()
    {
        if (precioProducto <= int.Parse(inventarioController.oro.text))
        {
            inventarioController.anadirInventario(productoComprar.id, cantidadProducto);
            comprar.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
            panelProductos.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            string oro = (int.Parse(inventarioController.oro.text) - precioProducto).ToString();
            inventarioController.oro.text = (oro);
        }
    }

    public void getDisponibilidadProductoComprar(string itemBuscar)
    {
        GameObject[] posicionesInventario = inventarioController.posiciones;

        for (int i = 0; i < posicionesInventario.Length; i++)
        {
            if (posicionesInventario[i].GetComponent<PosicionController>().item == itemBuscar)
            {
                cantidadMaximaProducto = cantidadMaximaProducto + (128 - posicionesInventario[i].GetComponent<PosicionController>().cantidad);
            } else if (posicionesInventario[i].GetComponent<PosicionController>().item == "")
            {
                cantidadMaximaProducto = cantidadMaximaProducto + 128;
            }
        }
    }
}
