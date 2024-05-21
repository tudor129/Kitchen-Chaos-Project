using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] GameObject _hasProgressGameObject;
    [SerializeField] Image _barImage;
    
    IHasProgress _hasProgress;

    void Start()
    {
        _hasProgress = _hasProgressGameObject.GetComponent<IHasProgress>();
        if (_hasProgress == null)
        {
            // this is because Unity does not support dragging references in the editor if they are interface references
            Debug.LogError("Game object " + _hasProgressGameObject + "does not have a component that implements IHasProgress!");
        }
        
        _hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        _barImage.fillAmount = 0f;
        
        gameObject.SetActive(false);
    }
   
    void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        _barImage.fillAmount = e._progressNormalized;

        if (e._progressNormalized == 0f || e._progressNormalized == 1f)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

    }
}
