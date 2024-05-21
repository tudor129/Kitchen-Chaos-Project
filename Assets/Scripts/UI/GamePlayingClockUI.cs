using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
   [SerializeField] Image _timerImage;

   void Update()
   {
      _timerImage.fillAmount = KitchenGameManager.Instance.GetGamePlayingTimerNormalized();
   }
}
