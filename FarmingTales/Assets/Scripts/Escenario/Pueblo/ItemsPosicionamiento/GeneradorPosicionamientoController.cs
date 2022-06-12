using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorPosicionamientoController : MonoBehaviour
{
    private Vector2 puntoGeneracion;

    public int numeroPuntos = 30;

    public GameObject punto;

    public GameObject[] puntos;

    private void Awake()
    {
        puntos = new GameObject[numeroPuntos];

        puntoGeneracion = new Vector2(11f,0f);
    }

    private void Start()
    {
        for (int i = 0; i < numeroPuntos; i++)
        {
            puntos[i] = Instantiate(punto);

            puntos[i].transform.position = puntoGeneracion;

            puntoGeneracion = new Vector2(puntoGeneracion.x + 0.7f, puntoGeneracion.y);
        }
    }
}
