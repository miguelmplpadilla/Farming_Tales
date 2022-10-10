using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class FlechaController : MonoBehaviour
{
    private bool tocandoLinea = false;
    private bool correcto = false;

    private int tiempoActual = 0;
    
    public string direccion;
    public KeyCode key = KeyCode.LeftArrow;
    public float speedFlecha = -100;

    private Rigidbody2D rigidbody;

    private PescaController pescaController;

    private Animator playerPescadorAnimator;

    public int numRandomAnteriorPose = 0;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerPescadorAnimator = GameObject.Find("PlayerPescador").GetComponent<Animator>();
        pescaController = GameObject.Find("JuegoPesca").GetComponent<PescaController>();
    }

    private void Update()
    {
        if (tocandoLinea && !correcto)
        {
            if (Input.GetKeyDown(key))
            {
                GetComponent<SpriteRenderer>().color = Color.green;
                transform.localScale = new Vector3(0.45f, 0.45f, 1);
                //Debug.Log("Pulsacion flecha "+direccion+" correcta");
                pescaController.sumarTeclaCorrecta();
                numRandomAnteriorPose = playerPescadorAnimator.GetInteger("pose");
                while (true)
                {
                    Random random = new Random();
                    int numRandom = random.Next(1, 5);
                    if (numRandom != numRandomAnteriorPose)
                    {
                        Debug.Log("Numero Random: "+numRandom+" Numero Random Anterior: "+numRandomAnteriorPose);
                        playerPescadorAnimator.SetInteger("pose", numRandom);
                        break;
                    }
                }
                
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