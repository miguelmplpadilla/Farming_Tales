using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaEnemigoController : MonoBehaviour
{

    public int vida = 2;
    private Animator animator;

    private bool muriendo = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitBoxPlayer"))
        {
            if (!muriendo)
            {
                gameObject.SendMessage("setHit", true);
                vida--;
                if (vida <= 0)
                {
                    animator.SetTrigger("muerte");
                    muriendo = true;
                }
                else
                {
                    animator.SetTrigger("hit");
                }
            }
        }
    }

    public void stopHit()
    {
        gameObject.SendMessage("setHit", false);
    }

    public void death()
    {
        Destroy(gameObject);
    }
}
