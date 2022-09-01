using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlantacionController : MonoBehaviour
{
    [System.Serializable]
    public class sprite
    {
        public string id;
        public int cantidad;
        public List<Sprite> sprites;
    }

    private ParticulasController particulasController;
    
    private TextoEmergenteController textoEmergenteController;

    private int tiempoAnterior;
    
    public List<sprite> spritesTerreno = new List<sprite>();

    public string id = "";
    private int estado = 1;

    private ToolBarController toolBarController;
    private InventarioController inventarioController;
    private AudioController audioController;

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

    private GeneradorPosicionamientoController generadorPosicionamientoController;
    private PosicionadorItemController posicionadorItemController;
    private Controller controller;

    public int contadorTiempoTranscurridoJuegoPlantacion = 0;

    private void Awake()
    {
        audioController = GetComponent<AudioController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        estado = 1;
    }

    void Start()
    {
        controller = GameObject.Find("Controller").GetComponent<Controller>();
        generadorPosicionamientoController =
            GameObject.Find("GeneradorPosiciones").GetComponent<GeneradorPosicionamientoController>();
        particulasController = transform.GetChild(3).GetComponent<ParticulasController>();
        toolBarController = GameObject.Find("ToolBar").GetComponent<ToolBarController>();
        inventarioController = GameObject.Find("ToolBar").GetComponent<InventarioController>();
        textoEmergenteController = GameObject.Find("PanelTextoEmergente").GetComponent<TextoEmergenteController>();
        posicionadorItemController = GameObject.Find("PosicionadorItem").GetComponent<PosicionadorItemController>();
    }

    public void quitar()
    {
        for (int i = 0; i < generadorPosicionamientoController.puntos.Length; i++)
        {
            string idPosicionGenerado = generadorPosicionamientoController.puntos[i].GetComponent<PuntoGeneradoController>().tipo + (i+1) + SceneManager.GetActiveScene().name;

            if (idPosicionGenerado.Equals(id))
            {
                generadorPosicionamientoController.puntos[i].GetComponent<PuntoGeneradoController>().tipo = "";
                generadorPosicionamientoController.puntos[i].GetComponent<PuntoGeneradoController>().ocupado = false;
                break;
            }
        }
        
        posicionadorItemController.guardarPosicionesItem();

        inventarioController.anadirInventario("plantacion", 1);
        
        PlayerPrefs.DeleteKey(id+"planta");
        PlayerPrefs.Save();
        
        Destroy(gameObject);
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
                        particulasController.startParticulas(1);
                        audioController.playAudio(0);
                        guardarPlantacion();
                    }
                }
                else
                {
                    regar = false;
                    creciendo = true;
                    spriteRenderer.sprite = spritesTerreno[posicionPlanta].sprites[1];
                    StartCoroutine("crecerPlanta");
                    particulasController.startParticulas(0);
                    audioController.playAudio(1);
                    guardarPlantacion();
                }
            }
        }
        else
        {

            int resto1 = 0;
            
            if (planta == "semillaTrigo")
            {
                resto1 = inventarioController.anadirInventario("trigo",cantidad);
                
                if (resto1 == cantidad)
                {
                    textoEmergenteController.mostrarTexto("No tienes espacio suficiente para trigo");
                }
            }
            int resto2 = inventarioController.anadirInventario(planta,4);

            if (resto1 != 0)
            {
                if (resto2 == 4 || resto1 == cantidad)
                {
                    textoEmergenteController.mostrarTexto("No tienes espacio suficiente para "+planta);
                }
                else
                {
                    planta = "";
                    cantidad = 0;
                    posicionPlanta = -1;
                    spriteRenderer.sprite = terrenoSinPlanta;
                    recoger = false;
                    regar = false;
                    particulasController.startParticulas(1);
                    audioController.playAudio(0);
                    guardarPlantacion();
                }
            }
            else
            {
                if (resto2 == 4)
                {
                    textoEmergenteController.mostrarTexto("No tienes espacio suficiente para "+planta);
                }
                else
                {
                    planta = "";
                    cantidad = 0;
                    posicionPlanta = -1;
                    spriteRenderer.sprite = terrenoSinPlanta;
                    recoger = false;
                    regar = false;
                    particulasController.startParticulas(1);
                    audioController.playAudio(0);
                    guardarPlantacion();
                }
            }
        }
    }
    
    public void mostrarInter()
    {
        if (isInArrayPlanta() != "" && !creciendo)
        {
            transform.GetChild(0).GetComponent<InteractuarUIController>().visible();
        }
        else
        {
            transform.GetChild(0).GetComponent<InteractuarUIController>().esconder();
        }

        if (regar)
        {
            transform.GetChild(0).GetComponent<InteractuarUIController>().visible();
            transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            transform.GetChild(0).GetComponent<InteractuarUIController>().esconder();
            transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
            
            if (isInArrayPlanta() != "" && !creciendo)
            {
                transform.GetChild(0).GetComponent<InteractuarUIController>().visible();
            }
        }
        
        if (recoger)
        {
            transform.GetChild(0).GetComponent<InteractuarUIController>().visible();
        }
    }

    public void mostrarInterQuitar()
    {
        transform.GetChild(1).GetComponent<InteractuarUIController>().visible();
        transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    }

    public void esconderInter()
    {
        transform.GetChild(0).GetComponent<InteractuarUIController>().esconder();
        transform.GetChild(1).GetComponent<InteractuarUIController>().esconder();
        transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
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

        StartCoroutine("contadorTiempoTranscurridoPlantacion");
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
                
                if (PlayerPrefs.HasKey("ContadorTiempoTranscurridoJuegoPlantacion"+id))
                {
                    contadorTiempoTranscurridoJuegoPlantacion = PlayerPrefs.GetInt("ContadorTiempoTranscurridoJuegoPlantacion" + id);
                    int tiempoJuego = PlayerPrefs.GetInt("ContadorTiempoTranscurridoJuego");

                    int resto = tiempoJuego - contadorTiempoTranscurridoJuegoPlantacion;

                    int cantSumar = 0;

                    if (resto >= tiempoPlantacion)
                    {
                        cantSumar = resto / tiempoPlantacion;

                        if (cantSumar < spritesTerreno[posicionPlanta].sprites.Count)
                        {
                            estado = cantSumar;
                        }
                        else
                        {
                            estado = spritesTerreno[posicionPlanta].sprites.Count;
                        }
                    }
                }
                
                contadorTiempoTranscurridoJuegoPlantacion = PlayerPrefs.GetInt("ContadorTiempoTranscurridoJuego");
                
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

    IEnumerator contadorTiempoTranscurridoPlantacion()
    {
        while (true)
        {
            contadorTiempoTranscurridoJuegoPlantacion++;
            PlayerPrefs.SetInt("ContadorTiempoTranscurridoJuegoPlantacion"+id, contadorTiempoTranscurridoJuegoPlantacion);
            yield return new WaitForSeconds(1f);
        }
    }
    
}
