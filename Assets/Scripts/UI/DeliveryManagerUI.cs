using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
   [SerializeField] Transform _container;
   [SerializeField] Transform _recipeTemplate;

   void Awake()
   {
      _recipeTemplate.gameObject.SetActive(false);
   }

   void Start()
   {
      DeliveryManager.Instance.OnRecipeSpawned += DeliveryManagerInstance_OnRecipeSpawned;
      DeliveryManager.Instance.OnRecipeCompleted += DeliveryManagerInstance_OnRecipeCompleted;
      
      UpdateVisual();
   }
   void DeliveryManagerInstance_OnRecipeCompleted(object sender, EventArgs e)
   {
      UpdateVisual();
   }
   void DeliveryManagerInstance_OnRecipeSpawned(object sender, EventArgs e)
   {
      UpdateVisual();
   }
   void UpdateVisual()
   {
      foreach (Transform child in _container)
      {
         if (child == _recipeTemplate) continue;
         Destroy(child.gameObject);
      }

      foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
      {
         Transform recipeTransform = Instantiate(_recipeTemplate, _container);
         recipeTransform.gameObject.SetActive(true);
         recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
      }
   }
}
