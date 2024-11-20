using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SoundEffectsControl : MonoBehaviour
{
    [SerializeField] private AudioSource _srcSE; // Sound Effects Audio Source Object
    [SerializeField] private AudioClip punch1Sfx;
    [SerializeField] private AudioClip punch2Sfx;
    [SerializeField] private AudioClip buttonClickSfx;
    [FormerlySerializedAs("redLightSfx")] [SerializeField] private AudioClip TagTransferSfx;
    [SerializeField] private AudioClip roundStartSfx;
    [SerializeField] private AudioClip roundEndSfx;
    [SerializeField] private AudioClip timerFastClockSfx;

    public void PlayPunch()
    {
        if (_srcSE == null) return;
        _srcSE.clip = Random.Range(0,3) < 2? punch1Sfx : punch2Sfx;
        _srcSE.Play();
    }

    public void PlayButtonClick()
    {
        if (_srcSE == null) return;
        _srcSE.clip = buttonClickSfx;
        _srcSE.Play();
    }

    public void PlayTagTransfer()
    {
        if (_srcSE == null) return;
        _srcSE.clip = TagTransferSfx;
        _srcSE.Play();
    }
    
    public void PlayRoundStart()
    {
        if (_srcSE == null) return;
        _srcSE.clip = roundStartSfx;
        _srcSE.Play();
    }
    
    public void PlayRoundEnd()
    {
        if (_srcSE == null) return;
        _srcSE.clip = roundEndSfx;
        _srcSE.Play();
    }
    
    public void PlayTimerFastClock()
    {
        if (_srcSE == null) return;
        _srcSE.clip = timerFastClockSfx;
        _srcSE.Play();
    }
}
