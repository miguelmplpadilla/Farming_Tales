using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PosicionadorItemController : MonoBehaviour
{
    public GameObject itemPosicionado;
    private GameObject mano;
    private GameObject posicionadorItem;

    private GameObject[] puntos;

    public string[] puntosOcupados;

    public PosicionController posicionController;

    private InventarioController inventarioController;

    private GameObject inventario;

    private void Start()
    {
        mano = GameObject.Find("Mano");
        posicionadorItem = GameObject.Find("GeneradorPosiciones");

        puntos = posicionadorItem.GetComponent<GeneradorPosicionamientoController>().puntos;

        puntosOcupados = new string[puntos.Length];

        inventarioController = GameObject.Find("ToolBar").GetComponent<InventarioController>();

        cargarPosicionesItem();
        
        inventario = GameObject.Find("Inventario");
        
        PlayerPrefs.DeleteAll();
    }

    private void Update()
    {

        if (GetComponent<SpriteRenderer>().sprite == null && itemPosicionado != null)
        {
            GetComponent<SpriteRenderer>().sprite = itemPosicionado.GetComponent<SpriteRenderer>().sprite;
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (itemPosicionado == null) {
            GetComponent<SpriteRenderer>().sprite = null;
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }
        
        GameObject punto = instanceNearest();

        Vector2 posicion = punto.transform.position;

        float distancia = Vector3.Distance(punto.transform.position,mano.transform.parent.position);

        if (distancia > 1f)
        {
            posicion = new Vector3(mano.transform.position.x, punto.transform.position.y);
        }
        
        transform.position = posicion;

        if (distancia <= 1f && punto.GetComponent<PuntoGeneradoController>().ocupado == false && inventario.GetComponent<RectTransform>().localScale.x == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (itemPosicionado != null)
                {
                    GameObject item = Instantiate(itemPosicionado);
                    item.transform.position = posicion;

                    punto.GetComponent<PuntoGeneradoController>().tipo = posicionController.item;
                    punto.GetComponent<PuntoGeneradoController>().ocupado = true;

                    posicionController.cantidad = posicionController.cantidad - 1;
                    if (posicionController.cantidad == 0)
                    {
                        posicionController.item = "";
                        itemPosicionado = null;
                        GetComponent<SpriteRenderer>().sprite = null;
                    }

                    guardarPosicionesItem();
                    inventarioController.guardarInventario();
                }
            }

            Color colorTemp = Color.green;
            colorTemp.a = 0.3f;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = colorTemp;
        } else
        {
            Color colorTemp = Color.red;
            colorTemp.a = 0.3f;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = colorTemp;
        }
    }

    public GameObject instanceNearest()
    {
        GameObject punto = puntos[0];

        float ultimaDistancia = Vector3.Distance(punto.transform.position, mano.transform.position);
        
        for (int i = 0; i < puntos.Length; i++)
        {
            float distancia = Vector3.Distance(puntos[i].transform.position, mano.transform.position);

            if (distancia < Vector3.Distance(punto.transform.position, mano.transform.position))
            {
                punto = puntos[i];
            }
        }

        return punto;
    }

    public void guardarPosicionesItem()
    {
        for (int i = 0; i < puntos.Length; i++)
        {
            PlayerPrefs.SetString("punto"+i+SceneManager.GetActiveScene().name, puntos[i].GetComponent<PuntoGeneradoController>().tipo);
        }
        
        PlayerPrefs.Save();
    }

    public void cargarPosicionesItem()
    {
        for (int i = 0; i < puntos.Length; i++)
        {
            puntos[i].GetComponent<PuntoGeneradoController>().tipo =
                PlayerPrefs.GetString("punto" + i + SceneManager.GetActiveScene().name);

            if (puntos[i].GetComponent<PuntoGeneradoController>().tipo != "")
            {
                puntos[i].GetComponent<PuntoGeneradoController>().ocupado = true;
                
                GameObject item = Resources.Load("Prefabs/Instancias/"+puntos[i].GetComponent<PuntoGeneradoController>().tipo) as GameObject;

                GameObject itemGenerado = Instantiate(item);

                itemGenerado.transform.position = puntos[i].transform.position;
                
                itemGenerado.SendMessage("setId", puntos[i].GetComponent<PuntoGeneradoController>().tipo + i + SceneManager.GetActiveScene().name);
            }
        }
    }
}
