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

    public IDictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

    private void Awake()
    {
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
        
        anadirInventario("plantacion", 5);
        anadirInventario("cofre", 5);
        anadirInventario("semillaTrigo", 5);
        anadirInventario("patata", 5);
        anadirInventario("zanahoria", 5);
        //anadirInventario("madera", spritePrueba, 1, null);
        
        //PlayerPrefs.DeleteAll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (rectTransformInventario.localScale.x == 0)
            {
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

            //Debug.Log("Tipo: "+datos[0]);

            posicionController.GetComponent<Image>().sprite = sprites[datos[0]];
        }

        oro.text = PlayerPrefs.GetString("dineroPlayer");
    }
}
