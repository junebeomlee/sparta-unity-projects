using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ingredient")]
public class IngredientSO: ScriptableObject
{
    [SerializeField] private string _id = System.Guid.NewGuid().ToString();
    public string name;
    public string title;
    public Sprite sprite;
}