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
    
    public List<sprite> spritesTerreno = new List<sprite>();

    public string id = "";

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

    private bool creciendo = false;

    void Start()
    {
        toolBarController = GameObject.Find("ToolBar").GetComponent<ToolBarController>();
        inventarioController = GameObject.Find("ToolBar").GetComponent<InventarioController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
                    }
                }
                else
                {
                    regar = false;
                    creciendo = true;
                    StartCoroutine("crecerPlanta");
                }
            }
        }
        else
        {
            inventarioController.anadirInventario(planta,cantidad);
            planta = "";
            cantidad = 0;
            posicionPlanta = -1;
            spriteRenderer.sprite = terrenoSinPlanta;
            recoger = false;
            regar = false;
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
        }
    }

    public void esconderInter()
    {
        GetComponentInChildren<InteractuarUIController>().invisibleDerecho();
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
        spriteRenderer.sprite = spritesTerreno[posicionPlanta].sprites[1];
        for (int i = 1; i < spritesTerreno[posicionPlanta].sprites.Count; i++)
        {
            for (int j = 0; j < tiempoPlantacion; j++)
            {
                yield return new WaitForSeconds(1f);
                tiempo++;
            }

            tiempo = 0;
            spriteRenderer.sprite = spritesTerreno[posicionPlanta].sprites[i];
        }
        
        creciendo = false;
        recoger = true;
        
        yield return null;
    }
    
    public void setId(string ide)
    {
        id = ide;
    }
}
