using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    public string id;
    public string title;
    public string description;
    public Sprite sprite;
}
