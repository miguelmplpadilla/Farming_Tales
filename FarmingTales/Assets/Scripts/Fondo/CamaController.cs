using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaController : MonoBehaviour
{
    private GameObject player;
    private NocheDiaController nocheDiaController;
    public Animator fundido;
    
    void Start()
    {
        player = GameObject.Find("Player");
        nocheDiaController = GameObject.Find("Fondos").GetComponent<NocheDiaController>();
    }

    public void inter()
    {
        if (nocheDiaController.estado == 2)
        {
            player.GetComponent<PlayerController>().mov = false;
            StartCoroutine("transicion");
        }
    }
    
    public void quitar()
    {
    }
    
    public void mostrarInterQuitar()
    {
    }

    IEnumerator transicion()
    {
        fundido.gameObject.SetActive(true);
        
        fundido.SetTrigger("fundido");
        
        yield return new WaitForSeconds(3f);
        
        nocheDiaController.dia();
        
        fundido.SetTrigger("desfundido");
        
        yield return new WaitForSeconds(1f);
        
        player.GetComponent<PlayerController>().mov = true;
        
        yield return new WaitForSeconds(2f);
        
        fundido.gameObject.SetActive(false);
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
