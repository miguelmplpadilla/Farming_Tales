using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventarioController : MonoBehaviour
{

    public GameObject[] posiciones = new GameObject[4];
    public GameObject[] posicionesInventario = new GameObject[4];
    public TextMeshProUGUI oro;
    private GameObject inventario;
    private RectTransform rectTransformInventario;
    private RectTransform rectTransformRaton;
    private PosicionRatonController posicionRatonController;
    private ToolBarController toolBarController;

    public Sprite spritePrueba;
    

    public bool cofreAbierto = false;

    private void Start()
    {
        oro = GameObject.Find("TextoOro").GetComponent<TextMeshProUGUI>();
        inventario = GameObject.Find("Inventario");
        rectTransformInventario = inventario.GetComponent<RectTransform>();
        rectTransformRaton = GameObject.Find("Raton").GetComponent<RectTransform>();
        
        anadirInventario("cofre", spritePrueba, 1, null);

        posicionRatonController = GameObject.Find("PosRaton").GetComponent<PosicionRatonController>();

        toolBarController = GetComponent<ToolBarController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (rectTransformInventario.localScale.x == 0)
            {
                rectTransformInventario.localScale = new Vector3(1, 1, 1);
                rectTransformRaton.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                if (posicionRatonController.item != "" && posicionRatonController.cantidad > 0)
                {
                    int resto = anadirInventario(posicionRatonController.item, posicionRatonController.GetComponent<Image>().sprite, posicionRatonController.cantidad, posicionRatonController.gameObject);

                    posicionRatonController.item = "";
                    posicionRatonController.cantidad = 0;
                    posicionRatonController.GetComponent<Image>().sprite = null;
                    
                    if (cofreAbierto)
                    {
                        // Aqui se añadiria el resto del inventario al cofre que estamos cerrando
                    }
                }
                rectTransformInventario.localScale = new Vector3(0, 1, 1);
                rectTransformRaton.localScale = new Vector3(0, 1, 1);
            }
        }
    }

    public int anadirInventario(string tipo, Sprite sprite, int cantidad, GameObject other)
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
                        posiciones[i].GetComponent<Image>().sprite = sprite;
                        break;
                    }
                    else
                    {
                        posiciones[i].GetComponent<PosicionController>().cantidad = 128;
                        posiciones[i].GetComponentInChildren<TextMeshProUGUI>().text =
                            posiciones[i].GetComponent<PosicionController>().cantidad.ToString();
                        cantidad = cantidad - 128;
                        posiciones[i].GetComponent<Image>().sprite = sprite;
                    }
                }
            }
        }

        return cantidad;
    }
    
    public int anadirSoloInventario(string tipo, Sprite sprite, int cantidad, GameObject other)
    {
        for (int i = 0; i < posicionesInventario.Length; i++)
        {
            if (!posicionesInventario[i].Equals(other))
            {
                if (posicionesInventario[i].GetComponent<PosicionController>().item == tipo)
                {
                    if ((posicionesInventario[i].GetComponent<PosicionController>().cantidad+cantidad) < 128)
                    {
                        posicionesInventario[i].GetComponent<PosicionController>().cantidad = posicionesInventario[i].GetComponent<PosicionController>().cantidad + cantidad;
                        posicionesInventario[i].GetComponentInChildren<TextMeshProUGUI>().text =
                            posicionesInventario[i].GetComponent<PosicionController>().cantidad.ToString();
                        cantidad = 0;
                        break;
                    }
                    else
                    {
                        int cantAnterior = posicionesInventario[i].GetComponent<PosicionController>().cantidad;
                        posicionesInventario[i].GetComponent<PosicionController>().cantidad = 128;
                        posicionesInventario[i].GetComponentInChildren<TextMeshProUGUI>().text =
                            posicionesInventario[i].GetComponent<PosicionController>().cantidad.ToString();
                        cantidad = (cantAnterior + cantidad) - 128;
                    }
                }
            }
        }

        if (cantidad > 0)
        {
            for (int i = 0; i < posicionesInventario.Length; i++)
            {
                if (!posicionesInventario[i].Equals(other))
                {
                    if (posicionesInventario[i].GetComponent<PosicionController>().item == "")
                    {
                        posicionesInventario[i].GetComponent<PosicionController>().item = tipo;
                        if (cantidad < 128)
                        {
                            posicionesInventario[i].GetComponent<PosicionController>().cantidad = cantidad;
                            posicionesInventario[i].GetComponentInChildren<TextMeshProUGUI>().text =
                                posicionesInventario[i].GetComponent<PosicionController>().cantidad.ToString();
                            cantidad = 0;
                            posicionesInventario[i].GetComponent<Image>().sprite = sprite;
                            break;
                        }
                        else
                        {
                            posicionesInventario[i].GetComponent<PosicionController>().cantidad = 128;
                            posicionesInventario[i].GetComponentInChildren<TextMeshProUGUI>().text =
                                posicionesInventario[i].GetComponent<PosicionController>().cantidad.ToString();
                            cantidad = cantidad - 128;
                            posicionesInventario[i].GetComponent<Image>().sprite = sprite;
                        }
                    }
                }
            }
        }

        return cantidad;
    }

    public void anadirDinero(int cantidad)
    {
        int cantidadOro = int.Parse(oro.text);

        oro.text = (cantidadOro + cantidad).ToString();
    }
}
