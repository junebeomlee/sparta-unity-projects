using UnityEngine;

[CreateAssetMenu(menuName = "SO/item")]
public class ItemSO: ScriptableObject
{
    [SerializeField] private string _id = System.Guid.NewGuid().ToString();
    public string name;
}

[CreateAssetMenu(menuName = "SO/consumableItem")]
public class ConsumableItemSO : ItemSO
{
    public StatType statType;
    public int amount;
}

[CreateAssetMenu(menuName = "SO/buffItem")]
public class BuffItemSO : ItemSO
{
    private string _id = System.Guid.NewGuid().ToString();
    public int durationTurn;
}