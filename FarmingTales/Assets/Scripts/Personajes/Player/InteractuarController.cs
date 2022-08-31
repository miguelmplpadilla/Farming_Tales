using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuarController : MonoBehaviour
{

    private bool interactuar = false;
    private GameObject objInteractuado;
    private PlayerController playerController;
    private Rigidbody2D rigidbody;
    private Animator animator;

    private GameObject indicador;

    private void Awake()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        rigidbody = transform.parent.GetComponent<Rigidbody2D>();
        animator = transform.parent.GetComponent<Animator>();
        indicador = GameObject.Find("Indicador");
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

                if (!indicador.GetComponent<SpriteRenderer>().enabled)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        objInteractuado.SendMessage("quitar");
                    }
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
            other.SendMessage("mostrarInterQuitar");
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
    
    public void stopAtack()
    {
        StartCoroutine("stopAtackEnum");
    }

    IEnumerator stopAtackEnum()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.parent.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        playerController.isAttacking = false;
    }
}
