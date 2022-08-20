using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProductoMesaController : MonoBehaviour
{

    public string nombreProductoMesa = "";

    private InventarioController inventarioController;

    public List<Materiales> materiales = new List<Materiales>();
    
    private void Start()
    {
        inventarioController = GameObject.Find("ToolBar").GetComponent<InventarioController>();
    }

    public void anadirDatos(string name, List<Materiales> materials)
    {
        nombreProductoMesa = name;
        materiales = materials;

        gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = nombreProductoMesa;
    }

    public void fabricar()
    {
        Debug.Log("Frabricando");
        if (comprobarMaterial())
        {
            Debug.Log("Estoy fabricando");
            if (inventarioController.comprobarEspacio(nombreProductoMesa, 1))
            {
                restarMateriales();

                inventarioController.anadirInventario(nombreProductoMesa, 1);
            }
            else
            {
                // Mostrar al jugador que no hay espacio
                Debug.Log("No hay espacio suficiente");
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
        for (int i = 0; i < materiales.Count; i++)
        {
            int cantidadARestar = materiales[i].cantidad;
            for (int j = 0; j < inventarioController.posiciones.Length; j++)
            {
                if (inventarioController.posiciones[j].GetComponent<PosicionController>().item.Equals(materiales[i].nombre))
                {
                    if (inventarioController.posiciones[j].GetComponent<PosicionController>().cantidad == cantidadARestar)
                    {
                        inventarioController.posiciones[j].GetComponent<PosicionController>().cantidad = 0;
                        inventarioController.posiciones[j].GetComponent<PosicionController>().item = "";
                        cantidadARestar = 0;
                        break;
                    } else if (inventarioController.posiciones[j].GetComponent<PosicionController>().cantidad < cantidadARestar)
                    {
                        cantidadARestar -= inventarioController.posiciones[j].GetComponent<PosicionController>()
                            .cantidad;
                        inventarioController.posiciones[j].GetComponent<PosicionController>().cantidad = 0;
                        inventarioController.posiciones[j].GetComponent<PosicionController>().item = "";
                    } else if (inventarioController.posiciones[j].GetComponent<PosicionController>().cantidad > cantidadARestar)
                    {
                        inventarioController.posiciones[j].GetComponent<PosicionController>().cantidad -= cantidadARestar;
                        cantidadARestar = 0;
                        break;
                    }
                }
            }
        }
    }

}
