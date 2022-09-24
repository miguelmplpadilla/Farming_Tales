using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EstrellaController : MonoBehaviour
{
    private Image imagenPez;
    private Animator animator;

    private int balanceos = 0;

    private PescaController pescaController;

    private void Start()
    {
        imagenPez = transform.Find("ImagenPez").GetComponent<Image>();
        animator = GetComponent<Animator>();
        pescaController = GameObject.Find("JuegoPesca").GetComponent<PescaController>();
    }

    public void startAnimation(Sprite pez)
    {
        imagenPez.sprite = pez;
        animator.SetBool("grande", true);
    }

    public void sumarBalanceos()
    {
        balanceos++;
        if (balanceos >= 2)
        {
            pescaController.empezarPescar = true;
            animator.SetBool("grande", false);
        }
    }
}
