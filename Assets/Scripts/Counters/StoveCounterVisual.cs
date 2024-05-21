using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
   [SerializeField] StoveCounter _stoveCounter;
   [SerializeField] GameObject _stoveOnGameObject;
   [SerializeField] GameObject _particlesGameObject;

   void Start()
   {
      _stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
   }
   void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
   {
      bool showVisual = e._state == StoveCounter.State.Frying || e._state == StoveCounter.State.Fried;
      _stoveOnGameObject.SetActive(showVisual);
      _particlesGameObject.SetActive(showVisual);
   }
}
