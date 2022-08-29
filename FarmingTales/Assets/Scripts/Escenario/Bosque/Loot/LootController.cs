using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    public string tipo = "madera";
    public int cant = 5;
    public Sprite spr;
    public int numAudio;
    public IDictionary<string, ObjetoInventario> sprites;

    private AudioController audioController;

    public int numParticula;

    public List<string> tiposLoot = new List<string>();
    public List<info> infos = new List<info>();

    [System.Serializable]
    public class info
    {
        public Vector2 size;
        public int cant;
        public int life;
        public int numParticula;
        public int numAudio;
    }

    public IDictionary<string, info> infoLoot = new Dictionary<string, info>();

    private ParticulasController particulasController;

    public int life = 3;

    private InventarioController inventarioController;

    private void Awake()
    {
        for (int i = 0; i < tiposLoot.Count; i++)
        {
            infoLoot.Add(tiposLoot[i], infos[i]);
        }

        audioController = GetComponent<AudioController>();
    }

    private void Start()
    {
        inventarioController = GameObject.Find("ToolBar").GetComponent<InventarioController>();

        sprites = inventarioController.infoObjetos;

        spr = sprites[tipo].sprite;

        particulasController = transform.GetChild(1).GetComponent<ParticulasController>();
        
        gameObject.GetComponent<BoxCollider2D>().size = infoLoot[tipo].size;
        cant = infoLoot[tipo].cant;
        life = infoLoot[tipo].life;
        numParticula = infoLoot[tipo].numParticula;
        numAudio = infoLoot[tipo].numAudio;
    }

    public void setDatos(string tipe, Sprite sprite)
    {
        tipo = tipe;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("loot"))
        {
            if (other.transform.position.x > transform.position.x)
            {
                transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
            }
        }

        if (other.CompareTag("limiteGeneracion"))
        {
            Destroy(gameObject);
        }
    }

    public void temblar()
    {
        StartCoroutine("tremble");
    }
    
    IEnumerator tremble() {
        particulasController.startParticulas(numParticula);
        audioController.playAudio(numAudio);
        
        for ( int i = 0; i < 5; i++)
        {
            transform.localPosition += new Vector3(0.05f, 0, 0);
            yield return new WaitForSeconds(0.01f);
            transform.localPosition -= new Vector3(0.05f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
