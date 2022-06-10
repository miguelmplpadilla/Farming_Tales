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
        
    }

    void Update()
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
                if (Input.GetAxis("Mouse ScrollWheel") > 0 && position == 1)
                {
                    posicionArrayInventario = 0;
                }
                else
                {
                    posicionArrayInventario = posicionArrayInventario - (int)(Input.GetAxis("Mouse ScrollWheel") * 10);
                }

                if (posicionArrayInventario == 4)
                {
                    posicionArrayInventario = 0;
                }
            
                if (posicionArrayInventario == -1)
                {
                    posicionArrayInventario = 5;
                }

                Debug.Log(inventarioController.posiciones[posicionArrayInventario].GetComponent<PosicionController>().item);
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
