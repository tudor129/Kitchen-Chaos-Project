using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] BaseCounter _counter;
    [SerializeField] GameObject[] _visualGameObjArray;
    
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }
    void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e._selectedCounter == _counter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    void Show()
    {
        foreach (GameObject visualGameObject in _visualGameObjArray)
        {
            visualGameObject.SetActive(true);
        }
        
    }
    void Hide()
    {
        foreach (GameObject visualGameObject in _visualGameObjArray)
        {
            visualGameObject.SetActive(false);
        }
    }
}
