using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GeneradorEnemigos : MonoBehaviour
{

    public GameObject[] enemigos;
    private NocheDiaController nocheDiaController;

    private bool enemigosGenerados = false;
    
    void Start()
    {
        nocheDiaController = GameObject.Find("Fondos").GetComponent<NocheDiaController>();
    }

   
    void Update()
    {
        if (!enemigosGenerados)
        {
            if (nocheDiaController.estado == 2)
            {
                Debug.Log("Creando enemigos");
                for (int i = 0; i < 4; i++)
                {
                    Random random = new Random();
                    int numEnemigo = random.Next(0, enemigos.Length);

                    GameObject enemigo = Instantiate(enemigos[numEnemigo]);
                    enemigo.transform.position = new Vector3(transform.position.x - (i+1), transform.position.y, 1);

                    enemigo.GetComponent<SpriteRenderer>().enabled = false;
                    
                    enemigo.SendMessage("startComprobarPosicionCrear");
                }

                Debug.Log("Enemigos creados");
            
                enemigosGenerados = true;
            }
        }
    }
}
