using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{

       [SerializeField] Button _resumeButton;
       [SerializeField] Button _mainMenuButton;
       [SerializeField] Button _optionsButton;

       void Awake()
       {
              _resumeButton.onClick.AddListener(() =>
              {
                     KitchenGameManager.Instance.TogglePauseGame();
              });
              
              _mainMenuButton.onClick.AddListener(() =>
              {
                     Loader.Load(Loader.Scene.MainMenuScene);
              });
              
              _optionsButton.onClick.AddListener(() =>
              {
                     Hide();
                     OptionsUI.Instance.Show(Show);
              });
       }

       void Start()
       {
              KitchenGameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
              KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
              
              Hide();
       }
       void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)
       {
              Hide();
       }
       void KitchenGameManager_OnGamePaused(object sender, EventArgs e)
       {
              Show();
       }

       void Show()
       {
              gameObject.SetActive(true);
              
              _resumeButton.Select();
       }
       void Hide()
       {
              gameObject.SetActive(false);
       }
}
