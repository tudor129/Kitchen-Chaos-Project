using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    
    [SerializeField] KitchenObjectSO _plateKitchenObjectSO;
    
    float _spawnPlateTimer;
    float _spawnPlateTimerMax = 4f;
    int _platesSpawnedAmount;
    int _platesSpawnedAmountMax = 4;

    void Update()
    {
        _spawnPlateTimer += Time.deltaTime;

        if (_spawnPlateTimer > _spawnPlateTimerMax)
        {
            _spawnPlateTimer = 0f;

            if (KitchenGameManager.Instance.IsGamePlaying() && _platesSpawnedAmount < _platesSpawnedAmountMax)
            {
                _platesSpawnedAmount++;
                
                OnPlateSpawned?.Invoke(this,EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (_platesSpawnedAmount > 0)
            {
                // there's at least one plate here
                _platesSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(_plateKitchenObjectSO, player);
                
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
