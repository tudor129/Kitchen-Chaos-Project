using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour
{
    const string IS_FlASHING = "IsFlashing";
    
    [SerializeField] StoveCounter _stoveCounter;

    Animator _animator;


    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        _stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        _animator.SetBool(IS_FlASHING, false);
    }
    void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool show = _stoveCounter.IsFried() && e._progressNormalized >= burnShowProgressAmount;

        _animator.SetBool(IS_FlASHING, show);
    }

}
