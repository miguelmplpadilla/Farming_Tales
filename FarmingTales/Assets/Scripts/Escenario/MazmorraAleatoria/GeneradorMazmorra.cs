using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GeneradorMazmorra : MonoBehaviour
{

    public GameObject[] salasDisponibles;
    public List<GameObject> salas;

    private GameObject grid;
    
    void Start()
    {
        grid = GameObject.Find("Grid");
        
        Random random = new Random();
        int numSalaGenerar = random.Next(5, 15);

        int numSalaAnterior = -1;
        int numSala = 0;
        
        for (int i = 0; i < numSalaGenerar; i++)
        {

            while (true)
            {
                random = new Random();
                numSala = random.Next(0, salasDisponibles.Length);

                if (numSala != numSalaAnterior)
                {
                    numSalaAnterior = numSala;
                    break;
                }
            }

            GameObject salaInstanciada = Instantiate(salasDisponibles[numSala], grid.transform.position, Quaternion.identity, grid.transform);
            
            if (i == 0)
            {
                salaInstanciada.transform.position = new Vector3(0, 0, 0);
            }
            else
            {
                GameObject salaAnterior = salas[i - 1].transform.GetChild(0).GetChild(0).gameObject;
                salaInstanciada.transform.position = new Vector3(salaAnterior.transform.position.x,
                    salaAnterior.transform.position.y, salaAnterior.transform.position.z);
            }
            
            salas.Add(salaInstanciada);
            
        }
    }
}
