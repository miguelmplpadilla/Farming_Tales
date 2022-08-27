using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class HistoriaController : MonoBehaviour
{

    public string historia;
    public int puntoControl;
    
    private CinemachineVirtualCamera camara;

    public Animator transicion;
    public Animator puerta;

    public GameObject[] enemigos;
    public GameObject npcSalvar;
    public GameObject rey;

    private GameObject player;
    
    private void Start()
    {
        player = GameObject.Find("Player");
        camara = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        cargarHistoria();

        if (puntoControl == 0)
        {
            camara.Follow = npcSalvar.transform;
            npcSalvar.GetComponent<NPCController>().inter();
            npcSalvar.GetComponent<NPCController>().hablar = false;
        }
        
    }

    public void moverCamaraPlayer()
    {
        camara.Follow = player.transform;
    }

    public void guardadorHistoria()
    {
        PlayerPrefs.SetInt(historia, puntoControl);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        if (puntoControl == 0)
        {
            bool existenEnemigos = false;
            for (int i = 0; i < enemigos.Length; i++)
            {
                if (enemigos[i] != null)
                {
                    existenEnemigos = true;
                }
            }

            if (!existenEnemigos)
            {
                npcSalvar.GetComponent<NPCController>().hablar = true;
                npcSalvar.GetComponent<NPCController>().frasesDisponibles[0] = "Dialogo2";
            }
        }
    }

    public void startTransicionHistoriaCastillo()
    {
        StartCoroutine("transicionHistoriaCastillo");
    }

    IEnumerator transicionHistoriaCastillo()
    {
        yield return new WaitForSeconds(0.05f);
        
        player.GetComponent<PlayerController>().mov = false;
        transicion.gameObject.SetActive(true);
        transicion.SetTrigger("fundido");

        yield return new WaitForSeconds(3f);

        player.transform.position = GameObject.Find("PosicionPlayerCastillo").transform.position;
        npcSalvar.transform.position = GameObject.Find("PosicionNpcCastillo").transform.position;
        npcSalvar.transform.localScale = new Vector3(-2.9623f, 2.9623f, 1);
        player.transform.localScale = new Vector3(-1, 1, 1);
        
        transicion.SetTrigger("desfundido");

        yield return new WaitForSeconds(3f);
        
        transicion.gameObject.SetActive(false);
        
        camara.Follow = rey.transform;
        rey.GetComponent<NPCController>().inter();
        rey.GetComponent<NPCController>().hablando = false;
    }

    IEnumerator transicionArribaCastillo()
    {
        yield return new WaitForSeconds(0.05f);
        
        player.GetComponent<PlayerController>().mov = false;
        transicion.gameObject.SetActive(true);
        transicion.SetTrigger("fundido");

        yield return new WaitForSeconds(3f);

        player.transform.position = GameObject.Find("PosicionPlayerArribaCastillo").transform.position;
        
        transicion.SetTrigger("desfundido");
        
        player.GetComponent<PlayerController>().mov = true;
        moverCamaraPlayer();

        yield return new WaitForSeconds(3f);
        
        transicion.gameObject.SetActive(false);
        
        puerta.SetTrigger("abrir");

        puntoControl = 1;
        guardadorHistoria();
    }

    public void cargarHistoria()
    {
        if (PlayerPrefs.HasKey(historia))
        {
            
            int puntoControl = PlayerPrefs.GetInt(historia);

            if (puntoControl == 1)
            {
            
            
            
            }
            
        }
        else
        {
            puntoControl = 0;
        }
    }
    
}
