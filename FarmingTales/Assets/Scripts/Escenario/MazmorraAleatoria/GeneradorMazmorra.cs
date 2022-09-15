using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class GeneradorMazmorra : MonoBehaviour
{
    
    [System.Serializable]
    public class ObjetoCofre
    {
        public string id;
        public int cantMin;
        public int cantMax;
    }

    public GameObject salaInicio;

    public GameObject[] enemigos;
    public GameObject[] objetosColocacion;
    public ObjetoCofre[] objectosCofre;
    
    public GameObject salaFinal;

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

            if (localScale.x < 0)
            {
                GameObject escaleras = null;

                try
                {
                    escaleras = salaInstanciada.transform.Find("Escaleras").gameObject;
                }
                catch (Exception e)
                {
                    escaleras = null;
                }

                if (escaleras != null)
                {
                    foreach (Transform child in escaleras.transform)
                    {
                        child.localScale = new Vector3(-child.localScale.x, child.localScale.y, 1);
                    }
                }
            }

            GameObject padreInstancias = null;
            try
            {
                padreInstancias = salaInstanciada.transform.Find("Instancias").gameObject;
            }
            catch (Exception e)
            {
                padreInstancias = null;
            }

            if (padreInstancias != null)
            {
                random = new Random();
                foreach (Transform child in padreInstancias.transform)
                {
                    int numInstanciar = random.Next(0, 5);

                    GameObject instanciado = null;
                    Vector2 posicionInstancia = new Vector2(0,0);

                    if (numInstanciar == 0)
                    {
                        int numEnemigoInstanciar = random.Next(0, enemigos.Length);
                        instanciado = Instantiate(enemigos[numEnemigoInstanciar], salaInstanciada.transform.position, Quaternion.identity, salaInstanciada.transform);
                        
                        posicionInstancia =
                            new Vector2(child.transform.position.x, child.transform.position.y + 0.1f);
                    } else if (numInstanciar >= 1)
                    {
                        int numObjetoInstanciar = random.Next(0, objetosColocacion.Length);
                        instanciado = Instantiate(objetosColocacion[numObjetoInstanciar], salaInstanciada.transform.position, Quaternion.identity, salaInstanciada.transform);
                        
                        posicionInstancia =
                            new Vector2(child.transform.position.x, child.transform.position.y);

                        if (instanciado.name.Equals("cofre(Clone)"))
                        {
                            anadirObjetosCofre(instanciado);
                        }
                    }

                    if (localScale.x < 0)
                    {
                        instanciado.transform.localScale = new Vector3(-1, 1, 1);
                    }
                    
                    instanciado.transform.position = posicionInstancia;
                }
            }
            
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
        
        GameObject salaInstanciadaFinal = Instantiate(salaFinal, grid.transform.position, Quaternion.identity, grid.transform);
        
        salaInstanciadaFinal.transform.localScale = localScale;
        
        salaAnterior = salas[salas.Count-1].transform.GetChild(0).GetChild(0).gameObject;
        salaInstanciadaFinal.transform.position = new Vector3(salaAnterior.transform.position.x,
            salaAnterior.transform.position.y, salaAnterior.transform.position.z);
        
        salas.Add(salaInstanciadaFinal);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void anadirObjetosCofre(GameObject cofre)
    {
        PosicionInventarioCofre[] posicionInventarioCofres = cofre.GetComponent<CofreController>().posicionInventarioCofres;

        InventarioCofreController inventarioCofreController =
            GameObject.Find("InventarioCofre").GetComponent<InventarioCofreController>();
        
        posicionInventarioCofres = new PosicionInventarioCofre[inventarioCofreController.posiciones.Length];

        for (int i = 0; i < posicionInventarioCofres.Length; i++)
        {
            posicionInventarioCofres[i] = new PosicionInventarioCofre();
        }
        
        Random random = new Random();
        int numObjetosCofre = random.Next(1, 5);

        int numAnteriorObjetoCofre = -1;
        
        for (int i = 0; i < numObjetosCofre; i++)
        {
            random = new Random();
            int numObjetoCofre = -1;
            while (true)
            {
                numObjetoCofre = random.Next(0, objectosCofre.Length);

                if (numObjetoCofre != numAnteriorObjetoCofre)
                {
                    numAnteriorObjetoCofre = numObjetoCofre;
                    break;
                }
            }

            Sprite sprite = GameObject.Find("ToolBar").GetComponent<InventarioController>()
                .infoObjetos[objectosCofre[numObjetoCofre].id].sprite;

            posicionInventarioCofres[i].item = objectosCofre[numObjetoCofre].id;
            posicionInventarioCofres[i].cantidad = random.Next(objectosCofre[numObjetoCofre].cantMin,
                objectosCofre[numObjetoCofre].cantMax);
            posicionInventarioCofres[i].sprite = sprite;
        }
    }
}
