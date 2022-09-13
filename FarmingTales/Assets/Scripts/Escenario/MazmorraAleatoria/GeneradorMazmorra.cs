using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class GeneradorMazmorra : MonoBehaviour
{

    public GameObject salaInicio;

    public GameObject[] salasDisponibles;
    public List<GameObject> salas;

    public GameObject girar1;
    public GameObject girar2;

    private GameObject grid;
    
    void Start()
    {
        grid = GameObject.Find("Grid");
        
        Random random = new Random();
        int numSalaGenerar = random.Next(5, 20);

        int numSalaAnterior = -1;
        int numSala = 0;

        Vector3 localScale = new Vector3(1, 1, 1);

        GameObject salaAnterior = null;
        
        GameObject salaInstanciadaInicio = Instantiate(salaInicio, grid.transform.position, Quaternion.identity, grid.transform);
        
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
            
            salaInstanciada.transform.localScale = localScale;
            
            if (i == 0)
            {
                salaAnterior = salaInstanciadaInicio.transform.GetChild(0).GetChild(0).gameObject;
                salaInstanciada.transform.position = new Vector3(salaAnterior.transform.position.x,
                    salaAnterior.transform.position.y, salaAnterior.transform.position.z);
            }
            else
            {
                salaAnterior = salas[i - 1].transform.GetChild(0).GetChild(0).gameObject;
                salaInstanciada.transform.position = new Vector3(salaAnterior.transform.position.x,
                    salaAnterior.transform.position.y, salaAnterior.transform.position.z);
            }

            salas.Add(salaInstanciada);
            
            random = new Random();
            int numGirar = random.Next(0, 10);

            if (numGirar == 8 && ((i+1)+2) < numSalaGenerar)
            {
                if (localScale.x == 1)
                {
                    salaInstanciada = Instantiate(girar1, grid.transform.position, Quaternion.identity, grid.transform);
                    
                    salaAnterior = salas[i].transform.GetChild(0).GetChild(0).gameObject;
                    salaInstanciada.transform.position = new Vector3(salaAnterior.transform.position.x,
                        salaAnterior.transform.position.y, salaAnterior.transform.position.z);
                    
                    salas.Add(salaInstanciada);
                    
                    salaAnterior = salaInstanciada.transform.GetChild(0).GetChild(0).gameObject;
                    
                    salaInstanciada = Instantiate(girar2, grid.transform.position, Quaternion.identity, grid.transform);
                    
                    salaInstanciada.transform.position = new Vector3(salaAnterior.transform.position.x,
                        salaAnterior.transform.position.y, salaAnterior.transform.position.z);
                    
                    salas.Add(salaInstanciada);
                    
                    localScale = new Vector3(-1, 1, 1);

                    i += 2;
                }
                else
                {
                    
                    salaAnterior = salaInstanciada.transform.GetChild(0).GetChild(0).gameObject;
                    
                    salaInstanciada = Instantiate(girar1, grid.transform.position, Quaternion.identity, grid.transform);

                    salaInstanciada.transform.localScale = new Vector3(-1, 1, 1);
                    
                    salaInstanciada.transform.position = new Vector3(salaAnterior.transform.position.x,
                        salaAnterior.transform.position.y, salaAnterior.transform.position.z);
                    
                    salas.Add(salaInstanciada);
                    
                    salaAnterior = salaInstanciada.transform.GetChild(0).GetChild(0).gameObject;
                    
                    salaInstanciada = Instantiate(girar2, grid.transform.position, Quaternion.identity, grid.transform);
                    
                    salaInstanciada.transform.localScale = new Vector3(-1, 1, 1);
                    
                    salaInstanciada.transform.position = new Vector3(salaAnterior.transform.position.x,
                        salaAnterior.transform.position.y, salaAnterior.transform.position.z);
                    
                    salas.Add(salaInstanciada);
                    
                    localScale = new Vector3(1, 1, 1);
                    
                    i += 2;
                }
            }
            
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
