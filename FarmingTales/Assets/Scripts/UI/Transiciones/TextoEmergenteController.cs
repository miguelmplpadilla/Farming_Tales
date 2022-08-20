using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class TextoEmergenteController : MonoBehaviour
{

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void mostrarTexto(string textoMostrar)
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = textoMostrar;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("fundido"))
        {
            animator.SetTrigger("volverInvisible");
        }
        animator.SetTrigger("mostrarTexto");
    }
}
