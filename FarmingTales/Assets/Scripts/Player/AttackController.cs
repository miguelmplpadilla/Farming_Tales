using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    private PlayerController playerController;
    private Animator animator;
    private GameObject player;

    public int golpe = 2;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("player");
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && playerController.isGrounded == true && playerController.isAttacking == false) {
            playerController.movement = Vector2.zero;
            rigidbody.velocity = Vector2.zero;
            animator.SetInteger("golpe", golpe);
            animator.SetTrigger("golpear");
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            playerController.isAttacking = true;
        }
    }

    public void stopAtack()
    {
        StartCoroutine("stopAtackEnum");
    }

    IEnumerator stopAtackEnum()
    {
        yield return new WaitForSeconds(0.1f);
        player.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        playerController.isAttacking = false;
    }
}
