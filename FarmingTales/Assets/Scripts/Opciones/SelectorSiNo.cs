using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorSiNo : MonoBehaviour
{
    public string metodoEjecutar;

    private GameObject textoSiNo;
    private GameObject npc;

    private void Start()
    {
        textoSiNo = transform.Find("TextoSiNo").gameObject;
    }

    public void mostrarTexto(string text, string metodo, GameObject hablante)
    {
        npc = hablante;
        metodoEjecutar = metodo;
        textoSiNo.GetComponent<TextMeshProUGUI>().text = text;
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    public void ejecutarSi()
    {
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
        gameObject.SendMessage(metodoEjecutar);
    }

    public void ejecutarNo()
    {
        npc.GetComponent<NPCController>().pararTemporal = false;
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
    }

    public void moverMazmorraAleatoria()
    {
        SceneManager.LoadScene("MazmorraAleatoria");
    }
    
    public void moverPesca()
    {
        SceneManager.LoadScene("Pesca");
    }
    
}
