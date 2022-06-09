using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePlayerController : MonoBehaviour
{

    public int life = 8;
    private RectTransform rectTransformBarraVida;

    void Start()
    {
        rectTransformBarraVida = GameObject.Find("BarraVidaPlayer").GetComponent<RectTransform>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            life--;
        }
    }

    private void LateUpdate()
    {
        float tamanoBarraVida = (float)(14.34568 * life);
        rectTransformBarraVida.sizeDelta = new Vector2(tamanoBarraVida, 12.1327f);
        rectTransformBarraVida.gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(tamanoBarraVida, 4.0773f);
    }
}
