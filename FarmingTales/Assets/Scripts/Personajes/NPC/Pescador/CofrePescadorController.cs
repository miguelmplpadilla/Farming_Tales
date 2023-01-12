using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CofrePescadorController : MonoBehaviour
{
    private CofreController cofreController;
    private InventarioCofreController inventarioCofreController;
    public Sprite spriteBacalao;
    public Sprite spriteSalmon;
    void Start()
    {
        cofreController = GameObject.Find("CofrePesca").GetComponent<CofreController>();
        inventarioCofreController = GameObject.Find("InventarioCofre").GetComponent<InventarioCofreController>();
        
        int numBacalao = PlayerPrefs.GetInt("NumBacalao");
        int numSalmon = PlayerPrefs.GetInt("NumSalmon");

        Debug.Log("Numero Bacalao: "+numBacalao);
        Debug.Log("Numero Salmon: "+numSalmon);
        
        cofreController.cargarInventario();
        
        cofreController.anadirProductosInterfazInventario();

        inventarioCofreController.anadirInventario("bacalao",spriteBacalao, numBacalao, null);
        inventarioCofreController.anadirInventario("salmon",spriteSalmon, numSalmon, null);
        
        cofreController.guardarInventarioArray();
        cofreController.guardarInventario();

        //cofreController.anadirObjetoInicioCofre("bacalao", numBacalao, spriteBacalao);
        //cofreController.anadirObjetoInicioCofre("salmon", numSalmon, spriteSalmon);
        //cofreController.anadirObjetosCofre();
        
        PlayerPrefs.SetInt("NumBacalao", 0);
        PlayerPrefs.SetInt("NumSalmon", 0);
    }
}
