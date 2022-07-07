using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private GameObject incubadora;
    public List<GameObject> animales;
    
    public PosicionInventarioCofre[] posicionesInventarioGranja;
    
    private RectTransform rectTransformInventario;
    private RectTransform rectTransformRaton;
    private List<TextMeshProUGUI> numAnimales = new List<TextMeshProUGUI>();
    public int[] cantidadAnimales = new int[2];
    
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
        inventario = GameObject.Find("Inventario");
        rectTransformInventario = inventario.GetComponent<RectTransform>();
        rectTransformRaton = GameObject.Find("Raton").GetComponent<RectTransform>();
        player = GameObject.Find("Player");
        toolBar = GameObject.Find("ToolBar");
        comida = GameObject.Find("Comida");
        incubadora = GameObject.Find("Incubadora");
        
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
        Vector2 posicionamientoAnimal = new Vector2(transform.position.x + 1f, transform.position.y);

        anadirAnimal("gallina");
        anadirAnimal("vaca");

        GameObject numerosAnimales = GameObject.Find("NumerosAnimales");
        
        for (int i = 0; i < numerosAnimales.transform.childCount; i++)
        {
            numAnimales.Add(numerosAnimales.transform.GetChild(i).GetComponent<TextMeshProUGUI>());
        }

        for (int i = 0; i < animales.Count; i++)
        {
            if (animales[i].name == "gallina(Clone)")
            {
                cantidadAnimales[0]++;
            } else if (animales[i].name == "vaca(Clone)")
            {
                cantidadAnimales[1]++;
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

    private void LateUpdate()
    {
        cantidadAnimales[0] = 0;
        cantidadAnimales[1] = 0;
        
        for (int i = 0; i < animales.Count; i++)
        {
            if (animales[i].name == "gallina(Clone)")
            {
                cantidadAnimales[0]++;
            } else if (animales[i].name == "vaca(Clone)")
            {
                cantidadAnimales[1]++;
            }
        }

        numAnimales[0].text = cantidadAnimales[0].ToString();
        numAnimales[1].text = cantidadAnimales[1].ToString();
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
        cargarPartida();
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

        cantidadAnimales[0] = 0;
        cantidadAnimales[1] = 0;
        
        for (int i = 0; i < animales.Count; i++)
        {
            if (animales[i].name == "gallina(Clone)")
            {
                cantidadAnimales[0]++;
            } else if (animales[i].name == "vaca(Clone)")
            {
                cantidadAnimales[1]++;
            }
        }

        numAnimales[0].text = cantidadAnimales[0].ToString();
        numAnimales[1].text = cantidadAnimales[1].ToString();

        incubadora.GetComponent<IncubadoraController>().granjaIncubadora = GetComponent<GranjaIncubadoraController>();
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

    public void anadirAnimal(string tipoAnimal)
    {
        Vector2 posicionamientoAnimal = new Vector2(transform.position.x + 1f, transform.position.y);
        
        GameObject animal = Instantiate(Resources.Load("Prefabs/Instancias/Animales/"+tipoAnimal) as GameObject);
        animal.SendMessage("setId", montarIdAnimal(tipoAnimal));
        animal.transform.SendMessage("setGranja", gameObject);
        animal.transform.position = posicionamientoAnimal;
        animales.Add(animal);

        guardarPartida();
    }

    public string montarIdAnimal(string tipoAnimal)
    {
        return id+tipoAnimal+animales.Count;
    }

    public void guardarPartida()
    {
        PlayerPrefs.SetInt(id+"NumeroAnimales", animales.Count);
        for (int i = 0; i < animales.Count; i++)
        {
            if (animales[i].name == "gallina(Clone)")
            {
                PlayerPrefs.SetString(id+"Animal"+i, "gallina");
            } else if (animales[i].name == "vaca(Clone)")
            {
                PlayerPrefs.SetString(id+"Animal"+i, "vaca");
            }
        }
        
        PlayerPrefs.SetInt(id+"CantidadComida", porcentageComida);
        PlayerPrefs.Save();
    }

    public void cargarPartida()
    {
        Vector2 posicionamientoAnimal = new Vector2(transform.position.x + 1f, transform.position.y);
        
        for (int i = 0; i < PlayerPrefs.GetInt(id+"NumeroAnimales"); i++)
        {
            GameObject animal = Instantiate(Resources.Load("Prefabs/Instancias/Animales/"+PlayerPrefs.GetString(id+"Animal"+i)) as GameObject);
            animal.SendMessage("setGranja", gameObject);
            animal.SendMessage("setId", montarIdAnimal(PlayerPrefs.GetString(id+"Animal"+i)));
            animal.transform.position = posicionamientoAnimal;
            animales.Add(animal);
        }

        porcentageComida = PlayerPrefs.GetInt(id + "CantidadComida");
    }
}
