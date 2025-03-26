using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Recipe
{   
    [FormerlySerializedAs("ingredient")] public IngredientSO ingredientSO;
    public int amount;
}

[CreateAssetMenu(menuName = "SO/Recipe")]
public class RecipeSO: ScriptableObject
{
    public List<Recipe> requiredItems;
    public ItemSO resultItem;
}