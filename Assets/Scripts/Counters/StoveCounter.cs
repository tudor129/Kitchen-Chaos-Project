using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State _state;
    }
    
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }
    
    [SerializeField] FryingRecipeSO[] _fryingRecipeSOArray;
    [SerializeField] BurningRecipeSO[] _burningRecipeSOArray;

    State _state;
    float _fryingTimer;
    float _burningTimer;
    FryingRecipeSO _fryingRecipeSO;
    BurningRecipeSO _burningRecipeSO;


    void Start()
    {
        _state = State.Idle;
    }
    void Update()
    {
        if (HasKitchenObject())
        {
            switch (_state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    _fryingTimer += Time.deltaTime;
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                    {
                        _progressNormalized = _fryingTimer / _fryingRecipeSO._fryingTimerMax
                    });
                    
                    if (_fryingTimer > _fryingRecipeSO._fryingTimerMax)
                    {
                        // frying
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(_fryingRecipeSO._output, this);

                        _state = State.Fried;
                        _burningTimer = 0f;
                        _burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                        {
                            _state = _state,
                        });
                    }
                    break;
                case State.Fried:
                    _burningTimer += Time.deltaTime;
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                    {
                        _progressNormalized = _burningTimer / _burningRecipeSO._burningTimerMax
                    });
                    
                    if (_burningTimer > _burningRecipeSO._burningTimerMax)
                    {
                        // fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(_burningRecipeSO._output, this);

                        _state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                        {
                            _state = _state,
                        });
                        
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                        {
                            _progressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:
                    break;
            
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // there is no kitchen object here
            if (player.HasKitchenObject())
            {
                // player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // player is carrying something that can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    
                    _fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    _state = State.Frying;
                    _fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                    {
                        _state = _state,
                    });
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                    {
                        _progressNormalized = _fryingTimer / _fryingRecipeSO._fryingTimerMax
                    });
                }
            }
            else
            {
                // player is not carrying anything
            }
        }
        else
        {
            //there is a kitchen obj here
            if (player.HasKitchenObject())
            {
                // player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        
                        _state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                        {
                            _state = _state,
                        });
                
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                        {
                            _progressNormalized = 0f
                        });
                    } 
                }
            }
            else
            {
                // player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
                _state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                {
                    _state = _state,
                });
                
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                {
                    _progressNormalized = 0f
                });
            }
        }
    }
    
    bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO._output;
        }
        return null;
    }

    FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in _fryingRecipeSOArray)
        {
            if (fryingRecipeSO._input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }

        return null;
    }
    BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in _burningRecipeSOArray)
        {
            if (burningRecipeSO._input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }

        return null;
    }

    public bool IsFried()
    {
        return _state == State.Fried;
    }
}
