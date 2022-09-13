using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifePlayerController : MonoBehaviour
{

    public int vidaCompleta = 8;
    public int life = 8;
    private RectTransform rectTransformBarraVida;
    private PlayerController playerController;
    private Animator animator;

    public bool hit = false;
    public bool muriendo = false;

    void Start()
    {
        rectTransformBarraVida = GameObject.Find("BarraVidaPlayer").GetComponent<RectTransform>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();

        life = PlayerPrefs.GetInt("VidaPlayer");
    }

    
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.K))
        {
            restarVida(1);
        }*/
    }

    private void LateUpdate()
    {
        float tamanoBarraVida = (float)(14.34568 * life);
        rectTransformBarraVida.sizeDelta = new Vector2(tamanoBarraVida, 12.1327f);
        rectTransformBarraVida.gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(tamanoBarraVida, 4.0773f);
    }

    public void restarVida(int restar)
    {
        if (life > 0 && !hit && !muriendo)
        {
            playerController.mov = false;
            life -= restar;
            if (life <= 0)
            {
                animator.SetBool("muriendo", true);
            }
            else
            {
                animator.SetTrigger("hit");
            }

            hit = true;
            
            PlayerPrefs.SetInt("VidaPlayer", life);
        }
    }

    public bool sumarVida(int sumar)
    {
        bool vidaRecuperada = false;
        if (life < vidaCompleta)
        {
            vidaRecuperada = true;
            life += sumar;
            if (life > vidaCompleta)
            {
                life = vidaCompleta;
            }
            
            PlayerPrefs.SetInt("VidaPlayer", life);
        }

        return vidaRecuperada;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("HitBoxEnemy"))
        {
            if (!hit)
            {
                restarVida(1);
            }
        }
    }

    public void stopHit()
    {
        playerController.mov = true;
        hit = false;
    }

    public void startMuerte()
    {
        muriendo = true;
    }

    public void reiniciarMuerte()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
