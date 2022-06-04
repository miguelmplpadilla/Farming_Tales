using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 movement;
    private bool isAttacking = false;
    private bool isGrounded = true;
    private Rigidbody2D rigidbody;
    private Animator animator;

    public float jumpForce = 2f;
    public float speed = 2f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking) {
            // Movement
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            movement = new Vector2(horizontalInput, 0f);

            // Flip character
            if (horizontalInput < 0f) {
                //Flip();
            } else if (horizontalInput > 0f) {
                //Flip();
            }
        }

        // Is Grounded?
        //_isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        if (Input.GetButtonDown("Jump") && isGrounded == true && isAttacking == false) {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Wanna Attack?
        if (Input.GetButtonDown("Fire1") && isGrounded == true && isAttacking == false) {
            movement = Vector2.zero;
            rigidbody.velocity = Vector2.zero;
        }
    }
    
    void FixedUpdate()
    {
        if (isAttacking == false) {
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

            if (horizontalVelocity > 0 && horizontalVelocity != 0)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontalVelocity < 0 && horizontalVelocity != 0)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
