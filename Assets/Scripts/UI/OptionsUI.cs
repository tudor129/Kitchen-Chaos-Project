using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }
    
    [SerializeField] Button _soundEffectsButton;
    [SerializeField] Button _musicButton;
    [SerializeField] Button _closeButton;
    [SerializeField] Button _moveUpButton;
    [SerializeField] Button _moveDownButton;
    [SerializeField] Button _moveLeftButton;
    [SerializeField] Button _moveRightButton;
    [SerializeField] Button _interactButton;
    [SerializeField] Button _interactAlternateButton;
    [SerializeField] Button _pauseButton;
    [SerializeField] Button _gamepadInteractButton;
    [SerializeField] Button _gamepadInteractAlternateButton;
    [SerializeField] Button _gamepadPauseButton;
    [SerializeField] TextMeshProUGUI _soundEffectsText;
    [SerializeField] TextMeshProUGUI _musicText;
    [SerializeField] TextMeshProUGUI _moveUpText;
    [SerializeField] TextMeshProUGUI _moveDownText;
    [SerializeField] TextMeshProUGUI _moveLeftText;
    [SerializeField] TextMeshProUGUI _moveRightText;
    [SerializeField] TextMeshProUGUI _interactText;
    [SerializeField] TextMeshProUGUI _interactAlternateText;
    [SerializeField] TextMeshProUGUI _pauseText;
    [SerializeField] TextMeshProUGUI _gamepadInteractText;
    [SerializeField] TextMeshProUGUI _gamepadInteractAlternateText;
    [SerializeField] TextMeshProUGUI _gamepadPauseText;
    [SerializeField] Transform _pressToRebindKeyTransform;


    Action _onCloseButtonAction;
    
    void Awake()
    {
        Instance = this;
        
        _soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        
        _musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        
        _closeButton.onClick.AddListener(() =>
        {
            Hide();
            _onCloseButtonAction();
        });
        
        _moveUpButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Up);
        });
        _moveDownButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Down);
        });
        _moveLeftButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Left);
        });
        _moveRightButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Right);
        });
        _interactButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Interact);
        });
        _interactAlternateButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.InteractAlternate);
        });
        _pauseButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Pause);
        });
        _gamepadInteractButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Gamepad_Interact);
        });
        _gamepadInteractAlternateButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Gamepad_InteractAlternate);
        });
        _gamepadPauseButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Gamepad_Pause);
        });
    }

    void Start()
    {
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
        
        UpdateVisual();
        Hide();
        HidePressToRebindKey();
    }
    void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    void UpdateVisual()
    {
        _soundEffectsText.text = "Sound Effects : " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        _musicText.text = "Music : " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        _moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        _moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        _moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        _moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        _interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        _interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        _pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        _gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        _gamepadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        _gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    public void Show(Action onCloseButtonAction)
    {
        this._onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);
        
        _soundEffectsButton.Select();
    }
    void Hide()
    {
        gameObject.SetActive(false);
    }

    void ShowPressToRebindKey()
    {
        _pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    void HidePressToRebindKey()
    {
        _pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }

}
