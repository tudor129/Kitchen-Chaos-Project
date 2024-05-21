using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI _recipeNameText;
   [SerializeField] Transform _iconContainer;
   [SerializeField] Transform _iconTemplate;



   void Awake()
   {
       _iconTemplate.gameObject.SetActive(false);
   }

   public void SetRecipeSO(RecipeSO recipeSO)
   {
       _recipeNameText.text = recipeSO._recipeName;

       foreach (Transform child in _iconContainer)
       {
           if (child == _iconTemplate) continue;
           Destroy(child.gameObject);
       }

       foreach (KitchenObjectSO kitchenObjectSO in recipeSO._KitchenObjectSOList)
       {
           Transform iconTransform = Instantiate(_iconTemplate, _iconContainer);
           iconTransform.gameObject.SetActive(true);
           iconTransform.GetComponent<Image>().sprite = kitchenObjectSO._sprite;
       }
   }
}
