using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PescaController : MonoBehaviour
{

    public TextAsset partituras;

    private AudioSource audioSource;
    public int tiempoActual = 0;

    private string todaPartitura = "";

    public Sprite[] spritesFlechas;
    public GameObject flecha;

    public GameObject[] puntosCreacionFlechas;
    
    public bool empezarContador = false;
    private bool teclasLanzadas = false;

    private GameObject playerPescador;
    private Animator playerPescadorAnimator;
    
    [System.Serializable]
    public class Cancion
    {
        public string nombreCancion;
        public AudioClip audio;
        public int puntuacion;
    }

    private List<GameObject> flechaCreadas = new List<GameObject>();
    public bool cancionTerminada = false;

    public List<Cancion> canciones = new List<Cancion>();

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        playerPescador = GameObject.Find("PlayerPescador");
        playerPescadorAnimator = playerPescador.GetComponent<Animator>();
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.W))
        {
            string path = "Assets/Resources/partitura.txt";
            
            StreamWriter writer = new StreamWriter(path, true);
            Debug.Log(todaPartitura);
            writer.WriteLine(todaPartitura);
            writer.Close();
        }*/
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!empezarContador)
            {
                playerPescadorAnimator.SetBool("lanzar", true);
                audioSource.clip = canciones[0].audio;
                audioSource.Play();
                StartCoroutine("creadorTeclas");
                empezarContador = true;
            }
        }

        if (audioSource.isPlaying && empezarContador)
        {
            cancionTerminada = false;
        }
        else
        {
            cancionTerminada = true;
        }

        /*if (teclasLanzadas)
        {
            for (int i = 0; i < flechaCreadas.Count; i++)
            {
                try
                {
                    if (flechaCreadas[i] != null)
                    {
                        cancionTerminada = false;
                        break;
                    }
                } finally{}
            }
        }*/

        /*if (Input.anyKeyDown)
        {
            detectPressedKeyOrButton();
        }*/
    }

    private void FixedUpdate()
    {
        if (empezarContador)
        {
            tiempoActual += 1;
        }
        
        if (cancionTerminada)
        {
            playerPescadorAnimator.SetBool("lanzar", false);
            empezarContador = false;
            tiempoActual = 0;
        }
    }

    public void detectPressedKeyOrButton()
    {
        foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                Debug.Log("Tecla: " + kcode+" Tiempo: "+tiempoActual);
                string tecla = "";
                if (kcode.ToString().Equals("LeftArrow"))
                {
                    tecla = "0";
                } else if (kcode.ToString().Equals("DownArrow"))
                {
                    tecla = "1";
                } else if (kcode.ToString().Equals("UpArrow"))
                {
                    tecla = "2";
                } else if (kcode.ToString().Equals("RightArrow"))
                {
                    tecla = "3";
                }
                todaPartitura = todaPartitura + tecla + "," + tiempoActual+"\n";
            }
        }
    }

    IEnumerator creadorTeclas()
    {

        DialogeController dialogeController = new DialogeController();

        List<string> teclas =
            dialogeController.getTextoDialogos(partituras, canciones[0].nombreCancion, "Cancion", "Español");

        List<string> tiempos =
            dialogeController.getTextoDialogos(partituras, canciones[0].nombreCancion, "Cancion", "English");

        for (int i = 0; i < teclas.Count; i++)
        {
            while (true)
            {
                if (tiempoActual == int.Parse(tiempos[i]) - 40)
                {
                    GameObject flechaInstanciada = Instantiate(flecha);

                    flechaCreadas.Add(flechaInstanciada);
                    
                    if (teclas[i].Equals("0"))
                    {
                        flechaInstanciada.GetComponent<FlechaController>().key = KeyCode.LeftArrow;
                    } else if (teclas[i].Equals("1"))
                    {
                        flechaInstanciada.GetComponent<FlechaController>().key = KeyCode.DownArrow;
                    } else if (teclas[i].Equals("2"))
                    {
                        flechaInstanciada.GetComponent<FlechaController>().key = KeyCode.UpArrow;
                    } else if (teclas[i].Equals("3"))
                    {
                        flechaInstanciada.GetComponent<FlechaController>().key = KeyCode.RightArrow;
                    }
                    
                    flechaInstanciada.GetComponent<SpriteRenderer>().sprite = spritesFlechas[int.Parse(teclas[i])];
                    flechaInstanciada.transform.position =
                        puntosCreacionFlechas[int.Parse(teclas[i])].transform.position;
                    break;
                }

                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
}
