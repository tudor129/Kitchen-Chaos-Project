using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] StoveCounter _stoveCounter;
    
    AudioSource _audioSource;

    float _warningSoundTimer;
    bool _playWarningSound;
    
    
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        _stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        _stoveCounter.OnProgressChanged += StoveCounterOnOnProgressChanged;
    }
    void StoveCounterOnOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool playWarningSound = _stoveCounter.IsFried() && e._progressNormalized >= burnShowProgressAmount;
        _playWarningSound = playWarningSound;
    }
    void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e._state == StoveCounter.State.Frying || e._state == StoveCounter.State.Fried;
        if (playSound)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Pause();
        }
    }

    void Update()
    {
        if (_playWarningSound)
        {
            _warningSoundTimer -= Time.deltaTime;
            if (_warningSoundTimer <= 0f)
            {
                float warningSoundTimerMax = .2f;
                _warningSoundTimer = warningSoundTimerMax;
                
                SoundManager.Instance.PlayWarningSound(_stoveCounter.transform.position);
            }
            
            
        }
        
    }

}
