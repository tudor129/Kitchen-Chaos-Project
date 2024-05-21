using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    const string POPUP = "Popup";
    
    [SerializeField] Image _backgroundImage;
    [SerializeField] Image _iconImage;
    [SerializeField] TextMeshProUGUI _messageText;
    [SerializeField] Color _successColor;
    [SerializeField] Color _failedColor;
    [SerializeField] Sprite _successSprite;
    [SerializeField] Sprite _failedSprite;

    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        gameObject.SetActive(false);
    }
    void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(POPUP);
        _backgroundImage.color = _successColor;
        _iconImage.sprite = _successSprite;
        _messageText.text = "DELIVERY\nSUCCESS";
    }
    void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(POPUP);
        _backgroundImage.color = _failedColor;
        _iconImage.sprite = _failedSprite;
        _messageText.text = "DELIVERY\nFAILED";
    }

}
