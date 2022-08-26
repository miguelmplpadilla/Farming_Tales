using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantacionController : MonoBehaviour
{
    [System.Serializable]
    public class sprite
    {
        public string id;
        public int cantidad;
        public List<Sprite> sprites;
    }

    private int tiempoAnterior;
    
    public List<sprite> spritesTerreno = new List<sprite>();

    public string id = "";
    private int estado = 1;

    private ToolBarController toolBarController;
    private InventarioController inventarioController;

    public string planta = "";
    public int posicionPlanta = -1;

    private SpriteRenderer spriteRenderer;

    public Sprite terrenoSinPlanta;

    public bool recoger = false;
    public bool regar = false;
    public int cantidad = 0;

    public int tiempo = 0;

    public int tiempoPlantacion = 150;
    public int tiempoActualPlantacion = 0;

    private bool creciendo = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        estado = 1;
    }

    void Start()
    {
        toolBarController = GameObject.Find("ToolBar").GetComponent<ToolBarController>();
        inventarioController = GameObject.Find("ToolBar").GetComponent<InventarioController>();
    }

    public void inter()
    {
        if (!recoger)
        {
            if (!creciendo)
            {
                if (!regar)
                {
                    for (int i = 0; i < spritesTerreno.Count; i++)
                    {
                        if (spritesTerreno[i].id == toolBarController.posicionActual)
                        {
                            planta = spritesTerreno[i].id;
                            cantidad = spritesTerreno[i].cantidad;
                            posicionPlanta = i;
                        }
                    }

                    if (planta != "")
                    {
                        toolBarController.posicionController.cantidad =
                            toolBarController.posicionController.cantidad - 1;
                        regar = true;
                        spriteRenderer.sprite = spritesTerreno[posicionPlanta].sprites[0];
                        guardarPlantacion();
                    }
                }
                else
                {
                    regar = false;
                    creciendo = true;
                    spriteRenderer.sprite = spritesTerreno[posicionPlanta].sprites[1];
                    StartCoroutine("crecerPlanta");
                    guardarPlantacion();
                }
            }
        }
        else
        {
            if (planta == "semillaTrigo")
            {
                inventarioController.anadirInventario("trigo",cantidad);
            }
            inventarioController.anadirInventario(planta,4);
            planta = "";
            cantidad = 0;
            posicionPlanta = -1;
            spriteRenderer.sprite = terrenoSinPlanta;
            recoger = false;
            regar = false;
            guardarPlantacion();
        }
    }
    
    public void mostrarInter()
    {
        if (isInArrayPlanta() != "" && !creciendo)
        {
            GetComponentInChildren<InteractuarUIController>().visibleDerecho();   
        }
        else
        {
            GetComponentInChildren<InteractuarUIController>().invisibleDerecho();
        }

        if (regar)
        {
            GetComponentInChildren<InteractuarUIController>().visibleDerecho();
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        }
        
        if (recoger)
        {
            GetComponentInChildren<InteractuarUIController>().visibleDerecho();  
        }
    }

    public void esconderInter()
    {
        GetComponentInChildren<InteractuarUIController>().invisibleDerecho();
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
    }

    private string isInArrayPlanta()
    {
        string plant = "";
        
        for (int i = 0; i < spritesTerreno.Count; i++)
        {
            if (spritesTerreno[i].id == toolBarController.posicionActual)
            {
                plant = spritesTerreno[i].id;
            }
        }

        return plant;
    }

    IEnumerator crecerPlanta()
    {
        for (int i = estado; i < spritesTerreno[posicionPlanta].sprites.Count; i++)
        {
            for (int j = tiempoActualPlantacion; j < tiempoPlantacion; j++)
            {
                yield return new WaitForSeconds(1f);
                tiempo++;
                tiempoActualPlantacion = j;
                guardarPlantacion();
            }
            tiempo = 0;
            estado = i;
            spriteRenderer.sprite = spritesTerreno[posicionPlanta].sprites[i];
        }

        estado = 1;
        creciendo = false;
        recoger = true;
        
        yield return null;
    }
    
    public void setId(string ide)
    {
        id = ide;
        cargarPlantacion();
    }

    private void guardarPlantacion()
    {
        PlayerPrefs.SetString(id+"planta", planta);
        PlayerPrefs.SetInt(id+"cantidad", cantidad);
        PlayerPrefs.SetInt(id+"estado", estado);
        PlayerPrefs.SetInt(id+"tiempoActual", tiempoActualPlantacion);
        if (creciendo)
        {
            PlayerPrefs.SetInt(id+"creciendo",1);
        } else
        {
            PlayerPrefs.SetInt(id+"creciendo",0);
        }
        
        DateTime dt = DateTime.Now;
        int ms = dt.Millisecond;
        
        PlayerPrefs.SetString("TiempoAnterior", ms.ToString());
        
        PlayerPrefs.Save();
    }

    private void cargarPlantacion()
    {
        planta = PlayerPrefs.GetString(id+"planta");

        if (planta != "")
        {
            cantidad = PlayerPrefs.GetInt(id+"cantidad");
            estado = PlayerPrefs.GetInt(id+"estado");
            tiempoActualPlantacion = PlayerPrefs.GetInt(id+"tiempoActual");

            if (PlayerPrefs.GetInt(id+"creciendo") == 1)
            {
                creciendo = true;
            } else
            {
                creciendo = false;
            }
            
            for (int i = 0; i < spritesTerreno.Count; i++)
            {
                if (spritesTerreno[i].id == planta)
                {
                    cantidad = spritesTerreno[i].cantidad;
                    posicionPlanta = i;
                }
            }

            if (creciendo)
            {
                spriteRenderer.sprite = spritesTerreno[posicionPlanta].sprites[estado];
                StartCoroutine("crecerPlanta");
            }
            else
            {
                regar = true;
                spriteRenderer.sprite = spritesTerreno[posicionPlanta].sprites[0];
            }
        }
    }
    
}
