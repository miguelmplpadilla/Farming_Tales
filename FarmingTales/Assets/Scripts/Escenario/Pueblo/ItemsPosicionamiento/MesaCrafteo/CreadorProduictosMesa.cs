using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreadorProduictosMesa : MonoBehaviour
{
    [System.Serializable]
    public class ObjProducto
    {
        public string id;
        public string nombreProducto;
        public Sprite imagen;
        public int precio;
        public List<Materiales> materialesNecesarios = new List<Materiales>();
    }
    
    public class Materiales
    {
        public Sprite imagen;
        public int cantidad;
    }
    
    public List<ObjProducto> listaProductos = new List<ObjProducto>();
    
    
}
