using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreadorProductosController : MonoBehaviour
{
    
    [System.Serializable]
    public class ObjProducto
    {
        public string id;
        public string nombreProducto;
        public Sprite imagen;
        public int precio;
    }
    
    public List<ObjProducto> listaProductos = new List<ObjProducto>();

    public IDictionary<string, ObjProducto> productosDisponibles = new Dictionary<string, ObjProducto>();

    public GameObject producto;
    public GameObject continer;

    public List<GameObject> productos = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < listaProductos.Count; i++)
        {
            productosDisponibles.Add(listaProductos[i].id, listaProductos[i]);
        }
    }

    void Start()
    {
        List<string> pro = new List<string>();
        pro.Add("patata");
        pro.Add("zanahoria");
        pro.Add("semillaTrigo");
        anadirProductos(pro);
    }

    public void anadirProductos(List<string> productosAnadir)
    {
        for (int i = 0; i < productosAnadir.Count; i++)
        {
            GameObject instanciado = Instantiate(producto, continer.transform.position, Quaternion.identity, continer.transform);
            instanciado.GetComponent<ProductoController>().anadirDatos(productosDisponibles[productosAnadir[i]].nombreProducto, productosDisponibles[productosAnadir[i]].imagen, productosDisponibles[productosAnadir[i]].precio);
            productos.Add(instanciado);
        }
    }

    public void eliminarProductos()
    {
        for (int i = 0; i < productos.Count; i++)
        {
            Destroy(productos[i]);
        }
        productos = new List<GameObject>();
    }
}
