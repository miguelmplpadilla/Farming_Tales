using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranjaIncubadoraController : MonoBehaviour
{
    
    public string item = "";
    public int cantidad = 0;
    public string id = "";
    
    public int tiempoIncubacion = 600;
    public int tiempoIncubacionTranscurrido = 0;

    private GameObject granja;

    public bool incubando = false;

    private void Awake()
    {
        granja = gameObject;
    }

    private void Start()
    {
        id = GetComponent<GranjaController>().id + "Incubadora";
        
        cargarIncubadora();
    }

    IEnumerator incubar()
    {
        incubando = true;
        guardarIncubadora();
        for (int i = tiempoIncubacionTranscurrido; i < tiempoIncubacion; i++)
        {
            tiempoIncubacionTranscurrido++;
            yield return new WaitForSeconds(1f);
            guardarIncubadora();
        }

        item = "";
        cantidad = 0;

        granja.SendMessage("anadirAnimal", "gallina");

        incubando = false;
        tiempoIncubacionTranscurrido = 0;
        guardarIncubadora();

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
    
    public void guardarIncubadora()
    {
        if (incubando)
        {
            PlayerPrefs.SetString(id+"TieneHuevo", "1");
        }
        else
        {
            PlayerPrefs.SetString(id+"TieneHuevo", "0");
        }
        
        PlayerPrefs.SetInt(id+"TiempoIncubacionTranscurrido", tiempoIncubacionTranscurrido);
        PlayerPrefs.Save();
    }

    public void cargarIncubadora()
    {
        if (PlayerPrefs.HasKey(id+"TieneHuevo"))
        {
            if (PlayerPrefs.GetString(id+"TieneHuevo").Equals("1"))
            {
                incubando = true;
                item = "huevoFecundado";
                cantidad = 1;
                tiempoIncubacionTranscurrido = PlayerPrefs.GetInt(id + "TiempoIncubacionTranscurrido");
                startIncubar();
            }
        }
    }
}
