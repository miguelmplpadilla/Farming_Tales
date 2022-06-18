using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuarController : MonoBehaviour
{

    private bool interactuar = false;
    private GameObject objInteractuado;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerController.mov)
        {
            if (interactuar)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    objInteractuado.SendMessage("inter");
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Inter"))
        {
            objInteractuado = other.gameObject;
            other.SendMessage("mostrarInter");
            interactuar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Inter"))
        {
            other.SendMessage("esconderInter");
            interactuar = false;
            objInteractuado = null;
        }
    }
}
