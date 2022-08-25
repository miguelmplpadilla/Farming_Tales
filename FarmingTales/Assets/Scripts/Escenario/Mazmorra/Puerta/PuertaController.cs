using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaController : MonoBehaviour
{
    public bool cerrar;
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (cerrar)
        {
            animator.SetTrigger("cerrar");
        }
    }
}
