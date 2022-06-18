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
                    toolBarController.posicionController.cantidad = toolBarController.posicionController.cantidad - 1;
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
            creciendo = false;
            recoger = false;
        }
    }

    IEnumerator crecerPlanta()
    {
        spriteRenderer.sprite = spritesTerreno[posicionPlanta].sprites[0];
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

        recoger = true;
        
        yield return null;
    }
    
    public void setId(string ide)
    {
        id = ide;
    }
}
