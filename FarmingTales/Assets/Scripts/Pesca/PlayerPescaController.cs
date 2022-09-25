using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPescaController : MonoBehaviour
{

    private Animator animator;

    public int numAnteriorPose;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void setPoseCero(int numAnterior)
    {
        numAnteriorPose = numAnterior;
        animator.SetInteger("pose", 0);
    }
}
