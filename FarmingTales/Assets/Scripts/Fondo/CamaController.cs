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

    
    void Update()
    {
        
    }

    public void inter()
    {
        player.GetComponent<PlayerController>().mov = false;
        StartCoroutine("transicion");
    }

    IEnumerator transicion()
    {

        fundido.SetTrigger("fundido");
        
        yield return new WaitForSeconds(1f);
        
        nocheDiaController.dia();
        
        fundido.SetTrigger("desfundido");
        
        yield return new WaitForSeconds(1f);
        
        player.GetComponent<PlayerController>().mov = true;
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
