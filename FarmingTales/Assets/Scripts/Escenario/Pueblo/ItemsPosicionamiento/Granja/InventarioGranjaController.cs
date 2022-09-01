using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventarioGranjaController : MonoBehaviour
{
    
    public GameObject[] posiciones;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public int anadirInventario(string tipo, int cantidad)
    {
        
        for (int i = 0; i < posiciones.Length; i++)
        {
            if (posiciones[i].GetComponent<PosicionController>().item == tipo)
            {
                if ((posiciones[i].GetComponent<PosicionController>().cantidad+cantidad) < 128)
                {
                    posiciones[i].GetComponent<PosicionController>().cantidad = posiciones[i].GetComponent<PosicionController>().cantidad + cantidad;
                    posiciones[i].GetComponentInChildren<TextMeshProUGUI>().text =
                        posiciones[i].GetComponent<PosicionController>().cantidad.ToString();
                    cantidad = 0;
                    break;
                }
                else
                {
                    int cantAnterior = posiciones[i].GetComponent<PosicionController>().cantidad;
                    posiciones[i].GetComponent<PosicionController>().cantidad = 128;
                    posiciones[i].GetComponentInChildren<TextMeshProUGUI>().text =
                        posiciones[i].GetComponent<PosicionController>().cantidad.ToString();
                    cantidad = (cantAnterior + cantidad) - 128;
                }
            }
        }

        if (cantidad > 0)
        {
            for (int i = 0; i < posiciones.Length; i++)
            {
                if (posiciones[i].GetComponent<PosicionController>().item == "")
                {
                    posiciones[i].GetComponent<PosicionController>().item = tipo;
                    if (cantidad < 128)
                    {
                        posiciones[i].GetComponent<PosicionController>().cantidad = cantidad;
                        posiciones[i].GetComponentInChildren<TextMeshProUGUI>().text =
                            posiciones[i].GetComponent<PosicionController>().cantidad.ToString();
                        cantidad = 0;
                        break;
                    }
                    else
                    {
                        posiciones[i].GetComponent<PosicionController>().cantidad = 128;
                        posiciones[i].GetComponentInChildren<TextMeshProUGUI>().text =
                            posiciones[i].GetComponent<PosicionController>().cantidad.ToString();
                        cantidad = cantidad - 128;
                    }
                }
            }
        }

        return cantidad;
    }

    public void cerrarGranja()
    {
        player.GetComponent<PlayerController>().mov = true;

        transform.localScale = new Vector3(0, 1, 1);

        GameObject.Find("Inventario").transform.localScale = new Vector3(0, 1, 1);

    }
    
}
