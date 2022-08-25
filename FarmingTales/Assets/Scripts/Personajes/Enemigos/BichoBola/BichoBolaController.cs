using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class BichoBolaController : MonoBehaviour
{
    private GameObject player;
    public float speed = 0.1f;
    public int dano = 1;
    private Animator animator;
    public bool atacar = true;
    public bool hit = false;
    private bool acabadoCrear = true;
    public bool mov = true;

    public float distancia = 0;

    public bool noGirar = false;
    
    private bool pararEnemigo = false;
    
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (mov)
        {
            if (!hit)
            {
                distancia = Vector2.Distance(player.transform.position, transform.position);

                if (distancia < 5)
                {
                    if (distancia > 1)
                    {
                        if (atacar)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                            animator.SetBool("run", true);
                        }
                    }
                    else
                    {
                        if (atacar)
                        {
                            StartCoroutine("atacando");
                            atacar = false;
                        }
                    }
                }
                else
                {
                    StopCoroutine("atacando");
                    StopCoroutine("waitAtacar");
                    atacar = true;
                    hit = false;
                }
            }
            else
            {
                StopCoroutine("atacando");
                StopCoroutine("waitAtacar");
                animator.SetBool("run", false);
                atacar = true;
                hit = false;
            }
        }
        else
        {
            StopCoroutine("atacando");
            StopCoroutine("waitAtacar");
            atacar = true;
            hit = false;
        }
    }

    public void setHit(bool h)
    {
        hit = h;
    }

    private void LateUpdate()
    {
        if (!noGirar)
        {
            if (player.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }   
        }
    }

    IEnumerator atacando()
    {
        animator.SetBool("run", false);
        noGirar = true;

        yield return new WaitForSeconds(0.5f);
        
        animator.SetTrigger("atacar");
    }

    IEnumerator waitAtacar()
    {

        yield return new WaitForSeconds(1f);

        noGirar = false;
        animator.SetBool("run", true);
        
        atacar = true;
    }

    public void setWaitAtacar()
    {
        StartCoroutine("waitAtacar");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("BoxCamara"))
        {
            if (!GetComponent<SpriteRenderer>().enabled)
            {
                mov = false;
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BoxCamara"))
        {
            if (!GetComponent<SpriteRenderer>().enabled && !pararEnemigo)
            {
                mov = true;
                GetComponent<SpriteRenderer>().enabled = true;
                acabadoCrear = false;
            }
        }
    }

    public void startComprobarPosicionCrear()
    {
        StartCoroutine("comprobarPosicionCrear");
    }

    IEnumerator comprobarPosicionCrear()
    {
        yield return new WaitForSeconds(1f);
        if (mov)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void stopEnemigo()
    {
        pararEnemigo = true;
        mov = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public void startEnemigo()
    {
        pararEnemigo = false;
        mov = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
