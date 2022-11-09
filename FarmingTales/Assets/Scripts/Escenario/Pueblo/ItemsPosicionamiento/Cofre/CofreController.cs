using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CofreController : MonoBehaviour
{

    public string id = "";
    public bool cofreExterior = false;

    public bool romper = true;

    [System.Serializable]
    public class ObjetosAnadirCofre
    {
        public string item = "";
        public int cantidad = 0;
        public Sprite sprite;

        public ObjetosAnadirCofre(string id, int cant, Sprite spr)
        {
            item = id;
            cantidad = cant;
            sprite = spr;
        }
    }

    public List<ObjetosAnadirCofre> objetosAnadir = new List<ObjetosAnadirCofre>();

    public PosicionInventarioCofre[] posicionInventarioCofres;
    public InventarioCofreController inventarioCofreController;
    
    private bool abierto = false;
    
    private RectTransform rectTransformInventario;
    private RectTransform rectTransformRaton;

    private GameObject player;

    public InventarioController inventarioController = null;

    private Animator animator;

    public Vector2 posicionInventario;
    
    private GeneradorPosicionamientoController generadorPosicionamientoController;
    private PosicionadorItemController posicionadorItemController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        try
        {
            generadorPosicionamientoController =
                GameObject.Find("GeneradorPosiciones").GetComponent<GeneradorPosicionamientoController>();
        }
        catch (Exception e)
        {
        }
        
        try
        {
            posicionadorItemController = GameObject.Find("PosicionadorItem").GetComponent<PosicionadorItemController>();
        }
        catch (Exception e)
        {
        }

        if (!cofreExterior)
        {
            inventarioCofreController = GameObject.Find("InventarioCofre").GetComponent<InventarioCofreController>();
        }
        else
        {
            inventarioCofreController = GameObject.Find("InvetarioCofreExterior").GetComponent<InventarioCofreController>();
        }

        if (posicionInventarioCofres == null)
        {
            posicionInventarioCofres = new PosicionInventarioCofre[inventarioCofreController.posiciones.Length];

            for (int i = 0; i < posicionInventarioCofres.Length; i++)
            {
                posicionInventarioCofres[i] = new PosicionInventarioCofre();
            }
        }
        
        rectTransformInventario = GameObject.Find("Inventario").GetComponent<RectTransform>();
        rectTransformRaton = GameObject.Find("Raton").GetComponent<RectTransform>();
        
        player = GameObject.Find("Player");
        
        inventarioController = GameObject.Find("ToolBar").GetComponent<InventarioController>();

        if (cofreExterior)
        {
            id = "cofreExterior" + SceneManager.GetActiveScene().name;
        }
        
        if (objetosAnadir.Count > 0)
        {
            posicionInventarioCofres = new PosicionInventarioCofre[objetosAnadir.Count];

            for (int i = 0; i < objetosAnadir.Count; i++)
            {
                posicionInventarioCofres[i] = new PosicionInventarioCofre(objetosAnadir[i].item, objetosAnadir[i].cantidad, objetosAnadir[i].sprite);
            }
            
        }
        
        if (id != "")
        {
            if (PlayerPrefs.HasKey(id+0))
            {
                cargarInventario();
            }
        }
    }

    public void anadirObjetoInicioCofre(string id, int cant, Sprite spr)
    {
        objetosAnadir.Add(new ObjetosAnadirCofre(id, cant, spr));
    }

    public void anadirObjetosCofre()
    {
        if (objetosAnadir.Count > 0)
        {
            posicionInventarioCofres = new PosicionInventarioCofre[objetosAnadir.Count];

            for (int i = 0; i < objetosAnadir.Count; i++)
            {
                posicionInventarioCofres[i] = new PosicionInventarioCofre(objetosAnadir[i].item, objetosAnadir[i].cantidad, objetosAnadir[i].sprite);
            }
            
        }
    }

    private void Update()
    {
        if (abierto)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                guardarInventarioArray();
                
                inventarioCofreController.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0,1,1);
                
                player.GetComponent<PlayerController>().mov = true;
                
                guardarInventario();
                
                animator.SetTrigger("cerrar");
                
                abierto = false;
            }
        }
    }

    public void guardarInventarioArray()
    {
        for (int i = 0; i < posicionInventarioCofres.Length; i++)
        {
            posicionInventarioCofres[i].item = inventarioCofreController.posiciones[i].GetComponent<PosicionController>().item;
            posicionInventarioCofres[i].cantidad = inventarioCofreController.posiciones[i].GetComponent<PosicionController>().cantidad;
            posicionInventarioCofres[i].sprite = inventarioCofreController.posiciones[i].GetComponent<Image>().sprite;
        }
    }
    
    public void quitar()
    {
        if (!cofreExterior && romper)
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

            inventarioController.anadirInventario("cofre", 1);
            
            PlayerPrefs.DeleteKey(id+0);
            PlayerPrefs.Save();
        
            Destroy(gameObject);
        }
    }

    public void inter()
    {
        for (int i = 0; i < posicionInventarioCofres.Length; i++)
        {
            inventarioCofreController.posiciones[i].GetComponent<PosicionController>().item =
                posicionInventarioCofres[i].item;
            inventarioCofreController.posiciones[i].GetComponent<PosicionController>().cantidad =
                posicionInventarioCofres[i].cantidad;
            inventarioCofreController.posiciones[i].GetComponent<Image>().sprite =
                posicionInventarioCofres[i].sprite;
        }

        rectTransformInventario.transform.localPosition = posicionInventario;
        rectTransformInventario.localScale = new Vector3(1, 1, 1);
        rectTransformRaton.localScale = new Vector3(1, 1, 1);
        inventarioCofreController.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);

        player.GetComponent<PlayerController>().mov = false;

        inventarioCofreController.cofre = gameObject;
        
        animator.SetTrigger("abrir");
        
        abierto = true;
    }

    public void mostrarInter()
    {
        GetComponentInChildren<InteractuarUIController>().visibleDerecho();
    }

    public void esconderInter()
    {
        GetComponentInChildren<InteractuarUIController>().invisibleDerecho();
        if (!cofreExterior)
        {
            transform.GetChild(1).GetComponent<InteractuarUIController>().esconder();
            transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    
    public void mostrarInterQuitar()
    {
        if (!cofreExterior && romper)
        {
            transform.GetChild(1).GetComponent<InteractuarUIController>().visible();
            transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void setId(string ide)
    {
        id = ide;
    }

    public void guardarInventario()
    {
        for (int i = 0; i < posicionInventarioCofres.Length; i++)
        {
            PlayerPrefs.SetString(id+i,posicionInventarioCofres[i].item+","+posicionInventarioCofres[i].cantidad);
        }
        
        PlayerPrefs.Save();
    }

    public void cargarInventario()
    {
        if (PlayerPrefs.HasKey(id+0))
        {
            for (int i = 0; i < posicionInventarioCofres.Length; i++)
            {
                string[] datos = PlayerPrefs.GetString(id+i).Split(',');
                posicionInventarioCofres[i].item = datos[0];
                posicionInventarioCofres[i].cantidad = int.Parse(datos[1]);

                posicionInventarioCofres[i].sprite = inventarioController.infoObjetos[datos[0]].sprite;
            }
        }
    }
}
