using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IdiomaOpciones : MonoBehaviour {
    
    private OpcionesContorller opcionesContorller;
    private string idioma;
    public string boton;
    private DialogeController dialogeController;
    public TextAsset dialogos;

    private void Awake() {
        opcionesContorller = GameObject.Find("OpcionesController").GetComponent<OpcionesContorller>();
        dialogeController = GetComponent<DialogeController>();
    }

    private void Start() {
        cambiarIdioma();
    }

    public void cambiarIdioma() {
        idioma = opcionesContorller.getIdioma();
        gameObject.GetComponent<TextMeshProUGUI>().text = dialogeController.getTextoDialogos(dialogos, boton, "boton", idioma)[0];
    }
    
}
