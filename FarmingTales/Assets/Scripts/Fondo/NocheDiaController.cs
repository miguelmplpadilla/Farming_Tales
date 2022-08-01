using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NocheDiaController : MonoBehaviour
{
    
    public int tiempoDiaNoche = 300;
    public int hora = 0;

    public GameObject[] fondosDia;
    public GameObject[] fondosNoche;

    public int estado = 1;

    void Start()
    {
        StartCoroutine("nocheDia");
    }

    IEnumerator nocheDia()
    {
        for (int i = hora; i < tiempoDiaNoche; i++)
        {
            hora++;
            yield return new WaitForSeconds(1f);
        }
        
        hora = 0;

        StartCoroutine("cambiarFondo");
        
        yield return null;
    }

    IEnumerator cambiarFondo()
    {
        if (estado == 1)
        {
            for (float i = 1000; i > 0; i--)
            {
                Color colorTemp = Color.white;
                colorTemp.a = i / 1000;
                fondosDia[0].GetComponent<SpriteRenderer>().color = colorTemp;
                fondosDia[1].GetComponent<SpriteRenderer>().color = colorTemp;
                fondosDia[2].GetComponent<SpriteRenderer>().color = colorTemp;

                yield return null;
            }
            estado = 2;
        }
        else
        {
            for (float i = 0; i < 1000; i++)
            {
                Color colorTemp = Color.white;
                colorTemp.a = i / 1000;
                fondosDia[0].GetComponent<SpriteRenderer>().color = colorTemp;
                fondosDia[1].GetComponent<SpriteRenderer>().color = colorTemp;
                fondosDia[2].GetComponent<SpriteRenderer>().color = colorTemp;

                yield return null;
            }
            estado = 1;
        }

        StartCoroutine("nocheDia");
        
        yield return null;
    }

    public void dia()
    {
        StopCoroutine("nocheDia");
        StopCoroutine("cambiarFondo");
        Color colorTemp = Color.white;
        colorTemp.a = 1;
        fondosDia[0].GetComponent<SpriteRenderer>().color = colorTemp;
        fondosDia[1].GetComponent<SpriteRenderer>().color = colorTemp;
        fondosDia[2].GetComponent<SpriteRenderer>().color = colorTemp;
        estado = 1;
        hora = 0;
        StartCoroutine("nocheDia");
    }

    public void noche()
    {
        StopCoroutine("nocheDia");
        StopCoroutine("cambiarFondo");
        Color colorTemp = Color.white;
        colorTemp.a = 0;
        fondosDia[0].GetComponent<SpriteRenderer>().color = colorTemp;
        fondosDia[1].GetComponent<SpriteRenderer>().color = colorTemp;
        fondosDia[2].GetComponent<SpriteRenderer>().color = colorTemp;
        estado = 2;
        hora = 0;
        StartCoroutine("nocheDia");
    }
}
