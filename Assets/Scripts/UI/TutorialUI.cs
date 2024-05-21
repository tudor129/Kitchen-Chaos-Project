using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
        [SerializeField] TextMeshProUGUI _keyMoveUpText;
        [SerializeField] TextMeshProUGUI _keyMoveDownText;
        [SerializeField] TextMeshProUGUI _keyMoveLeftText;
        [SerializeField] TextMeshProUGUI _keyMoveRightText;
        [SerializeField] TextMeshProUGUI _keyMoveInteractText;
        [SerializeField] TextMeshProUGUI _keyMoveInteractAltText;
        [SerializeField] TextMeshProUGUI _keyMovePauseText;
        [SerializeField] TextMeshProUGUI _keyMoveGamepadInteractText;
        [SerializeField] TextMeshProUGUI _keyMoveGamepadInteractAltText;
        [SerializeField] TextMeshProUGUI _keyMoveGamepadPauseText;

        void Start()
        {
                GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
                KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
                UpdateVisual();
                Show();
        }
        void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
        {
                if (KitchenGameManager.Instance.IsCountdownToStartActive())
                {
                        Hide();
                }
        }
        void GameInput_OnBindingRebind(object sender, EventArgs e)
        {
                UpdateVisual();
        }

        void UpdateVisual()
        {
                _keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
                _keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
                _keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
                _keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
                _keyMoveInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
                _keyMoveInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
                _keyMovePauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
                _keyMoveGamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
                _keyMoveGamepadInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
                _keyMoveGamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
        }

        void Show()
        {
                gameObject.SetActive(true);
        }
        void Hide()
        {
                gameObject.SetActive(false);
        }
        
}
