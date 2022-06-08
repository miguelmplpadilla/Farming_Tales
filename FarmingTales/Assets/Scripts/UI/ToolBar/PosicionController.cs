using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionController : MonoBehaviour
{
    public string item = "";
    public int cantidad = 0;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (cantidad > 0)
        {
            rectTransform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            rectTransform.localScale = new Vector3(0, 1, 1);
        }
    }
}
