using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BichoBolaController : MonoBehaviour
{
    private GameObject player;
    public float speed = 0.1f;
    private Animator animator;
    public bool atacar = true;
    
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        float distancia = Vector2.Distance(player.transform.position, transform.position);

        Debug.Log("Distancia: "+distancia);
        
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
}
