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

    private void Awake()
    {
        imageToolBar = GetComponent<Image>();
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
