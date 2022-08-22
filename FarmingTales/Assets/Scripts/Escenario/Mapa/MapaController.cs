using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapaController : MonoBehaviour
{

    private GameObject botones;
    private GameObject panelMoverNivel;

    private GameObject nivelMover;

    public Animator fundidoAnimator;
    private CinemachineVirtualCamera camara;

    private bool mov = true;
    
    void Start()
    {
        camara = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        botones = GameObject.Find("Botones");
        panelMoverNivel = GameObject.Find("MoverNivel");
        
        panelMoverNivel.SetActive(false);
    }

    public void zoomNivel(GameObject nivel)
    {
        nivelMover = nivel;
        botones.SetActive(false);
        camara.Follow = nivel.transform;
        camara.m_Lens.OrthographicSize = 1.3f;
        panelMoverNivel.SetActive(true);
    }

    public void volverMapaCompleto()
    {
        panelMoverNivel.SetActive(false);
        camara.Follow = transform;
        camara.m_Lens.OrthographicSize = 7.8f;
        botones.SetActive(true);
    }

    public void volverNivel()
    {
        panelMoverNivel.SetActive(false);
        mov = false;
        fundidoAnimator.gameObject.SetActive(true);

        string nivelAnterior = PlayerPrefs.GetString("NivelAnterior");
        SceneManager.LoadScene(nivelAnterior);

        //StartCoroutine("rutinaMoverNivel", nivelAnterior);
    }

    public void moverNivel()
    {
        if (mov)
        {
            panelMoverNivel.SetActive(false);
            mov = false;
            fundidoAnimator.gameObject.SetActive(true);
        
            StartCoroutine("rutinaMoverNivel", nivelMover.name);
        }
    }

    IEnumerator rutinaMoverNivel(string nivel)
    {
        fundidoAnimator.SetTrigger("fundido");

        yield return new WaitForSeconds(3f);
        
        SceneManager.LoadScene(nivel);
    }
    
}
