using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscaleraController : MonoBehaviour
{

    private GameObject player;
    
    public bool subido = false;
    
    private Vector2 movement;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void inter()
    {
        player.GetComponent<PlayerController>().mov = false;

        subido = true;

        player.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    private void Update()
    {
        if (subido)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                StartCoroutine("bajarEscalera");
            }
        }
    }

    IEnumerator bajarEscalera()
    {
        yield return new WaitForSeconds(0.01f);
        player.GetComponent<Rigidbody2D>().gravityScale = 1;
        subido = false;
        player.GetComponent<PlayerController>().mov = true;
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
        }
    }

    public void mostrarInter()
    {
        //GetComponentInChildren<InteractuarUIController>().visibleDerecho();
    }

    public void esconderInter()
    {
        //GetComponentInChildren<InteractuarUIController>().invisibleDerecho();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            player.GetComponent<PlayerController>().mov = true;
            player.GetComponent<Rigidbody2D>().gravityScale = 1;
            subido = false;
        }
    }
}
