using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class ToolBarController : MonoBehaviour
{
    public Sprite[] spritesToolBar;
    public Sprite[] spritesArmas;

    private Image imageToolBar;
    private SpriteRenderer armaPlayer;
    public float position = 0;
    private GameObject player;
    private AttackController attackController;
    private InventarioController inventarioController;

    public int posicionArrayInventario;

    private GameObject posicionadorItem;

    public PosicionController posicionController;

    public string posicionActual = "";

    private void Awake()
    {
        imageToolBar = GetComponent<Image>();
        inventarioController = GetComponent<InventarioController>();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("player");
        attackController = player.GetComponent<AttackController>();
        armaPlayer = player.transform.GetChild(0).GetComponent<SpriteRenderer>();
        
        posicionadorItem = GameObject.Find("PosicionadorItem");
        
    }

    void Update()
    {
        if (player.GetComponent<PlayerController>().mov)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && position == 1)
            {
                position = 0;
            }
            else
            {
                position = position - (Input.GetAxis("Mouse ScrollWheel") * 10);
            }

            if (position == 6f)
            {
                position = 0;
            }

            if (position == -1f)
            {
                position = 5;
            }

            if (position == 0)
            {
                attackController.golpe = 2;
            } else if (position == 1)
            {
                attackController.golpe = 3;
            }
            else
            {
                attackController.golpe = 1;
            }
        }

        if (position != 0 && position != 1)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0 && position == 2)
                {
                    posicionArrayInventario = 0;
                }
                else
                {
                    posicionArrayInventario = (int)position - 2;
                }

                if (posicionArrayInventario == 4)
                {
                    posicionArrayInventario = 0;
                }
            
                if (posicionArrayInventario == -1)
                {
                    posicionArrayInventario = 3;
                }

                posicionController = inventarioController.posiciones[posicionArrayInventario]
                    .GetComponent<PosicionController>();

                //Debug.Log(posicionController.item);
            }
            
            posicionActual = posicionController.item;

            if (posicionadorItem != null)
            {
                if (posicionController.item == "cofre" || posicionController.item == "plantacion" || posicionController.item == "valla" || posicionController.item == "mesaCrafteo")
                {
                    posicionadorItem.GetComponent<PosicionadorItemController>().itemPosicionado = Resources.Load("Prefabs/Instancias/"+posicionController.item) as GameObject;
                    posicionadorItem.GetComponent<PosicionadorItemController>().posicionController = posicionController;
                }
                else
                {
                    posicionadorItem.GetComponent<PosicionadorItemController>().itemPosicionado = null;
                    posicionadorItem.GetComponent<PosicionadorItemController>().posicionController = null;
                }

                if (inventarioController.infoObjetos[posicionController.item].tipoItem == "comida")
                {
                    player.GetComponent<AttackController>().comer = true;
                }
                else
                {
                    player.GetComponent<AttackController>().comer = false;
                }
            }
        }
        else
        {
            if (posicionadorItem != null)
            {
                posicionadorItem.GetComponent<PosicionadorItemController>().itemPosicionado = null;
                posicionadorItem.GetComponent<PosicionadorItemController>().posicionController = null;
            }

            posicionActual = "";
        }
        }
    }

    private void LateUpdate()
    {
        imageToolBar.sprite = spritesToolBar[(int)position];
        
        if (position == 0 || position == 1)
        {
            armaPlayer.sprite = spritesArmas[(int)position];
        }
        else
        {
            armaPlayer.sprite = null;
        }
    }
}
