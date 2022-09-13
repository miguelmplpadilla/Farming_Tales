using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverCameraConfiner : MonoBehaviour
{
    private GameObject cameraConfiner;
    private GameObject bloqueadorPuerta;

    private void Start()
    {
        cameraConfiner = GameObject.Find("CameraConfiner");
        bloqueadorPuerta = transform.parent.Find("BloqueadorPuerta").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        /*if (col.CompareTag("player"))
        {
            cameraConfiner.transform.position = transform.parent.Find("MoverCam").position;
            bloqueadorPuerta.GetComponent<BoxCollider2D>().enabled = true;
        }*/
    }
}
