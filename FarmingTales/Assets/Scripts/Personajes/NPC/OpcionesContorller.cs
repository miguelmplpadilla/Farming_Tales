using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpcionesContorller : MonoBehaviour {

    public string idioma = "Español";
    private List<GameObject> cambioIdioma = new List<GameObject>();
    public Animator animatorPanelOpciones;
    //private SaveGame saveGame;
    public TMPro.TMP_Dropdown dropdown;
    private bool abriendo = false;

    public bool abierto = false;

    public GameObject player;

    private void Awake() {
        //saveGame = GetComponent<SaveGame>();
        idioma = PlayerPrefs.GetString("idioma");
    }

    private void Start()
    {
        //cambioIdioma = GameObject.FindGameObjectsWithTag("Idioma").ToList();
        //cambioIdioma.AddRange(GameObject.FindGameObjectsWithTag("Interactuar").ToList());
        player = GameObject.Find("Player");
        //dropdown.value = dropdown.options.FindIndex(option => option.text == idioma);
    }

    private void Update()
    {
        /*if (!SceneManager.GetActiveScene().name.Equals("Inicio"))
        {
            if (Input.GetButtonDown("Opciones"))
            {
                if (!abierto)
                {
                    abrirOpciones();
                }
                else
                {
                    cerrarOpciones();
                }
            
            }
        }*/
    }
    
    public void guardarPartida(bool salir) {
        //saveGame.guardarPartida();
        if (salir) {
            Application.Quit();
        }
    }

    public void abrirOpciones() {
        if (!abriendo)
        {
            bool abrir = false;
            if (player != null)
            {
                abrir = !player.GetComponent<PlayerController>().mov;
            }
            
            if (!abrir)
            {
                animatorPanelOpciones.SetTrigger("abrir");
                abierto = true;
                abriendo = true;
                if (player != null)
                {
                    if (player.GetComponent<PlayerController>().mov)
                    {
                        player.GetComponent<PlayerController>().mov = false;
                    }
                }
            }
        }
    }
    
    public void cerrarOpciones() {
        if (!abriendo)
        {
            animatorPanelOpciones.SetTrigger("cerrar");
            abierto = false;
            abriendo = true;
            if (player != null)
            {
                player.GetComponent<PlayerController>().mov = true;
            }
        }
    }

    public void setAbriendo(bool a)
    {
        abriendo = a;
    }

    public void cambiarIdioma() {
        idioma = dropdown.options[dropdown.value].text;
        for (int i = 0; i < cambioIdioma.Count; i++) {
            cambioIdioma[i].SendMessage("cambiarIdioma");
        }
        PlayerPrefs.SetString("idioma", idioma);
        PlayerPrefs.Save();
    }

    public string getIdioma() {
        return idioma;
    }

    public void cambiarNivel(bool nuevaPartida) {
        if (nuevaPartida) {
            borrarPartida();
            
            PlayerPrefs.SetString("siguenteEscena", "Nivel1");
        }
        else
        {
            string nivel = "Nivel1";
            if (PlayerPrefs.HasKey("nivel"))
            {
                nivel = PlayerPrefs.GetString("nivel");
            }
            
            PlayerPrefs.SetString("siguenteEscena", nivel);
        }
        
        SceneManager.LoadScene("Load");
    }

    public void salirJuego() {
        if (PlayerPrefs.GetInt("autoGuardado") == 1)
        {
            borrarPartida();
        }
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void borrarPartida()
    {
        PlayerPrefs.DeleteKey("vida");
        PlayerPrefs.DeleteKey("balas");
        PlayerPrefs.DeleteKey("playerX");
        PlayerPrefs.DeleteKey("playerY");
        PlayerPrefs.DeleteKey("nivel");
        PlayerPrefs.DeleteKey("puntos");
        PlayerPrefs.DeleteKey("gun");
        PlayerPrefs.Save();
    }

    public void volverInicio()
    {
        SceneManager.LoadScene("Inicio");
    }
    
}
