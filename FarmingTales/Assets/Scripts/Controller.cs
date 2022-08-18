using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{

    public Animator fundidoNegro;
    private GameObject player;
    
    void Start()
    {
        player = GameObject.Find("Player");
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
