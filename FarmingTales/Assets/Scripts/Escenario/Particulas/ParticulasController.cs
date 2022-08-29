using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulasController : MonoBehaviour
{
    [System.Serializable]
    public class colores
    {
        public Color color1;
        public Color color2;
    }

    public List<colores> coloresList = new List<colores>();

    private ParticleSystem particulas;
    
    private void Awake()
    {
        particulas = GetComponent<ParticleSystem>();
    }

    public void startParticulas(int numColor)
    {
        ParticleSystem.MainModule mainParticulas = particulas.main;

        mainParticulas.startColor = new ParticleSystem.MinMaxGradient(coloresList[numColor].color1, coloresList[numColor].color2);
        
        particulas.Play();
    }
}
