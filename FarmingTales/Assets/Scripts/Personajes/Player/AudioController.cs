using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public List<AudioClip> audios;
    
    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void playAudio(int num)
    {
        audio.clip = audios[num];
        audio.Play();
    }
}
