using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventarioController : MonoBehaviour
{

    public GameObject[] posiciones = new GameObject[4];
    public GameObject[] posicionesInventario = new GameObject[4];
    private GameObject tienda;
    
    public TextMeshProUGUI oro;
    private GameObject inventario;
    private RectTransform rectTransformInventario;
    private RectTransform rectTransformRaton;
    private PosicionRatonController posicionRatonController;
    private ToolBarController toolBarController;
    private InventarioCofreController inventarioCofreController;

    public Sprite spritePrueba;

    private GameObject player;

    public string[] tipos;
    public Sprite[] sprs;

    public bool mostrar = true;
    
    public Vector2 posicionOriginal;

    public IDictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

    private void Awake()
    {
        
        //PlayerPrefs.DeleteAll();
        
        //Cursor.visible = false;
        
        for (int i = 0; i < tipos.Length; i++)
        {
            sprites.Add(tipos[i],sprs[i]);
        }
    }

    private void Start()
    {
        oro = GameObject.Find("TextoOro").GetComponent<TextMeshProUGUI>();
        inventario = GameObject.Find("Inventario");
        rectTransformInventario = inventario.GetComponent<RectTransform>();
        rectTransformRaton = GameObject.Find("Raton").GetComponent<RectTransform>();

        posicionRatonController = GameObject.Find("PosRaton").GetComponent<PosicionRatonController>();

        toolBarController = GetComponent<ToolBarController>();
        
        player = GameObject.Find("Player");

        inventarioCofreController = GameObject.Find("InventarioCofre").GetComponent<InventarioCofreController>();

        if (PlayerPrefs.HasKey("inventarioPosicion0"))
        {
            cargarInventarioDinero();
        }
        
        anadirInventario("mesaCrafteo", 5);
        anadirInventario("valla", 5);
        anadirInventario("zanahoria", 50);
        anadirInventario("plantacion", 5);

        anadirDinero(10000);
        
        tienda = GameObject.Find("InterfazTienda");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && mostrar)
        {
            if (rectTransformInventario.localScale.x == 0)
            {

                rectTransformInventario.transform.localPosition = posicionOriginal;
                
                rectTransformInventario.localScale = new Vector3(1, 1, 1);
                rectTransformRaton.localScale = new Vector3(1, 1, 1);

                player.GetComponent<PlayerController>().mov = false;
            }
            else
            {
                if (posicionRatonController.item != "" && posicionRatonController.cantidad > 0)
                {
                    int resto = anadirInventario(posicionRatonController.item, posicionRatonController.cantidad);
                    
                    if (inventarioCofreController.GetComponent<RectTransform>().localScale.x == 1)
                    {
                        inventarioCofreController.anadirInventario(posicionRatonController.item,posicionRatonController.GetComponent<Image>().sprite,resto,null);
                    }
                    
                    posicionRatonController.item = "";
                    posicionRatonController.cantidad = 0;
                    posicionRatonController.GetComponent<Image>().sprite = null;
                }
                
                rectTransformInventario.localScale = new Vector3(0, 1, 1);
                rectTransformRaton.localScale = new Vector3(0, 1, 1);
                
                player.GetComponent<PlayerController>().mov = true;
                
                guardarInventario();
            }
        }
    }

    public int anadirInventario(string tipo, int cantidad)
    {
        for (int i = 0; i < posiciones.Length; i++)
        {
            if (posiciones[i].GetComponent<PosicionController>().item == tipo)
            {
                if ((posiciones[i].GetComponent<PosicionController>().cantidad+cantidad) < 128)
                {
                    posiciones[i].GetComponent<PosicionController>().cantidad = posiciones[i].GetComponent<PosicionController>().cantidad + cantidad;
                    posiciones[i].GetComponentInChildren<TextMeshProUGUI>().text =
                        posiciones[i].GetComponent<PosicionController>().cantidad.ToString();
                    cantidad = 0;
                    guardarInventario();
                    break;
                }
                else
                {
                    int cantAnterior = posiciones[i].GetComponent<PosicionController>().cantidad;
                    posiciones[i].GetComponent<PosicionController>().cantidad = 128;
                    posiciones[i].GetComponentInChildren<TextMeshProUGUI>().text =
                        posiciones[i].GetComponent<PosicionController>().cantidad.ToString();
                    cantidad = (cantAnterior + cantidad) - 128;
                    guardarInventario();
                }
            }
        }

        if (cantidad > 0)
        {
            for (int i = 0; i < posiciones.Length; i++)
            {
                if (posiciones[i].GetComponent<PosicionController>().item == "")
                {
                    posiciones[i].GetComponent<PosicionController>().item = tipo;
                    if (cantidad < 128)
                    {
                        posiciones[i].GetComponent<PosicionController>().cantidad = cantidad;
                        posiciones[i].GetComponentInChildren<TextMeshProUGUI>().text =
                            posiciones[i].GetComponent<PosicionController>().cantidad.ToString();
                        cantidad = 0;
                        guardarInventario();
                        break;
                    }
                    else
                    {
                        posiciones[i].GetComponent<PosicionController>().cantidad = 128;
                        posiciones[i].GetComponentInChildren<TextMeshProUGUI>().text =
                            posiciones[i].GetComponent<PosicionController>().cantidad.ToString();
                        cantidad = cantidad - 128;
                        guardarInventario();
                    }
                }
            }
        }

        return cantidad;
    }
    
    public int anadirSoloInventario(string tipo, Sprite sprite, int cantidad, GameObject other)
    {
        for (int i = 0; i < posicionesInventario.Length; i++)
        {
            if (!posicionesInventario[i].Equals(other))
            {
                if (posicionesInventario[i].GetComponent<PosicionController>().item == tipo)
                {
                    if ((posicionesInventario[i].GetComponent<PosicionController>().cantidad+cantidad) < 128)
                    {
                        posicionesInventario[i].GetComponent<PosicionController>().cantidad = posicionesInventario[i].GetComponent<PosicionController>().cantidad + cantidad;
                        posicionesInventario[i].GetComponentInChildren<TextMeshProUGUI>().text =
                            posicionesInventario[i].GetComponent<PosicionController>().cantidad.ToString();
                        cantidad = 0;
                        guardarInventario();
                        break;
                    }
                    else
                    {
                        int cantAnterior = posicionesInventario[i].GetComponent<PosicionController>().cantidad;
                        posicionesInventario[i].GetComponent<PosicionController>().cantidad = 128;
                        posicionesInventario[i].GetComponentInChildren<TextMeshProUGUI>().text =
                            posicionesInventario[i].GetComponent<PosicionController>().cantidad.ToString();
                        cantidad = (cantAnterior + cantidad) - 128;
                        guardarInventario();
                    }
                }
            }
        }

        if (cantidad > 0)
        {
            for (int i = 0; i < posicionesInventario.Length; i++)
            {
                if (!posicionesInventario[i].Equals(other))
                {
                    if (posicionesInventario[i].GetComponent<PosicionController>().item == "")
                    {
                        posicionesInventario[i].GetComponent<PosicionController>().item = tipo;
                        if (cantidad < 128)
                        {
                            posicionesInventario[i].GetComponent<PosicionController>().cantidad = cantidad;
                            posicionesInventario[i].GetComponentInChildren<TextMeshProUGUI>().text =
                                posicionesInventario[i].GetComponent<PosicionController>().cantidad.ToString();
                            cantidad = 0;
                            posicionesInventario[i].GetComponent<Image>().sprite = sprite;
                            guardarInventario();
                            break;
                        }
                        else
                        {
                            posicionesInventario[i].GetComponent<PosicionController>().cantidad = 128;
                            posicionesInventario[i].GetComponentInChildren<TextMeshProUGUI>().text =
                                posicionesInventario[i].GetComponent<PosicionController>().cantidad.ToString();
                            cantidad = cantidad - 128;
                            posicionesInventario[i].GetComponent<Image>().sprite = sprite;
                            guardarInventario();
                        }
                    }
                }
            }
        }

        return cantidad;
    }

    public bool comprobarEspacio(string tipo, int cantidad)
    {
        for (int i = 0; i < posiciones.Length; i++)
        {
            if (posiciones[i].GetComponent<PosicionController>().item == tipo)
            {
                if ((posiciones[i].GetComponent<PosicionController>().cantidad+cantidad) < 128)
                {
                    cantidad = 0;
                    break;
                }
                else
                {
                    int cantAnterior = posiciones[i].GetComponent<PosicionController>().cantidad;
                    cantidad = (cantAnterior + cantidad) - 128;
                }
            }
        }

        if (cantidad > 0)
        {
            for (int i = 0; i < posiciones.Length; i++)
            {
                if (posiciones[i].GetComponent<PosicionController>().item == "")
                {
                    posiciones[i].GetComponent<PosicionController>().item = tipo;
                    if (cantidad < 128)
                    {
                        cantidad = 0;
                        break;
                    }
                    else
                    {
                        cantidad = cantidad - 128;
                    }
                }
            }
        }

        bool hayEspacio = true;

        if (cantidad > 0)
        {
            hayEspacio = false;
        }
        
        return hayEspacio;
    }

    public void anadirDinero(int cantidad)
    {
        int cantidadOro = int.Parse(oro.text);

        oro.text = (cantidadOro + cantidad).ToString();
        
        PlayerPrefs.SetString("dineroPlayer", oro.text);
        PlayerPrefs.Save();
    }

    public void guardarInventario()
    {
        for (int i = 0; i < posiciones.Length; i++)
        {
            PosicionController posicionController = posiciones[i].GetComponent<PosicionController>();
            PlayerPrefs.SetString("inventarioPosicion"+i, posicionController.item+","+posicionController.cantidad);
        }
        
        PlayerPrefs.Save();
    }

    public void cargarInventarioDinero()
    {
        for (int i = 0; i < posiciones.Length; i++)
        {
            string[] datos = PlayerPrefs.GetString("inventarioPosicion"+i).Split(',');
            PosicionController posicionController = posiciones[i].GetComponent<PosicionController>();

            posicionController.item = datos[0];
            posicionController.cantidad = int.Parse(datos[1]);

            posicionController.GetComponent<Image>().sprite = sprites[datos[0]];
        }

        oro.text = PlayerPrefs.GetString("dineroPlayer");
    }
}
