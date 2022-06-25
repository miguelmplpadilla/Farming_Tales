using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranjaPosicionController : MonoBehaviour
{
    public string idGranja = "";
    public string id = "";

    public void setId(string ide)
    {
        id = ide;
        cargarId();
    }

    public void guardarId()
    {
        PlayerPrefs.SetString(id, idGranja);
        PlayerPrefs.Save();
    }

    public void cargarId()
    {
        if (PlayerPrefs.HasKey(id))
        {
            idGranja = PlayerPrefs.GetString(id);
        }
    }
}
