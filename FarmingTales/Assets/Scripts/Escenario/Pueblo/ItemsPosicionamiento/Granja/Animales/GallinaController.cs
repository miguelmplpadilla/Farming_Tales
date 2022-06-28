using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GallinaController : MonoBehaviour
{

    public GameObject granja;
    
    public int tiempoPoner = 200;
    public int tiempoPonerTranscurrido = 0;

    public bool alimentado = false;

    public int huevosAnadir = 0;

    private GameObject inventarioGranja;

    void Start()
    {
        inventarioGranja = GameObject.Find("InventarioGranja");
        StartCoroutine("contadorPonerHuevo");
    }

    private void Update()
    {
        if (inventarioGranja.GetComponent<RectTransform>().localScale.x <= 0)
        {
            if (huevosAnadir > 0)
            {
                granja.GetComponent<GranjaController>().guardarItemGranja("huevo", huevosAnadir);
                huevosAnadir = 0;
            }
        }
    }

    IEnumerator contadorPonerHuevo()
    {
        while (true)
        {
            if (alimentado)
            {
                for (int i = 0; i < tiempoPoner; i++)
                {
                    yield return new WaitForSeconds(1f);
                    tiempoPonerTranscurrido++;
                }
                ponerHuevo();
                tiempoPonerTranscurrido = 0;
                yield return null;
            }
            else
            {
                comer();
                yield return null;
            }
        }
    }

    public void ponerHuevo()
    {
        huevosAnadir++;
        alimentado = false;
    }

    public void comer()
    {
        if (granja.GetComponent<GranjaController>().porcentageComida-2 >= 0)
        {
            granja.GetComponent<GranjaController>().porcentageComida =
                granja.GetComponent<GranjaController>().porcentageComida - 2;

            alimentado = true;
        }
        else
        {
            alimentado = false;
        }
    }
    
}
