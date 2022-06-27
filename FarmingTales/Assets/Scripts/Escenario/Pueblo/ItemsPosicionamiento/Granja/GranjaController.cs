using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GranjaController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    
    private GameObject toolBar;
    private GameObject inventarioGranja;
    private GameObject player;
    private GameObject inventario;
    private GameObject comida;
    
    public PosicionInventarioCofre[] posicionesInventarioGranja;
    
    private RectTransform rectTransformInventario;
    private RectTransform rectTransformRaton;
    
    public int puntoPosicionado;
    
    public Vector2 posicionInventario;

    public string id = "";

    private bool abierto = false;

    public int porcentageComida = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        inventarioGranja = GameObject.Find("InventarioGranja");
    }

    private void Start()
    {
        rectTransformInventario = GameObject.Find("Inventario").GetComponent<RectTransform>();
        rectTransformRaton = GameObject.Find("Raton").GetComponent<RectTransform>();
        player = GameObject.Find("Player");
        inventario = GameObject.Find("Inventario");
        toolBar = GameObject.Find("ToolBar");
        comida = GameObject.Find("Comida");
        
        posicionesInventarioGranja = new PosicionInventarioCofre[inventarioGranja.GetComponent<InventarioGranjaController>().posiciones.Length];

        for (int i = 0; i < posicionesInventarioGranja.Length; i++)
        {
            posicionesInventarioGranja[i] = new PosicionInventarioCofre();
        }
        
        if (id != "")
        {
            if (PlayerPrefs.HasKey(id+0))
            {
                cargarInventario();
            }
        }

        GameObject gallina = Instantiate(Resources.Load("Prefabs/Instancias/Animales/gallina") as GameObject);
        gallina.GetComponent<GallinaController>().granja = gameObject;
        gallina.transform.position = transform.position;


    }
    
    private void Update()
    {
        if (abierto)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                for (int i = 0; i < posicionesInventarioGranja.Length; i++)
                {
                    posicionesInventarioGranja[i].item = inventarioGranja.GetComponent<InventarioGranjaController>().posiciones[i].GetComponent<PosicionController>().item;
                    posicionesInventarioGranja[i].cantidad = inventarioGranja.GetComponent<InventarioGranjaController>().posiciones[i].GetComponent<PosicionController>().cantidad;
                    posicionesInventarioGranja[i].sprite = inventarioGranja.GetComponent<InventarioGranjaController>().posiciones[i].GetComponent<Image>().sprite;
                }
                
                inventarioGranja.GetComponent<InventarioGranjaController>().gameObject.GetComponent<RectTransform>().localScale = new Vector3(0,1,1);
                
                player.GetComponent<PlayerController>().mov = true;
                
                guardarInventario();
                
                abierto = false;
            }
        }
    }

    public void guardarItemGranja(string tipoItem, int cantidadItem)
    {
        for (int i = 0; i < inventarioGranja.GetComponent<InventarioGranjaController>().posiciones.Length; i++)
        {
            inventarioGranja.GetComponent<InventarioGranjaController>().posiciones[i].GetComponent<PosicionController>().item =
                posicionesInventarioGranja[i].item;
            inventarioGranja.GetComponent<InventarioGranjaController>().posiciones[i].GetComponent<PosicionController>().cantidad =
                posicionesInventarioGranja[i].cantidad;
            inventarioGranja.GetComponent<InventarioGranjaController>().posiciones[i].GetComponent<Image>().sprite =
                posicionesInventarioGranja[i].sprite;
        }

        inventarioGranja.GetComponent<InventarioGranjaController>().anadirInventario(tipoItem, cantidadItem);
    
        for (int i = 0; i < posicionesInventarioGranja.Length; i++)
        {
            posicionesInventarioGranja[i].item = inventarioGranja.GetComponent<InventarioGranjaController>().posiciones[i].GetComponent<PosicionController>().item;
            posicionesInventarioGranja[i].cantidad = inventarioGranja.GetComponent<InventarioGranjaController>().posiciones[i].GetComponent<PosicionController>().cantidad;
            posicionesInventarioGranja[i].sprite = inventarioGranja.GetComponent<InventarioGranjaController>().posiciones[i].GetComponent<Image>().sprite;
        }
    }

    public void setId(string ide)
    {
        id = ide;
    }

    public void guardarInventario()
    {
        for (int i = 0; i < posicionesInventarioGranja.Length; i++)
        {
            PlayerPrefs.SetString(id+i,posicionesInventarioGranja[i].item+","+posicionesInventarioGranja[i].cantidad);
        }
        
        PlayerPrefs.Save();
    }

    public void cargarInventario()
    {
        if (PlayerPrefs.HasKey(id+0))
        {
            for (int i = 0; i < posicionesInventarioGranja.Length; i++)
            {
                string[] datos = PlayerPrefs.GetString(id+i).Split(',');
                posicionesInventarioGranja[i].item = datos[0];
                posicionesInventarioGranja[i].cantidad = int.Parse(datos[1]);

                posicionesInventarioGranja[i].sprite = toolBar.GetComponent<InventarioController>().sprites[datos[0]];
            }
        }
    }


    public void inter()
    {
        for (int i = 0; i < inventarioGranja.GetComponent<InventarioGranjaController>().posiciones.Length; i++)
        {
            inventarioGranja.GetComponent<InventarioGranjaController>().posiciones[i].GetComponent<PosicionController>().item =
                posicionesInventarioGranja[i].item;
            inventarioGranja.GetComponent<InventarioGranjaController>().posiciones[i].GetComponent<PosicionController>().cantidad =
                posicionesInventarioGranja[i].cantidad;
            inventarioGranja.GetComponent<InventarioGranjaController>().posiciones[i].GetComponent<Image>().sprite =
                posicionesInventarioGranja[i].sprite;
        }

        comida.GetComponent<ComidaController>().setGranja(gameObject);
        rectTransformInventario.transform.localPosition = posicionInventario;
        rectTransformInventario.localScale = new Vector3(1, 1, 1);
        rectTransformRaton.localScale = new Vector3(1, 1, 1);
        inventarioGranja.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);

        player.GetComponent<PlayerController>().mov = false;

        abierto = true;
    }

    public void mostrarInter()
    {
        GetComponentInChildren<InteractuarUIController>().visibleDerecho();
    }

    public void esconderInter()
    {
        GetComponentInChildren<InteractuarUIController>().invisibleDerecho();
    }
}
