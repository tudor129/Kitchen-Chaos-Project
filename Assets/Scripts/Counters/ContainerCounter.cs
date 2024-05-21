using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabObject;
    
    [SerializeField] KitchenObjectSO _kitchenObjectSO;
  
    
 
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(_kitchenObjectSO, player);
            OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
        }
    }
    
    
}
