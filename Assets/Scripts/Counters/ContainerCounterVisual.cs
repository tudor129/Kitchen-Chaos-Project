using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    const string OPEN_CLOSE = "OpenClose";
    
    [SerializeField] ContainerCounter _containerCounter;
        
    Animator _animator;

    void Awake()
    { 
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _containerCounter.OnPlayerGrabObject += ContainerCounter_OnPlayerGrabObject;
    }
    void ContainerCounter_OnPlayerGrabObject(object sender, EventArgs e)
    {
        _animator.SetTrigger(OPEN_CLOSE);
    }


}
