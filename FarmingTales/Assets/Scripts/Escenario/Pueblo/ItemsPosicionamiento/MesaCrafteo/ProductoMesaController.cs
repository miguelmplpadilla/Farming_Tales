using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductoMesaController : MonoBehaviour
{

    public string nombreProductoMesa = "";

    private InventarioController inventarioController;

    public class Materiales
    {
        public string nombre;
        public int cantidad;
    }

    public List<Materiales> materiales = new List<Materiales>();
    
    private void Start()
    {
        inventarioController = GameObject.Find("ToolBar").GetComponent<InventarioController>();
    }

    public void fabricar()
    {
        if (comprobarMaterial())
        {
            restarMateriales();

            int sobra = inventarioController.anadirInventario(nombreProductoMesa, 1);

            if (sobra > 0)
            {
                // Añadir a cofre exterior
                Debug.Log("No hay suficiente espacio en el inventario");
            }
        }
    }

    public bool comprobarMaterial()
    {
        bool seguir = true;
        for (int i = 0; i < materiales.Count; i++)
        {
            int cantidadSeguir = 0;
            for (int j = 0; j < inventarioController.posiciones.Length; j++)
            {
                if (inventarioController.posiciones[j].GetComponent<PosicionController>().item.Equals(materiales[i].nombre))
                {
                    if (inventarioController.posiciones[j].GetComponent<PosicionController>().cantidad >= materiales[i].cantidad)
                    {
                        cantidadSeguir = materiales[i].cantidad;
                        break;
                    } else if (inventarioController.posiciones[j].GetComponent<PosicionController>().cantidad < materiales[i].cantidad)
                    {
                        cantidadSeguir += inventarioController.posiciones[j].GetComponent<PosicionController>()
                            .cantidad;
                    }
                }
            }

            if (cantidadSeguir >= materiales[i].cantidad)
            {
                seguir = false;
                break;
            }
            else
            {
                cantidadSeguir = 0;
            }
        }

        return seguir;
    }

    public void restarMateriales()
    {
        
    }

}
