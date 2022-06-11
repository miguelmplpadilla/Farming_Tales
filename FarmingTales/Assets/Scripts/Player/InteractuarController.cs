using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuarController : MonoBehaviour
{

    private bool interactuar = false;
    private GameObject interactuado;
    
    void Update()
    {
        if (interactuar)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                interactuado.SendMessage("inter");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Inter"))
        {
            interactuado = other.gameObject;
            interactuar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Inter"))
        {
            interactuar = false;
            interactuado = null;
        }
    }
}
