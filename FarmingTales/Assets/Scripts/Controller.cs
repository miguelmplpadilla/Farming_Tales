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
    public bool pausarPartida = true;

    public int contadorTiempoTranscurridoJuego = 0;
    private bool noMoverPlayer = false;

    private void Awake()
    {
        if (guardarPartida)
        {
            if (PlayerPrefs.HasKey("ContadorTiempoTranscurridoJuego"))
            {
                contadorTiempoTranscurridoJuego = PlayerPrefs.GetInt("ContadorTiempoTranscurridoJuego");
            }

            StartCoroutine("contadorTiempoJuego");
        }
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

            if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name+"Visitado"))
            {
                player.transform.position = GameObject.Find("InicioPueblo").transform.position;
                PlayerPrefs.SetString(SceneManager.GetActiveScene().name+"Visitado", "Visitado");
            }
        }

        if (SceneManager.GetActiveScene().name.Equals("PantallaInicio"))
        {
            if (!PlayerPrefs.HasKey("NivelGuardadoPartida"))
            {
                GameObject.Find("NuevaPartida").GetComponent<RectTransform>().anchoredPosition =  new Vector3(0f,-80, 1);
                GameObject.Find("CargarPartida").SetActive(false);
            }
        }

    }

    private void Update()
    {
        if (pausarPartida)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pausarDespausar();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene("Mapa");
        }
    }

    public void pausarDespausar()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController.mov)
        {
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
    }

    public void salirJuego()
    {
        if (SceneManager.GetActiveScene().name.Equals("PantallaInicio"))
        {
            Application.Quit();
        }
        else
        {
            PlayerPrefs.SetString("NivelGuardadoPartida", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("PantallaInicio");
        }
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
            PlayerPrefs.SetString("NivelGuardadoPartida", SceneManager.GetActiveScene().name);
            yield return new WaitForSeconds(300);
        }
    }

    public void cargarPartida()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("NivelGuardadoPartida"));
    }

    public void nuevaPartida()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("PuebloInicio");
    }

    IEnumerator contadorTiempoJuego()
    {
        while (true)
        {
            contadorTiempoTranscurridoJuego++;
            PlayerPrefs.SetInt("ContadorTiempoTranscurridoJuego", contadorTiempoTranscurridoJuego);
            yield return new WaitForSeconds(1f);
        }
    }

}
