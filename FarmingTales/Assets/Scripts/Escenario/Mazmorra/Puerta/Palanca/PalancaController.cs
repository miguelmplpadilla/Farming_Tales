using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PalancaController : MonoBehaviour
{
    public Animator[] puertas;
    private CinemachineVirtualCamera camara;

    private GameObject player;

    private bool puertasAbiertas = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        camara = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
    }

    public void inter()
    {
        player.GetComponent<PlayerController>().mov = false;
        StartCoroutine("abrirPuertas");
    }

    IEnumerator abrirPuertas()
    {
        for (int i = 0; i < puertas.Length; i++)
        {
            camara.Follow = puertas[i].transform;
            yield return new WaitForSeconds(0.1f);
            if (!puertasAbiertas)
            {
                puertas[i].SetTrigger("abrir");
            }
            else
            {
                puertas[i].SetTrigger("cerrar");
            }

            yield return new WaitForSeconds(0.5f);
        }

        player.GetComponent<PlayerController>().mov = true;
        camara.Follow = player.transform;
    }
    
    public void mostrarInter()
    {
        GetComponentInChildren<InteractuarUIController>().visible();
    }

    public void esconderInter()
    {
        GetComponentInChildren<InteractuarUIController>().esconder();
    }
    
}
