using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrashed;
    
    new public static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }
    
    KitchenObject _kitchenObjectTrash;
    
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().AddComponent<Rigidbody>();
            
            player.GetKitchenObject().SetKitchenObjectParent(this);
            
            KitchenObject kitchenObject = GetKitchenObject();
            
            Debug.Log("Object name: " + kitchenObject.name + "has been thrown");
            
            Destroy(kitchenObject.gameObject, .5f);
            
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
           
        }
    }


    
}
