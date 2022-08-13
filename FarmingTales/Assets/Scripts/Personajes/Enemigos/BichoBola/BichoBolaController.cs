using System;
using System.Collections;
using System.Collections.Generic;
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
    private bool mov = true;
    
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
                float distancia = Vector2.Distance(player.transform.position, transform.position);

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
                }
            }
            else
            {
                StopCoroutine("atacando");
                StopCoroutine("waitAtacar");
                animator.SetBool("run", false);
                atacar = true;
            }
        }
        else
        {
            StopCoroutine("atacando");
            StopCoroutine("waitAtacar");
            atacar = true;
        }
    }

    public void setHit(bool h)
    {
        hit = h;
    }

    private void LateUpdate()
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

    IEnumerator atacando()
    {
        animator.SetBool("run", false);

        yield return new WaitForSeconds(1f);
        
        animator.SetTrigger("atacar");
    }

    IEnumerator waitAtacar()
    {

        yield return new WaitForSeconds(1f);
        
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
            if (!GetComponent<SpriteRenderer>().enabled)
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
}
