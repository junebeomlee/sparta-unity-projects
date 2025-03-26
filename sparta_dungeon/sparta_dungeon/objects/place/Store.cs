namespace sparta_dungeon;

using System.Text.Json;

public class Store
{
    public static List<Item> Items;
    private string itemListPath = "gameItems.json";

    public enum Mode { None, Buying, Selling }
    
    public Store()
    {
        if (Items is not null) return;
        string itemListFile = File.ReadAllText(itemListPath);
        Items = JsonSerializer.Deserialize<List<Item>>(itemListFile);
    }

    public bool IsEnoughGold(Item item, int gold)
    {
        return gold - item.Price >= 0; 

    }
    public void SendItemTo(Item item, Player player)
    {
        int leftMoney = player.Gold - item.Price;
        player.Inventory.Add(item);
        player.Gold = leftMoney;
    }

    public void GetItemFrom(Item item, Player player)
    {
        player.Inventory.Remove(item);
        player.Gold += (int)Math.Truncate(item.Price * 0.6f);
    }
}