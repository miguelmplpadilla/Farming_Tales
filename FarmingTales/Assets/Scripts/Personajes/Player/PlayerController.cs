using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 movement;
    public bool isAttacking = false;
    public bool isGrounded = true;
    private Rigidbody2D rigidbody;
    private Animator animator;

    public float jumpForce = 2f;
    public float speed = 2f;

    public bool mov = true;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mov)
        {
            if (!isAttacking) {
                // Movement
                float horizontalInput = Input.GetAxisRaw("Horizontal");
                movement = new Vector2(horizontalInput, 0f);

                // Flip character
                if (horizontalInput > 0f) {
                    gameObject.transform.localScale = new Vector3(1, 1, 1);
                } else if (horizontalInput < 0f) {
                    gameObject.transform.localScale = new Vector3(-1, 1, 1);
                }
            }

            if (Input.GetButtonDown("Jump") && isGrounded == true && isAttacking == false) {
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        else
        {
            animator.SetBool("run", false);
            rigidbody.velocity = Vector3.zero;
        }
    }
    
    void FixedUpdate()
    {
        if (!isAttacking && mov) {
            float horizontalVelocity = movement.normalized.x * speed;
            rigidbody.velocity = new Vector2(horizontalVelocity, rigidbody.velocity.y);

            if ((horizontalVelocity > 0 || horizontalVelocity < 0) && horizontalVelocity != 0)
            {
                animator.SetBool("run", true);
            }
            else
            {
                animator.SetBool("run", false);
            }
        }
    }
}