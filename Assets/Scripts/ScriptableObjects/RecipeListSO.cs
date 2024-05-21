using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()] this is for safety, because we will probably need only one of this SO
public class RecipeListSO : ScriptableObject
{
        public List<RecipeSO> _recipeSOList;
}
