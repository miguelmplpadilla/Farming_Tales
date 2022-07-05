using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacaController : MonoBehaviour
{
    public GameObject granja;

    public string tipo = "vaca";
    
    public int tiempoLeche = 300;
    public int tiempoLecheTranscurrido = 0;

    public bool alimentado = false;

    public int lecheAnadir = 0;

    private GameObject inventarioGranja;

    void Start()
    {
        inventarioGranja = GameObject.Find("InventarioGranja");
        StartCoroutine("contadorLeche");
    }

    private void Update()
    {
        if (inventarioGranja.GetComponent<RectTransform>().localScale.x <= 0)
        {
            if (lecheAnadir > 0)
            {
                granja.GetComponent<GranjaController>().guardarItemGranja("leche", lecheAnadir);
                lecheAnadir = 0;
            }
        }
    }

    IEnumerator contadorLeche()
    {
        while (true)
        {
            if (alimentado)
            {
                for (int i = 0; i < tiempoLeche; i++)
                {
                    yield return new WaitForSeconds(1f);
                    tiempoLecheTranscurrido++;
                }
                ponerCuboLeche();
                tiempoLecheTranscurrido = 0;
                yield return null;
            }
            else
            {
                comer();
                yield return null;
            }
        }
    }

    public void ponerCuboLeche()
    {
        lecheAnadir++;
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

    public string getTipe()
    {
        return tipo;
    }
}
