namespace sparta_dungeon;

public class Dungeon(string name, int minDependence, int gold)
{
    public string Name = name;
    public readonly int MinDependence = minDependence;
    private int _gold = gold;

    public bool CheckVictory(Player player)
    {
        bool isVictory = true;
        if (player.Defense < MinDependence)
        {
            Random random = new Random();
            int percent = random.Next(0, 100);
            isVictory = percent < 40;
        }
        
        return isVictory;
    }

    public void OnClear(Player player)
    {
        Random random = new Random();
        int damage = random.Next(20 + (MinDependence - player.Defense), 35 + (MinDependence - player.Defense));
        if(damage < 0) damage = 0;
        player.CurrentHealth -= damage;
        player.Gold += _gold * (1 + random.Next((int)player.Attack / 100, (int)player.Attack * 2 / 100));

        // overDamage
        if (player.CurrentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void OnDefeat(Player player)
    {
        int leftHealth = player.CurrentHealth / 2;
        player.CurrentHealth = leftHealth;
        
        if (player.CurrentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}