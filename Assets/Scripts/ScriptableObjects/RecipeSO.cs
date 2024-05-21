using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
        public List<KitchenObjectSO> _KitchenObjectSOList;
        public string _recipeName;
}
