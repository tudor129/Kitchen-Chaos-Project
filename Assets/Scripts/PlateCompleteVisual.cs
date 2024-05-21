using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [System.Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO _kitchenObjectSO;
        public GameObject _gameObject;
    }
    
    [SerializeField] PlateKitchenObject _plateKitchenObject;
    [SerializeField] List<KitchenObjectSO_GameObject> _kitchenObjectSoGameObjectList;

    void Start()
    {
        _plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        
        foreach (KitchenObjectSO_GameObject kitchenObjectSoGameObject in _kitchenObjectSoGameObjectList)
        {
                kitchenObjectSoGameObject._gameObject.SetActive(false);
        }
    }
    
    void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSoGameObject in _kitchenObjectSoGameObjectList)
        {
            if (kitchenObjectSoGameObject._kitchenObjectSO == e._KitchenObjectSO)
            {
                kitchenObjectSoGameObject._gameObject.SetActive(true);
            }
        }
    }
}
