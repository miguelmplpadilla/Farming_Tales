using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatonController : MonoBehaviour
{
    private Image image;

    public Sprite click;
    public Sprite noClick;
    
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, 1, 1);
    }
    
    void Update()
    {
        transform.position = new Vector3(Input.mousePosition.x+6, Input.mousePosition.y-15, Input.mousePosition.z);
        
        image.sprite = noClick;

        if (Input.GetButton("Fire1"))
        {
            image.sprite = click;
        }
    }
}
