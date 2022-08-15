using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapaController : MonoBehaviour
{

    public Animator fundidoAnimator;

    private bool mov = true;
    
    void Start()
    {
        
    }

    public void moverNivel(string nivel)
    {
        if (mov)
        {
            mov = false;
            fundidoAnimator.gameObject.SetActive(true);
        
            StartCoroutine("rutinaMoverNivel", nivel);
        }
    }

    IEnumerator rutinaMoverNivel(string nivel)
    {
        fundidoAnimator.SetTrigger("fundido");

        yield return new WaitForSeconds(3f);
        
        SceneManager.LoadScene(nivel);
    }
    
}
