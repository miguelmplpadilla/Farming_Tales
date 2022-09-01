using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacaController : MonoBehaviour
{
    public GameObject granja;

    public string tipo = "vaca";
    public string id = "";
    
    public int tiempoLeche = 300;
    public int tiempoLecheTranscurrido = 0;

    public bool alimentado = false;

    public int lecheAnadir = 0;

    private GameObject inventarioGranja;

    public int contadorTiempoTranscurridoJuegoVaca = 0;

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
                for (int i = tiempoLecheTranscurrido; i < tiempoLeche; i++)
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
    
    public void setGranja(GameObject g)
    {
        granja = g;
    }

    public string getTipe()
    {
        return tipo;
    }
    
    public void setId(string ide)
    {
        id = ide;
        cargarPartida();
        
        StartCoroutine("contadorTiempoTranscurridoPlantacion");
    }

    private void cargarPartida()
    {
        if (PlayerPrefs.HasKey(id+"TiempoLecheTranscurrido"))
        {
            tiempoLecheTranscurrido = int.Parse(PlayerPrefs.GetString(id + "TiempoLecheTranscurrido"));
            lecheAnadir = int.Parse(PlayerPrefs.GetString(id + "LecheAnadir"));
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
    
    IEnumerator contadorTiempoTranscurridoPlantacion()
    {
        while (true)
        {
            contadorTiempoTranscurridoJuegoVaca++;
            PlayerPrefs.SetInt("ContadorTiempoTranscurridoJuegoVaca"+id, contadorTiempoTranscurridoJuegoVaca);
            yield return new WaitForSeconds(1f);
        }
    }

    private void guardarPartida()
    {
        PlayerPrefs.SetString(id+"TiempoLecheTranscurrido", tiempoLecheTranscurrido.ToString());
        PlayerPrefs.SetString(id+"LecheAnadir", lecheAnadir.ToString());
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
