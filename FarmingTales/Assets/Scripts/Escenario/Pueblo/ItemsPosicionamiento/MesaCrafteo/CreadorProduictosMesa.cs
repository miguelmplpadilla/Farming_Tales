using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreadorProduictosMesa : MonoBehaviour
{

    public GameObject producto;
    public GameObject continer;
    private GameObject player;
    
    private InventarioController inventarioController;
    
    [System.Serializable]
    public class ObjProducto
    {
        public string nombreProducto;
        public Sprite imagen;
        public List<Materiales> materialesNecesarios = new List<Materiales>();
    }
    
    public List<ObjProducto> listaProductos = new List<ObjProducto>();

    private void Start()
    {
        player = GameObject.Find("Player");
        inventarioController = GameObject.Find("ToolBar").GetComponent<InventarioController>();
        crearProductos();
    }

    public void crearProductos()
    {
        for (int i = 0; i < listaProductos.Count; i++)
        {
            GameObject instanciado = Instantiate(producto, continer.transform.position, Quaternion.identity, continer.transform);
            instanciado.GetComponent<ProductoMesaController>().anadirDatos(listaProductos[i].nombreProducto, listaProductos[i].materialesNecesarios);
            instanciado.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = listaProductos[i].imagen;
            instanciado.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "";
            for (int j = 0; j < listaProductos[i].materialesNecesarios.Count; j++)
            {
                if (j == 0)
                {
                    instanciado.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text =
                        instanciado.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text +
                        listaProductos[i].materialesNecesarios[j].cantidad + "<sprite name=" +
                        listaProductos[i].materialesNecesarios[j].nombre+">";
                }
                else
                {
                    instanciado.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text =
                        instanciado.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text + " / " +
                        listaProductos[i].materialesNecesarios[j].cantidad + "<sprite name=" +
                        listaProductos[i].materialesNecesarios[j].nombre+">";
                }
            }
        }
    }

    public void cerrarMesaCrafteo()
    {
        inventarioController.mostrar = true;
        player.GetComponent<PlayerController>().mov = true;
        transform.parent.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
    }
    
}
