using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    const string CUT = "Cut";
    
    [SerializeField] CuttingCounter _cuttingCounter;
        
    Animator _animator;

    void Awake()
    { 
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _cuttingCounter.OnCut += ContainerCounter_OnPlayerGrabObject;
    }
    void ContainerCounter_OnPlayerGrabObject(object sender, EventArgs e)
    {
        _animator.SetTrigger(CUT);
    }


}
