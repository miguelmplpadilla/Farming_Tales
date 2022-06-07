using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class Generador : MonoBehaviour
{
    
    public GameObject loot;
    public GameObject suelo;

    public Sprite[] sprites;

    public string[] tipos;

    public Color colorSuelo;
    
    void Start()
    {

        suelo.GetComponent<SpriteRenderer>().color = colorSuelo;
        
        int numAnterior = 1000;
        int repeticiones = 0;
        
        bool direccion = true;
        float num = getNumaleatorio(7, 1);

        float tipo = 0;

        for (int i = 0; i < 7; i++)
        {
            while (true)
            {
                tipo = getNumaleatorio(tipos.Length-1, 0);
            
                if (numAnterior == (int)tipo)
                {
                    numAnterior = (int)tipo;
                    repeticiones++;
                }
                else
                {
                    repeticiones = 0;
                    numAnterior = (int)tipo;
                    break;
                }

                if (repeticiones < 3)
                {
                    break;
                }
            }

            float x = 0;
            
            if (direccion)
            {
                x = num;
            }
            else
            {
                x = -num;
            }
            
            GameObject prefabLoot = Instantiate(loot);

            Vector3 posicionLoot = new Vector3(x, -0.8596f,
                suelo.transform.position.z);
        
            prefabLoot.transform.position = posicionLoot;
            
            prefabLoot.GetComponent<LootController>().setDatos(tipos[(int)tipo], sprites[(int)tipo]);

            direccion = !direccion;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public float getNumaleatorio(int max, int min)
    {
        Random rand = new Random();
        double sample = rand.NextDouble();
        double scaled = (sample * (max-min)) + min;

        return (float) Math.Round(scaled);
    }
}
