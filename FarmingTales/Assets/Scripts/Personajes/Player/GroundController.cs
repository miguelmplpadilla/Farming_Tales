using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{

    private PlayerController playerController;
    private Animator animator;

    private void Start()
    {
        playerController = gameObject.transform.parent.GetComponent<PlayerController>();
        animator = gameObject.transform.parent.GetComponent<Animator>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("sueloBosque") || other.CompareTag("suelo"))
        {
            playerController.isGrounded = false;
            animator.SetBool("jump", true);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("sueloBosque") || col.CompareTag("suelo"))
        {
            playerController.isGrounded = true;
            animator.SetBool("jump", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("sueloBosque") || col.CompareTag("suelo"))
        {
            transform.parent.GetComponent<AudioController>().playAudio(2);
        }
    }
}
