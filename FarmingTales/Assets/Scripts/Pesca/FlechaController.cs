using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlechaController : MonoBehaviour
{
    private bool tocandoLinea = false;
    private bool correcto = false;

    private int tiempoActual = 0;
    
    public string direccion;
    public KeyCode key = KeyCode.LeftArrow;
    public float speedFlecha = -100;

    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (tocandoLinea)
        {
            if (Input.GetKeyDown(key))
            {
                GetComponent<SpriteRenderer>().color = Color.green;
                transform.localScale = new Vector3(0.45f, 0.45f, 1);
                //Debug.Log("Pulsacion flecha "+direccion+" correcta");
                correcto = true;
            }
        }

        Vector2 destination = new Vector2(transform.position.x, GameObject.Find("LineaFinRitmo").transform.position.y);

        Vector2 movement = new Vector2(0, speedFlecha);
        
        float velocity = movement.normalized.y * speedFlecha;
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, -velocity);
    }
    
    private void FixedUpdate()
    {
        tiempoActual += 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LineaRitmo"))
        {
            //Debug.Log("Tiempo que tarda en llegar flecha: "+tiempoActual);
            tocandoLinea = true;
        }
        
        if (other.CompareTag("LineaFinRitmo"))
        {
            if (!correcto)
            {
                tocandoLinea = false;
                GetComponent<SpriteRenderer>().color = Color.red;
                //Debug.Log("Pulsacion flecha "+direccion+" incorrecta");
            }
        }

        if (other.CompareTag("LineaEliminacionFlecha"))
        {
            Destroy(gameObject);
        }
    }
}
