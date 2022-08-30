using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GallinaController : MonoBehaviour
{

    public GameObject granja;
    public string tipo = "gallina";
    public string id = "";
    
    public int tiempoPoner = 200;
    public int tiempoPonerTranscurrido = 0;

    public bool alimentado = false;

    public int huevosAnadir = 0;
    public int huevoFecundadoAnadir = 0;

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
                guardarPartida();
            }

            if (huevoFecundadoAnadir > 0)
            {
                granja.GetComponent<GranjaController>().guardarItemGranja("huevoFecundado", huevoFecundadoAnadir);
                huevoFecundadoAnadir = 0;
                guardarPartida();
            }
        }
    }

    IEnumerator contadorPonerHuevo()
    {
        while (true)
        {
            if (alimentado)
            {
                for (int i = tiempoPonerTranscurrido; i < tiempoPoner; i++)
                {
                    yield return new WaitForSeconds(1f);
                    tiempoPonerTranscurrido++;
                    guardarPartida();
                }
                ponerHuevo();
                tiempoPonerTranscurrido = 0;
                guardarPartida();
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
        Random r = new Random();
        int num = r.Next(100);

        if (granja.GetComponent<GranjaController>().cantidadAnimales[0] >= 2)
        {
            if (num > 90 && num <= 100)
            {
                huevoFecundadoAnadir++;
            }
            else
            {
                huevosAnadir++;
            }
        }
        else
        {
            huevosAnadir++;
        }

        alimentado = false;
        guardarPartida();
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
        guardarPartida();
    }
    
    public string getTipe()
    {
        return tipo;
    }

    public void setId(string ide)
    {
        id = ide;
        cargarPartida();
    }

    private void cargarPartida()
    {
        if (PlayerPrefs.HasKey(id+"TiempoPonerTranscurrido"))
        {
            tiempoPonerTranscurrido = int.Parse(PlayerPrefs.GetString(id + "TiempoPonerTranscurrido"));
            huevosAnadir = int.Parse(PlayerPrefs.GetString(id + "HuevosAnadir"));
            huevoFecundadoAnadir = int.Parse(PlayerPrefs.GetString(id + "HuevosFecundadosAnadir"));
            if (PlayerPrefs.GetString(id + "Alimentado").Equals("1"))
            {
                alimentado = true;
            }
            else
            {
                alimentado = false;
            }
        }
    }

    public void setGranja(GameObject g)
    {
        granja = g;
    }

    private void guardarPartida()
    {
        PlayerPrefs.SetString(id+"TiempoPonerTranscurrido", tiempoPonerTranscurrido.ToString());
        PlayerPrefs.SetString(id+"HuevosAnadir", huevosAnadir.ToString());
        PlayerPrefs.SetString(id+"HuevosFecundadosAnadir", huevoFecundadoAnadir.ToString());
        if (alimentado)
        {
            PlayerPrefs.SetString(id+"Alimentado", "1");
        }
        else
        {
            PlayerPrefs.SetString(id+"Alimentado", "2");
        }
        PlayerPrefs.Save();
    }
    
}
