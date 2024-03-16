using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMangerScript : MonoBehaviour
{
    [SerializeField] MusicCutScript musiccut;
    private int timeround = 2; 

    [Header("=== Audio Source ===")]
    [SerializeField] AudioSource musicSource;

    [Header("=== Audio Clip ===")]
    public AudioClip background;

    private void Start()
    {
        musicSource.clip = background;
        
    }
    void Update()
    {
        if ( Math.Round(Time.time, timeround) == musiccut.TimeAppear[1]) //temporarily
        {
            musicSource.Play();
        }
    }
}
