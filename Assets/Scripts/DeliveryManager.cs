using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
        public static DeliveryManager Instance { get; private set; }

        public event EventHandler OnRecipeSpawned;
        public event EventHandler OnRecipeCompleted;
        public event EventHandler OnRecipeSuccess;
        public event EventHandler OnRecipeFailed;
        
        
        [SerializeField] RecipeListSO _recipeListSO;
        
        List<RecipeSO> _waitingRecipeSOList;

        float _spawnRecipeTimer;
        float _spawnRecipeTimerMax = 4f;
        int _waitingRecipesMax = 4;
        int _successfulRecipesAmount;

        void Awake()
        {
                Instance = this;
                
                _waitingRecipeSOList = new List<RecipeSO>();
        }
        
        
        void Update()
        {
                _spawnRecipeTimer -= Time.deltaTime;
                if (_spawnRecipeTimer <= 0f)
                { 
                        _spawnRecipeTimer = _spawnRecipeTimerMax;

                        if (KitchenGameManager.Instance.IsGamePlaying() && _waitingRecipeSOList.Count < _waitingRecipesMax)
                        {
                                RecipeSO waitingRecipeSO = _recipeListSO._recipeSOList[UnityEngine.Random.Range(0, _recipeListSO._recipeSOList.Count)];
                                
                                _waitingRecipeSOList.Add(waitingRecipeSO);  
                                
                                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                        }
                }
        }

        public List<RecipeSO> GetWaitingRecipeSOList()
        {
                return _waitingRecipeSOList;
        }

        public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
        {
                for (int i = 0; i < _waitingRecipeSOList.Count; i++)
                {
                        RecipeSO waitingRecipeSO = _waitingRecipeSOList[i];

                        if (waitingRecipeSO._KitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
                        {
                                // has the same number of ingredients
                                bool plateContentsMatchesRecipe = true;
                                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO._KitchenObjectSOList)
                                {
                                        // cycling through all ingredients in the recipe
                                        bool ingredientFound = false;
                                        foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                                        {
                                                // cycling through all ingredients in the plate
                                                if (plateKitchenObjectSO == recipeKitchenObjectSO)
                                                {
                                                        // ingredient matches
                                                        ingredientFound = true;
                                                        break;
                                                }
                                        }
                                        if (!ingredientFound)
                                        {
                                                // this recipe ingredient was not found on the plate
                                                plateContentsMatchesRecipe = false;
                                        }
                                }
                                if (plateContentsMatchesRecipe)
                                {
                                        // player delivered the correct recipe
                                        _successfulRecipesAmount++;
                                        _waitingRecipeSOList.RemoveAt(i);
                                        
                                        OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                                        OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                                        return;
                                }
                        }
                }
                // No matches found!
                // Player didn't deliver a correct recipe
                OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        }

        public int GetSuccesfulRecipesAmount()
        {
                return _successfulRecipesAmount;
        }

}
