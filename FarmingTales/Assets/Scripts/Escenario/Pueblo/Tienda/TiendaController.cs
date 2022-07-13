using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TiendaController : MonoBehaviour
{
    public ProductoController productoComprar;

    public GameObject panelProductos;
    public GameObject comprar;
    public GameObject vender;

    public Sprite imagenTiendaComprar;
    public Sprite imagenTiendaVender;

    private CreadorProductosController creadorProductosController;

    public InventarioController inventarioController;

    public int cantidadProducto = 0;
    public int precioProducto = 0;
    public int cantidadMaximaProducto = 0;

    private void Start()
    {
        inventarioController = GameObject.Find("ToolBar").GetComponent<InventarioController>();
        creadorProductosController = GameObject.Find("CreadorProductos").GetComponent<CreadorProductosController>();
    }

    public void mostrarCompra(ProductoController proController)
    {
        productoComprar = proController;
        panelProductos.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        comprar.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        vender.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);

        transform.parent.GetComponent<Image>().sprite = imagenTiendaComprar;
    }
    
    public void mostrarVender(ProductoController proController)
    {
        productoComprar = proController;
        panelProductos.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        vender.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        comprar.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        
        transform.parent.GetComponent<Image>().sprite = imagenTiendaVender;
    }
    
    public void mostrarListaComprar() {
        comprar.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        vender.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        
        panelProductos.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        
        creadorProductosController.listaComprar();
        
        transform.parent.GetComponent<Image>().sprite = imagenTiendaComprar;
    }

    public void mostrarListaVender()
    {
        vender.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        comprar.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        
        panelProductos.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        
        creadorProductosController.listaVender();
        
        transform.parent.GetComponent<Image>().sprite = imagenTiendaVender;
    }
    
    public void sumarProductoVender()
    {
        if (cantidadProducto < productoComprar.cantidadVenta && cantidadProducto < 128)
        {
            cantidadProducto++;
            precioProducto = cantidadProducto * productoComprar.getPrecioProducto();
        }
        
        vender.transform.Find("TextoCantidadVender").GetComponent<TextMeshProUGUI>().text =
            cantidadProducto.ToString();
        vender.transform.Find("TextoOroVender").GetComponent<TextMeshProUGUI>().text = precioProducto.ToString();
        
    }

    public void restarProductoVender()
    {
        if (cantidadProducto > 0)
        {
            cantidadProducto--;
            precioProducto = cantidadProducto * productoComprar.getPrecioProducto();
        }
        
        vender.transform.Find("TextoCantidadVender").GetComponent<TextMeshProUGUI>().text =
            cantidadProducto.ToString();
        vender.transform.Find("TextoOroVender").GetComponent<TextMeshProUGUI>().text = precioProducto.ToString();
        
    }

    public void sumarProducto()
    {
        if (cantidadProducto < cantidadMaximaProducto && cantidadProducto < 128)
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

        comprar.transform.Find("TextoCantidadComprar").GetComponent<TextMeshProUGUI>().text =
            cantidadProducto.ToString();
        comprar.transform.Find("TextoOroComprar").GetComponent<TextMeshProUGUI>().text = precioProducto.ToString();
    }

    public void comprarProducto()
    {
        if (precioProducto <= int.Parse(inventarioController.oro.text) && cantidadProducto < cantidadMaximaProducto && cantidadProducto < 128)
        {
            inventarioController.anadirInventario(productoComprar.id, cantidadProducto);
            comprar.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
            panelProductos.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            string oro = (int.Parse(inventarioController.oro.text) - precioProducto).ToString();
            inventarioController.oro.text = (oro);
            
            comprar.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
            
            cantidadProducto = 0;
            precioProducto = 0;
            comprar.transform.Find("TextoCantidadComprar").GetComponent<TextMeshProUGUI>().text =
                cantidadProducto.ToString();
            comprar.transform.Find("TextoOroComprar").GetComponent<TextMeshProUGUI>().text = precioProducto.ToString();
            
            mostrarListaComprar();
        }
    }

    public void venderProducto()
    {
        if (cantidadProducto <= productoComprar.cantidadVenta && cantidadProducto < 128)
        {
            for (int i = 0; i < inventarioController.posiciones.Length; i++)
            {
                if (inventarioController.posiciones[i].GetComponent<PosicionController>().item == productoComprar.id)
                {
                    if (inventarioController.posiciones[i].GetComponent<PosicionController>().cantidad > cantidadProducto)
                    {
                        inventarioController.posiciones[i].GetComponent<PosicionController>().cantidad =
                            inventarioController.posiciones[i].GetComponent<PosicionController>().cantidad -
                            cantidadProducto;
                        if (inventarioController.posiciones[i].GetComponent<PosicionController>().cantidad == 0)
                        {
                            inventarioController.posiciones[i].GetComponent<PosicionController>().item = "";
                        }
                        break;
                    }
                    else
                    {
                        cantidadProducto = cantidadProducto -
                                           inventarioController.posiciones[i].GetComponent<PosicionController>().cantidad;
                        inventarioController.posiciones[i].GetComponent<PosicionController>().cantidad = 0;
                        inventarioController.posiciones[i].GetComponent<PosicionController>().item = "";
                    }
                }
            }
        
            inventarioController.oro.text = ((int.Parse(inventarioController.oro.text) + precioProducto).ToString());
        
            vender.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
            
            cantidadProducto = 0;
            precioProducto = 0;
            vender.transform.Find("TextoCantidadVender").GetComponent<TextMeshProUGUI>().text =
                cantidadProducto.ToString();
            vender.transform.Find("TextoOroVender").GetComponent<TextMeshProUGUI>().text = precioProducto.ToString();
            
            mostrarListaVender();
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
