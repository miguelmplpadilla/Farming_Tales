using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class NPCController : MonoBehaviour {

    public TextAsset dialogos;

    private int numFrase;
    private List<int> numFraseAnteriores = new List<int>();

    public string hablante;
    private String currentFrase = "";

    // Componentes
    private Animator animator;
    private GameObject player;

    // UI
    public Sprite image;
    private GameObject panel;
    private GameObject objectTexto;
    private TextMeshProUGUI texto;
    private GameObject imagePanel;

    private List<String> frases = new List<string>();
    private DialogeController dialogeController = new DialogeController();

    public bool hablando = false;
    public bool hablar = true;
    public string idioma = "Español";
    public string[] frasesDisponibles;

    void Start()
    {
        idioma = "Español";
        panel = GameObject.Find("CuadroDialogo");
        objectTexto = GameObject.Find("TextoDialogo");
        imagePanel = GameObject.Find("ImagenNPC");
        texto = objectTexto.GetComponent<TextMeshProUGUI>();
        player = GameObject.Find("Player");
    }

    public void empezarHablar() {
        if (hablando == false) {
            hablando = true;
            panel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            imagePanel.GetComponent<Image>().sprite = image;
            StartCoroutine("mostrarFrase");
            
            float escalaInteractuarX = transform.GetChild(0).transform.localScale.x;

            if (escalaInteractuarX < 0)
            {
                escalaInteractuarX = -escalaInteractuarX;
            }
            
            if (player.transform.position.x > gameObject.transform.position.x) {
                gameObject.transform.localScale = new Vector3(2.9623f, 2.9623f, 1f);
                transform.GetChild(0).transform.localScale = new Vector3(-escalaInteractuarX,transform.GetChild(0).transform.localScale.y, 1);
            }
            else {
                gameObject.transform.localScale = new Vector3(-2.9623f, 2.9623f, 1f);
                transform.GetChild(0).transform.localScale = new Vector3(escalaInteractuarX,transform.GetChild(0).transform.localScale.y, 1);
            }
        }
    }
    
    IEnumerator mostrarFrase() {
        bool seguir = true;
        for (int i = 0; i < frases.Count; i++) {
            if (seguir == true) {
                if (frases[i].Equals("execute1"))
                {
                    GameObject.Find("HistoriaController").GetComponent<HistoriaController>()
                        .StartCoroutine("transicionHistoriaCastillo");
                    seguir = true;
                } else if (frases[i].Equals("execute2"))
                {
                    GameObject.Find("HistoriaController").GetComponent<HistoriaController>()
                        .StartCoroutine("transicionArribaCastillo", 1);
                    seguir = true;
                } else if (frases[i].Equals("execute3"))
                {
                    GameObject.Find("HistoriaController").GetComponent<HistoriaController>().moverCamaraPlayer();
                    seguir = true;
                } else if (frases[i].Equals("execute4"))
                {
                    GameObject.Find("HistoriaController").GetComponent<HistoriaController>()
                        .StartCoroutine("transicionMazmorraCastillo");
                    seguir = true;
                } else if (frases[i].Equals("execute5"))
                {
                    GameObject.Find("HistoriaController").GetComponent<HistoriaController>()
                        .StartCoroutine("transicionArribaCastillo", 3);
                    seguir = true;
                }
                else {
                    for (int j = 0; j < frases[i].Length; j++) {
                        currentFrase = currentFrase + frases[i][j];
                        texto.text = currentFrase;
                        if (hablando == false) {
                            currentFrase = "";
                            yield break;
                        }

                        yield return new WaitForSeconds(0.01f);
                    }
                    seguir = false;
                }
                
                currentFrase = "";
            }

            while (!seguir) {
                if (Input.GetKeyDown(KeyCode.Mouse1)) {
                    seguir = true;
                }

                yield return null;
            }
        }

        dejarHablar();
    }
    
    public void mostrarInter()
    {
        if (hablar)
        {
            GetComponentInChildren<InteractuarUIController>().visible();
        }
    }

    public void esconderInter()
    {
        if (hablar)
        {
            GetComponentInChildren<InteractuarUIController>().esconder();
        }
    }

    private void dejarHablar() {
        player.GetComponent<PlayerController>().mov = true;
        StopCoroutine("mostrarFrase");
        hablando = false;
        panel.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        player.GetComponent<PlayerController>().mov = true;
    }

    public void cambiarIdioma()
    {
        idioma = "Español";

        numFraseAnteriores.Add(numFrase);

        if (numFraseAnteriores.Count >= 3)
        {
            numFraseAnteriores = new List<int>();
        }
            
        while (true)
        {
            Random random = new Random();
            numFrase = random.Next(0, frasesDisponibles.Length);

            bool sePuedeNumFrase = true;
                
            for (int i = 0; i < numFraseAnteriores.Count; i++)
            {
                if (numFrase != numFraseAnteriores[i])
                {
                    sePuedeNumFrase = true;
                }
                else
                {
                    sePuedeNumFrase = false;
                    break;
                }
            }

            if (sePuedeNumFrase)
            {
                break;
            }
        }
        
        frases = dialogeController.getTextoDialogos(dialogos, hablante, frasesDisponibles[numFrase], idioma);
    }

    public void inter() {
        if (!hablando && hablar) {
            player.GetComponent<PlayerController>().mov = false;
            hablando = false;

            if (frasesDisponibles.Length > 1)
            {
                numFraseAnteriores.Add(numFrase);

                if (numFraseAnteriores.Count >= 3)
                {
                    numFraseAnteriores = new List<int>();
                }
            
                while (true)
                {
                    Random random = new Random();
                    numFrase = random.Next(0, frasesDisponibles.Length);

                    bool sePuedeNumFrase = true;
                
                    for (int i = 0; i < numFraseAnteriores.Count; i++)
                    {
                        if (numFrase != numFraseAnteriores[i])
                        {
                            sePuedeNumFrase = true;
                        }
                        else
                        {
                            sePuedeNumFrase = false;
                            break;
                        }
                    }

                    if (sePuedeNumFrase)
                    {
                        break;
                    }
                }
            }
            else
            {
                numFrase = 0;
            }

            frases = dialogeController.getTextoDialogos(dialogos, hablante, frasesDisponibles[numFrase], idioma);
            
            empezarHablar();
        }
    }
    
    public void quitar()
    {
    }
    
    public void mostrarInterQuitar()
    {
    }

    public void setHablar(bool h)
    {
        hablar = h;
    }

    public void getFraseEspecifica(int numFraseEspecifica)
    {
        frases = dialogeController.getTextoDialogos(dialogos, hablante, frasesDisponibles[numFraseEspecifica], idioma);
    }
}
