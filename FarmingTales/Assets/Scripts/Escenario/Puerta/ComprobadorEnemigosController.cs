using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComprobadorEnemigosController : MonoBehaviour
{

    public GameObject[] enemigos;
    
    public Animator[] puertas;

    private bool puertasAbiertas = true;
    private bool enemigosCreados = false;
    
    void Start()
    {
        for (int i = 0; i < enemigos.Length; i++)
        {
            enemigos[i].SendMessage("stopEnemigo");
        }
    }

    private void LateUpdate()
    {
        bool existenEnemigos = false;
        for (int i = 0; i < enemigos.Length; i++)
        {
            if (enemigos[i] != null)
            {
                existenEnemigos = true;
            }
        }

        if (!existenEnemigos && !puertasAbiertas)
        {
            for (int i = 0; i < puertas.Length; i++)
            {
                puertas[i].SetTrigger("abrir");
            }

            puertasAbiertas = true;
        }
    }

    private void crearEnemigos()
    {
        for (int i = 0; i < enemigos.Length; i++)
        {
            enemigos[i].SendMessage("startEnemigo");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("player"))
        {
            if (!enemigosCreados)
            {
                for (int i = 0; i < puertas.Length; i++)
                {
                    puertas[i].SetTrigger("cerrar");
                }
            
                crearEnemigos();
            
                puertasAbiertas = false;
                enemigosCreados = true;
            }
        }
    }
}
