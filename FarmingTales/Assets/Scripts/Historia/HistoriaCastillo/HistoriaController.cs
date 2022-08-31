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
    public GameObject npcMazmorraSalvar;
    public GameObject rey;

    public GameObject[] NPCsMazmorraMover;

    private GameObject player;

    private TextoEmergenteController textoEmergenteController;
    
    private InventarioController inventarioController;
    
    private void Start()
    {
        inventarioController = GameObject.FindWithTag("toolBar").GetComponent<InventarioController>();
        textoEmergenteController = GameObject.Find("PanelTextoEmergente").GetComponent<TextoEmergenteController>();
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
        if (puntoControl == 3)
        {
            inventarioController.anadirDinero(2000);
        }
        
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
        player.transform.localScale = new Vector3(-1, 1, 1);
        
        transicion.SetTrigger("desfundido");
        
        camara.Follow = rey.transform;
        rey.GetComponent<NPCController>().inter();
        rey.GetComponent<NPCController>().hablar = false;

        yield return new WaitForSeconds(3f);

        transicion.gameObject.SetActive(false);
    }

    IEnumerator transicionArribaCastillo(int pControl)
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

        puntoControl = pControl;
        guardadorHistoria();
        textoEmergenteController.mostrarTexto("Punto de control alcanzado, historia guardada");
    }

    IEnumerator transicionMazmorraCastillo()
    {
        yield return new WaitForSeconds(0.05f);
        
        player.GetComponent<PlayerController>().mov = false;
        transicion.gameObject.SetActive(true);
        transicion.SetTrigger("fundido");

        yield return new WaitForSeconds(3f);

        player.transform.position = GameObject.Find("PosicionPlayerCastillo").transform.position;
        npcSalvar.transform.position = GameObject.Find("PosicionNpcCastillo").transform.position;
        player.transform.localScale = new Vector3(-1, 1, 1);
        
        transicion.SetTrigger("desfundido");
        
        camara.Follow = rey.transform;
        rey.GetComponent<NPCController>().frasesDisponibles[0] = "Dialogo2";
        rey.GetComponent<NPCController>().inter();
        rey.GetComponent<NPCController>().hablar = false;

        yield return new WaitForSeconds(3f);

        transicion.gameObject.SetActive(false);
        
        puntoControl = 2;
        guardadorHistoria();
        textoEmergenteController.mostrarTexto("Punto de control alcanzado, historia guardada");
    }

    public void cargarHistoria()
    {
        if (PlayerPrefs.HasKey(historia))
        {
            
            puntoControl = PlayerPrefs.GetInt(historia);

            if (puntoControl >= 1)
            {

                for (int i = 0; i < enemigos.Length; i++)
                {
                    Destroy(enemigos[i]);
                }
                
                npcSalvar.transform.position = GameObject.Find("PosicionNpcCastillo").transform.position;
                
                puerta.SetTrigger("abrir");
            
            }
            
            if (puntoControl >= 2)
            {
                if (puntoControl == 2)
                {
                    player.GetComponent<PlayerController>().mov = false;
                    player.transform.position = GameObject.Find("PosicionPlayerCastillo").transform.position;
                    npcSalvar.transform.position = GameObject.Find("PosicionNpcCastillo").transform.position;
                    player.transform.localScale = new Vector3(-1, 1, 1);
                
                    camara.Follow = rey.transform;
                    rey.GetComponent<NPCController>().frasesDisponibles[0] = "Dialogo2";
                    rey.GetComponent<NPCController>().inter();
                    rey.GetComponent<NPCController>().hablar = false;
                }

                for (int i = 0; i < NPCsMazmorraMover.Length; i++)
                {
                    NPCsMazmorraMover[i].SendMessage("setHablar", true);
                    NPCsMazmorraMover[i].transform.position = GameObject.Find("Posicion" + NPCsMazmorraMover[i].name).transform.position;
                }
            
                npcMazmorraSalvar.transform.position = GameObject.Find("Posicion" + npcMazmorraSalvar.name).transform.position;
                npcMazmorraSalvar.GetComponent<NPCController>().frasesDisponibles[0] = "Dialogo2";
            }
            else
            {
                for (int i = 0; i < NPCsMazmorraMover.Length; i++)
                {
                    NPCsMazmorraMover[i].SendMessage("setHablar", false);
                }
            }
        }
        else
        {
            puntoControl = 0;
        }
    }
    
}
