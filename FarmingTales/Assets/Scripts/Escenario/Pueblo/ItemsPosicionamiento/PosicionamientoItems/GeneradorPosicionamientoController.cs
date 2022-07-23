using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneradorPosicionamientoController : MonoBehaviour
{
    private Vector2 puntoGeneracion;

    public int numeroPuntos = 30;

    public GameObject punto;

    public GameObject[] puntos;

    private void Awake()
    {
        puntos = new GameObject[numeroPuntos];

        puntoGeneracion = new Vector2(15f,0f);
        
        for (int i = 0; i < numeroPuntos; i++)
        {
            puntos[i] = Instantiate(punto);

            puntos[i].transform.position = puntoGeneracion;
            puntos[i].GetComponent<PuntoGeneradoController>().posicion = i;

            puntoGeneracion = new Vector2(puntoGeneracion.x + 0.7f, puntoGeneracion.y);
        }
    }
}
