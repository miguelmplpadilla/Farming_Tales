using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CofreController : MonoBehaviour
{

    public string id = "";
    
    public PosicionInventarioCofre[] posicionInventarioCofres;
    private InventarioCofreController inventarioCofreController;
    
    private bool abierto = false;
    
    private RectTransform rectTransformInventario;
    private RectTransform rectTransformRaton;

    private GameObject player;

    private InventarioController inventarioController;

    private Animator animator;

    public Vector2 posicionInventario;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        inventarioCofreController = GameObject.Find("InventarioCofre").GetComponent<InventarioCofreController>();

        posicionInventarioCofres = new PosicionInventarioCofre[inventarioCofreController.posiciones.Length];

        for (int i = 0; i < posicionInventarioCofres.Length; i++)
        {
            posicionInventarioCofres[i] = new PosicionInventarioCofre();
        }
        
        rectTransformInventario = GameObject.Find("Inventario").GetComponent<RectTransform>();
        rectTransformRaton = GameObject.Find("Raton").GetComponent<RectTransform>();
        
        player = GameObject.Find("Player");
        
        inventarioController = GameObject.Find("ToolBar").GetComponent<InventarioController>();

        Debug.Log("ide: "+id);
        
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
                for (int i = 0; i < posicionInventarioCofres.Length; i++)
                {
                    posicionInventarioCofres[i].item = inventarioCofreController.posiciones[i].GetComponent<PosicionController>().item;
                    posicionInventarioCofres[i].cantidad = inventarioCofreController.posiciones[i].GetComponent<PosicionController>().cantidad;
                    posicionInventarioCofres[i].sprite = inventarioCofreController.posiciones[i].GetComponent<Image>().sprite;
                }
                
                inventarioCofreController.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0,1,1);
                
                player.GetComponent<PlayerController>().mov = true;
                
                guardarInventario();
                
                animator.SetTrigger("cerrar");
                
                abierto = false;
            }
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
        for (int i = 0; i < posicionInventarioCofres.Length; i++)
        {
            string[] datos = PlayerPrefs.GetString(id+i).Split(',');
            posicionInventarioCofres[i].item = datos[0];
            posicionInventarioCofres[i].cantidad = int.Parse(datos[1]);

            posicionInventarioCofres[i].sprite = inventarioController.sprites[datos[0]];
        }
    }
}
