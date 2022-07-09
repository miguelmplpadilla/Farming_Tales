using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductoController : MonoBehaviour
{

    private Image imagenProducto;
    private TextMeshProUGUI nombreProducto;
    private TextMeshProUGUI precio;
    public string id;

    private CreadorProductosController creadorProductosController;
    private TiendaController tiendaController;

    private void Awake()
    {
        imagenProducto = transform.Find("MarcoImagenProducto").transform.Find("ImagenProducto").GetComponent<Image>();
        nombreProducto = transform.Find("NombreProductoTexto").GetComponent<TextMeshProUGUI>();
        precio = transform.Find("PrecioTexto").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        tiendaController = GameObject.Find("TiendaController").GetComponent<TiendaController>();
        creadorProductosController = GameObject.Find("CreadorProductos").GetComponent<CreadorProductosController>();
    }

    public void anadirDatos(string ide, string item, Sprite imagen, int cantidadPrecio)
    {
        id = ide;
        imagenProducto.sprite = imagen;
        nombreProducto.text = item;
        precio.text = cantidadPrecio.ToString();
    }

    public void selectProduct()
    {
        tiendaController.seleccionarProducto(GetComponent<ProductoController>());
    }

    public Sprite getImagenProducto()
    {
        return imagenProducto.sprite;
    }

    public string getNombreProducto()
    {
        return nombreProducto.text;
    }

    public int getPrecioProducto()
    {
        return int.Parse(precio.text);
    }
    
}
