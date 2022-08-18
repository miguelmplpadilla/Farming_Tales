using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{

    public Animator fundidoNegro;
    private GameObject player;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
    }

    void Start()
    {
        player = GameObject.Find("Player");
        fundidoNegro.gameObject.SetActive(true);
        fundidoNegro.SetTrigger("desfundido");
    }
}
