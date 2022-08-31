using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapaNivelController : MonoBehaviour
{

    public Animator fundidoNegro;
    private GameObject player;
    
    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void inter()
    {
        player.GetComponent<PlayerController>().mov = false;
        fundidoNegro.gameObject.SetActive(true);
        fundidoNegro.SetTrigger("fundido");
        
        PlayerPrefs.SetString("NivelGuardadoPartida", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetString("NivelAnterior", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.Save();
        
        //StartCoroutine("moverMapa");
        SceneManager.LoadScene("Mapa");
    }
    
    public void quitar()
    {
    }
    
    public void mostrarInterQuitar()
    {
    }

    IEnumerator moverMapa()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Mapa");
    }

    public void mostrarInter()
    {
        GetComponentInChildren<InteractuarUIController>().visible();
    }

    public void esconderInter()
    {
        GetComponentInChildren<InteractuarUIController>().invisibleDerecho();
    }
    
}
