using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionadorItemController : MonoBehaviour
{
    public GameObject itemPosicionado;
    private GameObject mano;
    private GameObject posicionadorItem;

    private GameObject[] puntos;

    public string[] puntosOcupados;

    public PosicionController posicionController;

    private void Start()
    {
        mano = GameObject.Find("Mano");
        posicionadorItem = GameObject.Find("GeneradorPosiciones");

        puntos = posicionadorItem.GetComponent<GeneradorPosicionamientoController>().puntos;

        puntosOcupados = new string[puntos.Length];
    }

    private void Update()
    {

        if (GetComponent<SpriteRenderer>().sprite == null && itemPosicionado != null)
        {
            GetComponent<SpriteRenderer>().sprite = itemPosicionado.GetComponent<SpriteRenderer>().sprite;
        }
        
        Vector3 punto = instanceNearest().transform.position;

        transform.position = punto;

        if (Input.GetButtonDown("Fire1"))
        {
            if (itemPosicionado != null)
            {
                GameObject item = Instantiate(itemPosicionado);
                item.transform.position = punto;
                itemPosicionado = null;
                GetComponent<SpriteRenderer>().sprite = null;

                posicionController.cantidad = 0;
                posicionController.item = "";
            }
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
}
