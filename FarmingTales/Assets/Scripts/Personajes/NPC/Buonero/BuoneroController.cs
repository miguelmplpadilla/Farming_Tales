using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BuoneroController : MonoBehaviour
{

    public float speed = 1;
    private Rigidbody2D rigidbody;
    private Animator animator;

    public bool mov = false;
    private float velocity = 0f;
    private bool direccionFija = false;
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine("caminar");
    }

    IEnumerator caminar()
    {
        Random random = new Random();

        while (true)
        {
            if (!direccionFija)
            {
                random = new Random();
            
                float numTiempo = random.Next(1, 4);
            
                random = new Random();
            
                int direccion = random.Next(1, 3);

                if (direccion == 1)
                {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    velocity = new Vector2(1f, 0f).normalized.x * speed;
                }
                else
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    velocity = new Vector2(-1f, 0f).normalized.x * speed;
                }

                mov = true;
            
                animator.SetBool("run", true);

                yield return new WaitForSeconds(numTiempo);
            
                random = new Random();

                numTiempo = random.Next(1, 3);

                rigidbody.velocity = Vector2.zero;
            
                animator.SetBool("run", false);
            
                mov = false;
        
                yield return new WaitForSeconds(numTiempo);
            }
            else
            {
                animator.SetBool("run", true);
                yield return new WaitForSeconds(4f);
                direccionFija = false;
            }
        }
    }

    private void LateUpdate()
    {
        if (mov)
        {
            rigidbody.velocity = new Vector2(velocity, rigidbody.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Inter"))
        {
            StopCoroutine("caminar");
            if (transform.localScale.x == 1)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                velocity = new Vector2(-1f, 0f).normalized.x * speed;
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                velocity = new Vector2(1f, 0f).normalized.x * speed;
            }
            direccionFija = true;
            mov = true;
            StartCoroutine("caminar");
        }
    }

    /*private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine("caminar");
            if (player.transform.localScale.x > transform.localScale.x)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            mov = false;
        }
    }*/

    public void stopCaminar()
    {
        animator.SetBool("run", false);
        StopCoroutine("caminar");
    }

    public void startCaminar()
    {
        animator.SetBool("run", true);
        StartCoroutine("caminar");
    }
}
