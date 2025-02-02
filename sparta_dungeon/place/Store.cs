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

    public void Buy(Item item, Player player)
    {
        int leftMoney = player.Gold - item.Price;
        if (leftMoney < 0)
        {
            // 시스템적 에러를 여기서 쓰는 것이 맞는 지?
            throw new Exception("not enough gold");
        }
        else
        {
            player.Inventory.Add(item);
            player.Gold = leftMoney;
        }
    }

    public void Sell(Item item, Player player)
    {
        
    }
}