using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuarBuoneroController : MonoBehaviour
{

    private TenderoController tenderoController;
    private BuoneroController buoneroController;
    private GameObject player;
    
    private void Awake()
    {
        tenderoController = transform.parent.GetComponent<TenderoController>();
        buoneroController = transform.parent.GetComponentInChildren<BuoneroController>();
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void inter()
    {
        tenderoController.inter();
    }
    
    public void mostrarInter()
    {
        transform.parent.GetComponentInChildren<InteractuarUIController>().visible();
    }

    public void esconderInter()
    {
        transform.parent.GetComponentInChildren<InteractuarUIController>().invisibleDerecho();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("player"))
        {
            
            float escalaInteractuarX = transform.parent.GetChild(1).transform.localScale.x;

            if (escalaInteractuarX < 0)
            {
                escalaInteractuarX = -escalaInteractuarX;
            }
            
            buoneroController.stopCaminar();
            if (player.transform.position.x > transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(1f, 1f, 1f);
                transform.parent.GetChild(1).transform.localScale = new Vector3(-escalaInteractuarX, transform.parent.GetChild(1).transform.localScale.y, 1);
            }
            else
            {
                transform.parent.localScale = new Vector3(-1f, 1f, 1f);
                transform.parent.GetChild(1).transform.localScale = new Vector3(escalaInteractuarX, transform.parent.GetChild(1).transform.localScale.y, 1);
            }

           buoneroController.mov = false;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            buoneroController.mov = true;
            buoneroController.startCaminar();
        }
    }
}
