using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionadorItemController : MonoBehaviour
{
    public GameObject intemPosicionado;
    private GameObject player;
    private GameObject posicionadorItem;

    private GameObject[] puntos;

    public string[] puntosOcupados;

    private void Start()
    {
        player = GameObject.Find("Player");
        posicionadorItem = GameObject.Find("GeneradorPosiciones");

        puntos = posicionadorItem.GetComponent<GeneradorPosicionamientoController>().puntos;

        puntosOcupados = new string[puntos.Length];
    }

    private void Update()
    {
        Vector3 punto = instanceNearest().transform.position;

        transform.position = punto;
    }

    public GameObject instanceNearest()
    {
        GameObject punto = puntos[0];

        float ultimaDistancia = Vector3.Distance(punto.transform.position, player.transform.position);
        
        for (int i = 0; i < puntos.Length; i++)
        {
            float distancia = Vector3.Distance(puntos[i].transform.position, player.transform.position);

            if (distancia < ultimaDistancia)
            {
                punto = puntos[i];
            }
        }

        return punto;
    }
}
