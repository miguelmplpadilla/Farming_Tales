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
    public int cantidadVenta = 0;
    private int comprarVender = 1;

    private bool animalGranja = false;

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

    public void anadirDatos(string ide, string item, Sprite imagen, int cantidadPrecio, int compVend, bool animal)
    {
        id = ide;
        imagenProducto.sprite = imagen;
        nombreProducto.text = item;
        precio.text = cantidadPrecio.ToString();
        comprarVender = compVend;
        animalGranja = animal;
    }
    
    public void anadirDatos(string ide, string item, Sprite imagen, int cantidadPrecio, int cantVenta, int compVend, bool animal)
    {
        id = ide;
        imagenProducto.sprite = imagen;
        nombreProducto.text = item;
        precio.text = cantidadPrecio.ToString();
        cantidadVenta = cantVenta;
        comprarVender = compVend;
        animalGranja = animal;
    }

    public void selectProduct()
    {
        if (!animalGranja)
        {
            if (comprarVender == 1)
            {
                tiendaController.mostrarCompra(GetComponent<ProductoController>());
            }
            else if (comprarVender == 2)
            {
                tiendaController.mostrarVender(GetComponent<ProductoController>());
            }
        }
        else
        {
            if (comprarVender == 1)
            {
                tiendaController.mostrarComprarAnimal(GetComponent<ProductoController>());
            }
        }
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
