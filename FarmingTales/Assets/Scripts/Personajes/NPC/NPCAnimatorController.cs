using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimatorController : MonoBehaviour
{

    public int numeroNpc = 1;
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.SetInteger("npc", numeroNpc);
    }
}
