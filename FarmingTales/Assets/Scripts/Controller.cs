using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{

    public Animator fundidoNegro;
    
    void Start()
    {
        fundidoNegro.gameObject.SetActive(true);
        fundidoNegro.SetTrigger("desfundido");

        StartCoroutine("desactivarFundidoNegro");
    }

    IEnumerator desactivarFundidoNegro()
    {
        yield return new WaitForSeconds(3f);
        
        fundidoNegro.gameObject.SetActive(false);
    }
}
