using System.Text.Json.Serialization;
using System.Text.Json;

namespace sparta_dungeon;

public class Player
{
    [JsonPropertyName("class")]
    public string Class { get; set; } = "전사";
    
    [JsonPropertyName("level")]
    public int Level { get; set; } = 1;
    
    [JsonPropertyName("maxHealth")]
    public int MaxHealth { get; set; } = 100;
    
    [JsonPropertyName("currentHealth")]
    public int CurrentHealth { get; set; } = 100;
    
    [JsonPropertyName("attack")]
    public float Attack { get; set; } = 10;
    
    [JsonPropertyName("defense")]
    public int Defense { get; set; } = 5;
    
    [JsonPropertyName("gold")]
    public int Gold { get; set; } = 1500;
    
    [JsonPropertyName("equipped")]
    public List<Item> Equipped { get; set; } = new List<Item>();

    [JsonPropertyName("inventory")]
    public List<Item> Inventory { get; set; } = new List<Item>();
    
    public void LevelUp()
    {
        this.Level += 1;
        this.Attack += 0.5f;
        this.Defense += 1;
    }

    public int GetStatByEquipments(string equipType)
    {
        return Equipped.Where(equipment => equipment.Type == equipType).Sum(equipment => equipment.Value);
    }

    public bool IsEquippedType(Item item)
    {
        return Inventory.Contains(item);
    }

    public Player Snapshot()
    {
        string serialized = JsonSerializer.Serialize(this);
        return JsonSerializer.Deserialize<Player>(serialized);
    }
}