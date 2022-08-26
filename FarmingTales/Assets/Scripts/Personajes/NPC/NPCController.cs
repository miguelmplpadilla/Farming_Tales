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
    public GameObject panel;
    public GameObject objectTexto;
    private TextMeshProUGUI texto;
    public GameObject imagePanel;

    private List<String> frases = new List<string>();
    private DialogeController dialogeController = new DialogeController();

    public bool hablando = false;
    public string idioma = "Español";
    public string[] frasesDisponibles;

    private void Awake() {
        texto = objectTexto.GetComponent<TextMeshProUGUI>();
        //animator = gameObject.GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    void Start()
    {
        idioma = "Español";

        /*Random random = new Random();
        int numFrase = random.Next(0, frasesDisponibles.Length);
        
        frases = dialogeController.getTextoDialogos(dialogos, hablante, frasesDisponibles[numFrase], idioma);*/
    }


    /*void Update()
    {
        if (hablar == true && hablando == false)
        {
            if (Input.GetKeyDown(KeyCode.F)) {
                player.GetComponent<PlayerController>().mov = false;
                hablando = true;
                panel.SetActive(true);
                imagePanel.GetComponent<Image>().sprite = image;
                StartCoroutine("mostrarFrase");
                animator.SetBool("talk",true);
                if (player.transform.position.x > gameObject.transform.position.x)
                {
                    gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                }
                else
                {
                    gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
                }
            }
        }
    }*/

    private void empezarHablar() {
        if (hablando == false) {
            hablando = true;
            panel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            imagePanel.GetComponent<Image>().sprite = image;
            StartCoroutine("mostrarFrase");
            if (player.transform.position.x > gameObject.transform.position.x) {
                gameObject.transform.localScale = new Vector3(2.9623f, 2.9623f, 1f);
            }
            else {
                gameObject.transform.localScale = new Vector3(-2.9623f, 2.9623f, 1f);
            }
        }
    }
    
    IEnumerator mostrarFrase() {
        bool seguir = true;
        for (int i = 0; i < frases.Count; i++) {
            if (seguir == true) {
                if (frases[i].Equals("execute1")) {
                    seguir = true;
                } else if (frases[i].Equals("execute2")) {
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
        GetComponentInChildren<InteractuarUIController>().visible();
    }

    public void esconderInter()
    {
        GetComponentInChildren<InteractuarUIController>().esconder();
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

        int numFraseAnterior = numFrase;
        while (true)
        {
            Random random = new Random();
            numFrase = random.Next(0, frasesDisponibles.Length);

            if (numFrase != numFraseAnterior)
            {
                break;
            }
        }
        
        frases = dialogeController.getTextoDialogos(dialogos, hablante, frasesDisponibles[numFrase], idioma);
    }

    public void inter() {
        if (!hablando) {
            player.GetComponent<PlayerController>().mov = false;
            hablando = false;
            
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
            
            empezarHablar();
        }
    }
}
