using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class PescaController : MonoBehaviour
{

    public bool empezarPescar = true;

    public Sprite[] imagenesPeces;

    private EstrellaController estrellaController;

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
    
    public int numTeclasCorrectas = 0;
    
    public int numBacalaos = 0;
    public int numSalmon = 0;

    private TextMeshProUGUI textoNumBacalao;
    private TextMeshProUGUI textoNumSalmon;
    
    [System.Serializable]
    public class Cancion
    {
        public string nombreCancion;
        public AudioClip audio;
    }

    private List<GameObject> flechaCreadas = new List<GameObject>();
    public bool cancionTerminada = false;

    public List<Cancion> canciones = new List<Cancion>();

    private GameObject espacio;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        espacio = GameObject.Find("PanelTirarCana");
        estrellaController = GameObject.Find("Estrella").GetComponent<EstrellaController>();
        textoNumBacalao = GameObject.Find("TextoNumBacalao").GetComponent<TextMeshProUGUI>();
        textoNumSalmon = GameObject.Find("TextoNumSalmon").GetComponent<TextMeshProUGUI>();
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

        if (empezarPescar)
        {
            espacio.transform.localScale = new Vector3(1, 1, 1);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!empezarContador)
                {
                    espacio.transform.localScale = new Vector3(0, 1, 1);
                    playerPescadorAnimator.SetBool("lanzar", true);
                    audioSource.clip = canciones[0].audio;
                    audioSource.Play();
                    StartCoroutine("creadorTeclas");
                    cancionTerminada = false;
                    empezarContador = true;
                    empezarPescar = false;
                }
            }
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

        textoNumBacalao.text = numBacalaos.ToString();
        textoNumSalmon.text = numSalmon.ToString();
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

        GameObject flechaInstanciada = null;
        
        for (int i = 0; i < teclas.Count; i++)
        {
            while (true)
            {
                if (tiempoActual >= int.Parse(tiempos[i]) - 40)
                {
                    flechaInstanciada = Instantiate(flecha);

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

        while (true)
        {
            if (audioSource.isPlaying && empezarContador)
            {
                cancionTerminada = false;
            }
            else
            {
                if (flechaInstanciada == null)
                {
                    float portenzageAcierto = (numTeclasCorrectas * 100) / teclas.Count;

                    if (portenzageAcierto >= 45)
                    {
                        sumarPez();
                    }
                    else
                    {
                        empezarPescar = true;
                    }
                    
                    numTeclasCorrectas = 0;
                    cancionTerminada = true;
                    break;
                }
            }
            yield return null;
        }
        yield return null;
    }

    public void sumarPez()
    {
        Random random = new Random();
        int numRandom = random.Next(0, imagenesPeces.Length);

        if (numRandom >= 1)
        {
            numBacalaos++;
        }
        else
        {
            numSalmon++;
        }
        
        PlayerPrefs.SetInt("NumBacalao", numBacalaos);
        PlayerPrefs.SetInt("NumSalmon", numSalmon);
        PlayerPrefs.Save();
        
        estrellaController.startAnimation(imagenesPeces[numRandom]);
    }

    public void sumarTeclaCorrecta()
    {
        numTeclasCorrectas++;
    }
}
