namespace sparta_dungeon;

public class Lodging(int cost)
{
    public int Count = cost;
    
    public bool TakeRest(Player player)
    {
        int leftMoney = player.Gold - cost;
        if (leftMoney < 0)
        {
            return false;
        }
        else
        {
            player.Gold = leftMoney;
            player.CurrentHealth = player.MaxHealth;
            return true;
        }
    }
}