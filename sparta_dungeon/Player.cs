namespace sparta_dungeon;

public class Player
{
    public string Class;
    public string Name { get; set; }
    public int Level { get; set; }
    
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Gold { get; set; }
    
    public List<Item> Inventory { get; set; } = new List<Item>();
    public List<Item> Equipped = new List<Item>();
    
        
    public Player()
    {
        Class = "전사";
        Level = 1;
        MaxHealth = 100;
        CurrentHealth = 100;
        Attack = 10;
        Defense = 5;
        Gold = 1500;
    }
}