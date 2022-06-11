using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PosicionRatonController : MonoBehaviour
{
    public string item = "";
    public int cantidad = 0;

    private RectTransform rectTransform;
    private RectTransform rectTransformRaton;

    private TextMeshProUGUI texto;

    public Color color;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransformRaton = GameObject.Find("Raton").GetComponent<RectTransform>();
        texto = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {

        rectTransform.position = new Vector3(rectTransformRaton.position.x-17.5f, rectTransformRaton.position.y+29.5f, rectTransform.position.z);
        
        if (cantidad > 0)
        {
            Color tempColor = Color.white;
            tempColor.a = 1;
            GetComponent<Image>().color = tempColor;
        }
        else
        {
            Color tempColor = color;
            GetComponent<Image>().color = tempColor;
        }
    }

    private void LateUpdate()
    {
        if (cantidad > 0)
        {
            texto.text = cantidad.ToString();
        }
        else
        {
            texto.text = "";
        }
    }
}
