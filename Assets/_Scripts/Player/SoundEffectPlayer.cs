using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundEffectPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _srcSE; // Sound Effects Audio Source Object
    [SerializeField] private AudioClip punch1Sfx;
    [SerializeField] private AudioClip punch2Sfx;
    [SerializeField] private AudioClip hitMarkerSfx;
    
    private float _pitch = 1f;


    private void Awake()
    {
        _srcSE = GetComponent<AudioSource>();
    }

    public void PlayPunch()
    {
        if (_srcSE == null) return;
        _srcSE.clip = Random.Range(0,3) < 2? punch1Sfx : punch2Sfx;
        _srcSE.Play();
    }
    
    public void PlayHitMarker()
    {
        if (_srcSE == null) return;
        _srcSE.clip = hitMarkerSfx;
        // _srcSE.pitch = Random.Range(1f, 1.2f);
        _srcSE.Play();
    }
}
