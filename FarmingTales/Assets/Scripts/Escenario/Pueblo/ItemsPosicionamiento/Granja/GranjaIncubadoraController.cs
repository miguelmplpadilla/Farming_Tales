using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranjaIncubadoraController : MonoBehaviour
{
    
    public string item = "";
    public int cantidad = 0;
    
    public int tiempoIncubacion = 600;
    public int tiempoIncubacionTranscurrido = 0;

    private GameObject granja;

    public bool incubando;

    private void Awake()
    {
        granja = gameObject;
    }

    IEnumerator incubar()
    {

        for (int i = 0; i < tiempoIncubacion; i++)
        {
            tiempoIncubacionTranscurrido++;
            yield return new WaitForSeconds(1f);
        }

        item = "";
        cantidad = 0;

        GameObject gallina = Instantiate(Resources.Load("Prefabs/Instancias/Animales/gallina") as GameObject);
        granja.GetComponent<GranjaController>().animales.Add(gallina);
        gallina.GetComponent<GallinaController>().granja = granja;
        gallina.transform.position = transform.position;

        incubando = false;
        tiempoIncubacionTranscurrido = 0;

        yield return null;
    }

    public void startIncubar()
    {
        StartCoroutine("incubar");
    }

    public void stopIncubar()
    {
        StopCoroutine("incubar");
    }
}
