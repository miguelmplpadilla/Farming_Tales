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

    private List<string> productosComprar = new List<string>();
    private List<string> productosVender = new List<string>();
    private List<int> productosVenderCantidad = new List<int>();

    public IDictionary<string, ObjProducto> productosDisponibles = new Dictionary<string, ObjProducto>();

    public GameObject producto;
    public GameObject continer;

    public List<GameObject> productos = new List<GameObject>();

    private InventarioController inventarioController;

    private void Awake()
    {
        for (int i = 0; i < listaProductos.Count; i++)
        {
            productosDisponibles.Add(listaProductos[i].id, listaProductos[i]);
        }
    }

    void Start()
    {
        inventarioController = GameObject.Find("ToolBar").GetComponent<InventarioController>();
        
        List<string> pro = new List<string>();
        pro.Add("patata");
        pro.Add("zanahoria");
        pro.Add("semillaTrigo");
        pro.Add("valla");
        pro.Add("madera");
        pro.Add("roca");
        anadirProductosComprar(pro);
    }

    public void anadirProductosComprar(List<string> productosAnadir)
    {
        for (int i = 0; i < productosAnadir.Count; i++)
        {
            Debug.Log("Producto añadido: "+i);
            productosComprar.Add(productosAnadir[i]);
            GameObject instanciado = Instantiate(producto, continer.transform.position, Quaternion.identity, continer.transform);
            instanciado.GetComponent<ProductoController>().anadirDatos(productosDisponibles[productosAnadir[i]].id, productosDisponibles[productosAnadir[i]].nombreProducto, productosDisponibles[productosAnadir[i]].imagen, productosDisponibles[productosAnadir[i]].precio, 1);
            productos.Add(instanciado);
        }
    }
    
    public void anadirProductosVender(List<string> productosAnadir, List<int> cantidadVender)
    {
        for (int i = 0; i < productosAnadir.Count; i++)
        {
            GameObject instanciado = Instantiate(producto, continer.transform.position, Quaternion.identity, continer.transform);
            int precioVenta = (int) Math.Round((productosDisponibles[productosAnadir[i]].precio * 0.5f));
            instanciado.GetComponent<ProductoController>().anadirDatos(productosDisponibles[productosAnadir[i]].id, productosDisponibles[productosAnadir[i]].nombreProducto, productosDisponibles[productosAnadir[i]].imagen, precioVenta, cantidadVender[i], 2);
            productos.Add(instanciado);
        }
    }

    public void listaComprar()
    {
        eliminarProductos();
        List<string> pro = new List<string>();
        pro.Add("patata");
        pro.Add("zanahoria");
        pro.Add("semillaTrigo");
        pro.Add("cofre");
        pro.Add("valla");
        pro.Add("madera");
        pro.Add("roca");
        anadirProductosComprar(pro);
        
    }

    public void listaVender()
    {
        eliminarProductos();
        productosVender = new List<string>();
        productosVenderCantidad = new List<int>();
        disponibilidadVenta();
        anadirProductosVender(productosVender, productosVenderCantidad);
    }

    public void disponibilidadVenta()
    {
        for (int i = 0; i < inventarioController.posiciones.Length; i++)
        {
            bool existeArray = false;
            
            for (int j = 0; j < productosVender.Count; j++)
            {
                if (inventarioController.posiciones[i].GetComponent<PosicionController>().item == productosVender[j])
                {
                    existeArray = true;
                    productosVenderCantidad[j] = productosVenderCantidad[j] + inventarioController
                        .posiciones[i].GetComponent<PosicionController>().cantidad;
                }
            }

            if (!existeArray)
            {
                if (inventarioController.posiciones[i].GetComponent<PosicionController>().item != "")
                {
                    productosVender.Add(inventarioController.posiciones[i].GetComponent<PosicionController>().item);
                    productosVenderCantidad.Add(inventarioController.posiciones[i].GetComponent<PosicionController>()
                        .cantidad);
                }
            }
        }
    }

    public void eliminarProductos()
    {
        Debug.Log("He entrado a eliminar producto");
        for (int i = 0; i < productos.Count; i++)
        {
            Debug.Log("Producto eliminado: "+i);
            Destroy(productos[i]);
        }
        productos = new List<GameObject>();
    }
}
