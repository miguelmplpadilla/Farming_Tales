using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class AttackController : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    private PlayerController playerController;
    private Animator animator;
    private GameObject player;

    private GameObject loot = null;
    private bool looting = false;
    private InventarioController inventarioController;
    private ToolBarController toolBarController;

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
        inventarioController = GameObject.FindWithTag("toolBar").GetComponent<InventarioController>();
        toolBarController = GameObject.FindWithTag("toolBar").GetComponent<ToolBarController>();
    }

    void Update()
    {
        if (playerController.mov)
        {
            if (Input.GetButtonDown("Fire1") && playerController.isGrounded == true && playerController.isAttacking == false) {
                playerController.movement = Vector2.zero;
                rigidbody.velocity = Vector2.zero;
                animator.SetInteger("golpe", golpe);
                animator.SetTrigger("golpear");
                player.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                playerController.isAttacking = true;

                if (looting && toolBarController.position == 0)
                {
                    if (loot != null)
                    {
                        LootController lootController = loot.GetComponent<LootController>();
                        if (lootController.tipo != "oro")
                        {
                            inventarioController.anadirInventario(lootController.tipo, lootController.spr, lootController.cant, loot);
                        }
                        else
                        {
                            inventarioController.anadirDinero(lootController.cant);
                        }
                    
                        lootController.temblar();
                        lootController.life--;
                    }
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("looting"))
        {
            loot = other.transform.parent.gameObject;
            looting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("looting"))
        {
            loot = null;
            looting = false;
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
