using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovimientoAnimales : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    private float[] velocidades = {0.1f, -0.1f, 0.05f, -0.05f};
    private float[] velocidadesPositivas = {0.1f, 0.05f};
    private float[] velocidadesNegativas = {-0.1f, -0.05f};
    public float direccion = 0;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        direccion = velocidades[Random.Range(0, velocidades.Length)];
        if (direccion > 0)
        {
            rigidbody.transform.localScale = new Vector2(1f, 1f);
        }
        else
        {
            rigidbody.transform.localScale = new Vector2(-1f, 1f);
        }
    }

    void Update()
    {
        rigidbody.velocity = new Vector2(direccion, rigidbody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("paredGranja"))
        {
            if (direccion > 0)
            {
                direccion = velocidadesNegativas[Random.Range(0, velocidadesNegativas.Length)];
                rigidbody.transform.localScale = new Vector2(-1f, 1f);
            }
            else
            {
                direccion = velocidadesPositivas[Random.Range(0, velocidadesPositivas.Length)];
                rigidbody.transform.localScale = new Vector2(1f, 1f);
            }
        }
    }
}
