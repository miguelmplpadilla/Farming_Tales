using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GranjaController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    
    private GameObject posicionadorItem;
    private GameObject inventarioGranja;
    private GameObject player;
    private GameObject inventario;
    
    public PosicionInventarioCofre[] posicionesInventarioGranja;
    
    private RectTransform rectTransformInventario;
    private RectTransform rectTransformRaton;
    
    public int puntoPosicionado;
    
    public Vector2 posicionInventario;

    public string id = "";

    private bool abierto = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        posicionadorItem = GameObject.Find("PosicionadorItem");
        inventarioGranja = GameObject.Find("InventarioGranja");
    }

    private void Start()
    {
        rectTransformInventario = GameObject.Find("Inventario").GetComponent<RectTransform>();
        rectTransformRaton = GameObject.Find("Raton").GetComponent<RectTransform>();
        player = GameObject.Find("Player");
        inventario = GameObject.Find("Inventario");
        
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

                posicionesInventarioGranja[i].sprite = inventario.GetComponent<InventarioController>().sprites[datos[0]];
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
