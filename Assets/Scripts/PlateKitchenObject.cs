using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
        public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

        public class OnIngredientAddedEventArgs : EventArgs
        {
                public KitchenObjectSO _KitchenObjectSO;
        }
        
        [SerializeField] List<KitchenObjectSO> _validKitchenObjectSOList;
        
        List<KitchenObjectSO> _kitchenObjectSOList;

        void Awake()
        {
                _kitchenObjectSOList = new List<KitchenObjectSO>();
        }

        public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
        {
                if (!_validKitchenObjectSOList.Contains(kitchenObjectSO))
                {
                        // Not a valid ingredient
                        return false;
                }
                
                
                if (_kitchenObjectSOList.Contains(kitchenObjectSO))
                {
                        // already has this type
                        return false;
                }
                else
                {
                        _kitchenObjectSOList.Add(kitchenObjectSO);
                        
                        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs()
                        {
                                _KitchenObjectSO = kitchenObjectSO
                        });
                        
                        return true;    
                }
        }

        public List<KitchenObjectSO> GetKitchenObjectSOList()
        {
                return _kitchenObjectSOList;
        }
}
