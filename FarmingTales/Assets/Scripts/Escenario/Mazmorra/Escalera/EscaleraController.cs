using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscaleraController : MonoBehaviour
{

    private GameObject player;
    private Animator playerAnimator;
    
    public bool subido = false;
    
    private Vector2 movement;

    private bool bajar = false;

    private GameObject botonInteractuar;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        player = GameObject.Find("Player");
        playerAnimator = player.GetComponent<Animator>();
        botonInteractuar = transform.GetChild(0).gameObject;
    }

    public void inter()
    {
        player.GetComponent<PlayerController>().mov = false;

        subido = true;

        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        
        playerAnimator.SetBool("escalera", true);
        
        player.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Update()
    {
        botonInteractuar.transform.position = new Vector3(botonInteractuar.transform.position.x, player.transform.position.y+0.5f, botonInteractuar.transform.position.z);
        if (subido)
        {
            playerAnimator.SetBool("jump", false);
            playerAnimator.SetBool("run", false);
            
            if (bajar)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    StartCoroutine("bajarEscalera");
                }
            }

            bajar = true;
        }
    }

    IEnumerator bajarEscalera()
    {
        yield return new WaitForSeconds(0.01f);
        player.GetComponent<Rigidbody2D>().gravityScale = 1;
        subido = false;
        bajar = false;
        player.GetComponent<PlayerController>().mov = true;
        playerAnimator.SetInteger("subirBajarEscalera", 0);
        player.GetComponent<PlayerController>().saltar();
        playerAnimator.SetBool("escalera", false);
        playerAnimator.SetBool("jump", true);
        player.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    }

    private void LateUpdate()
    {
        if (subido)
        {
            player.transform.position = new Vector3(transform.position.x, player.transform.position.y, 1);

            float verticalInput = Input.GetAxisRaw("Vertical");
            
            movement = new Vector2(0f, verticalInput);

            float verticalVelocity = movement.normalized.y * 2;
            player.GetComponent<Rigidbody2D>().velocity =
                new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, verticalVelocity);


            Debug.Log(verticalInput);
            
            if (verticalInput > 0)
            {
                playerAnimator.SetInteger("subirBajarEscalera", 1);
            }
            else if (verticalInput < 0)
            {
                playerAnimator.SetInteger("subirBajarEscalera", 2);
            }
            else
            {
                playerAnimator.SetInteger("subirBajarEscalera", 0);
            }
            
        }
    }

    public void mostrarInter()
    {
        GetComponentInChildren<InteractuarUIController>().visible();
    }

    public void esconderInter()
    {
        GetComponentInChildren<InteractuarUIController>().esconder();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            if (subido)
            {
                player.GetComponent<PlayerController>().mov = true;
                player.GetComponent<Rigidbody2D>().gravityScale = 1;
                subido = false;
                bajar = false;
                playerAnimator.SetBool("escalera", false);
                playerAnimator.SetInteger("subirBajarEscalera", 0);
                playerAnimator.SetBool("jump", true);
                player.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
