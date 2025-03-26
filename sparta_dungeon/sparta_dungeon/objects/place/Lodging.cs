namespace sparta_dungeon;

public class Lodging(int cost)
{
    private int Cost { get; set;} = cost;

    public enum Status
    {
        FAIL_REQUIRE_MONEY,
        MAX_HEALTH,
        SUCCESS
    }

    public bool IsAffordable(int gold)
    {
        return gold - this.Cost >= 0;
    }
    
    public void TakeRest(Player player)
    {
        player.CurrentHealth = player.MaxHealth;
        player.Gold -= Cost;
    }
}