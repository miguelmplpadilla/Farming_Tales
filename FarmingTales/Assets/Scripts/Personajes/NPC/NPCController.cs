using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour {

    public TextAsset dialogos;

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
    private DialogeController dialogeController;

    public bool hablar = false;
    public bool hablando = false;
    public string idioma = "Español";
    public string frase = "saludo";

    private void Awake() {
        dialogeController = GetComponent<DialogeController>();
        texto = objectTexto.GetComponent<TextMeshProUGUI>();
        //animator = gameObject.GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    void Start()
    {
        idioma = "Español";
        frases = dialogeController.getTextoDialogos(dialogos, hablante, frase, idioma);
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
        if (hablar == true && hablando == false) {
            player.GetComponent<PlayerController>().mov = false;
            hablando = true;
            panel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            imagePanel.GetComponent<Image>().sprite = image;
            StartCoroutine("mostrarFrase");
            animator.SetBool("talk", true);
            if (player.transform.position.x > gameObject.transform.position.x) {
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else {
                gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
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
                if (Input.GetButtonDown("Interactuar")) {
                    seguir = true;
                }

                yield return null;
            }
        }

        dejarHablar();
    }

    private void dejarHablar() {
        player.GetComponent<PlayerController>().mov = true;
        StopCoroutine("mostrarFrase");
        hablando = false;
        panel.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        animator.SetBool("talk", false);
        player.GetComponent<PlayerController>().mov = true;
    }

    public void cambiarIdioma()
    {
        idioma = "Español";
        frases = dialogeController.getTextoDialogos(dialogos, hablante, frase, idioma);
    }

    public void inter(GameObject p) {
        if (!hablando) {
            player = p.transform.parent.gameObject;
            hablando = false;
            empezarHablar();

            player.GetComponent<PlayerController>().mov = false;
        }
    }
}
