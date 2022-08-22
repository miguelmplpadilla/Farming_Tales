using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{

    public Animator fundidoNegro;
    private GameObject player;
    private GameObject panelPausa;

    public bool paused = false;
    public bool guardarPartida;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
    }

    void Start()
    {
        player = GameObject.Find("Player");
        fundidoNegro.gameObject.SetActive(true);
        fundidoNegro.SetTrigger("desfundido");
        panelPausa = GameObject.Find("PanelPausa");

        if (guardarPartida)
        {
            StartCoroutine("guardadoPartida");
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausarDespausar();
        }
    }

    public void pausarDespausar()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (!paused)
        {
            panelPausa.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            playerController.mov = false;
            Time.timeScale = 0;
            paused = true;
        }
        else
        {
            panelPausa.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
            playerController.mov = true;
            Time.timeScale = 1;
            paused = false;
        }
    }

    public void salirJuego()
    {
        Application.Quit();
    }

    public void abrirMapa()
    {
        //PlayerPrefs.SetString("NivelAnterior", SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        SceneManager.LoadScene("Mapa");
    }

    IEnumerator guardadoPartida()
    {
        while (true)
        {
            yield return new WaitForSeconds(300);
            PlayerPrefs.SetString("NivelGuardadoPartida", SceneManager.GetActiveScene().name);
        }
    }

}
