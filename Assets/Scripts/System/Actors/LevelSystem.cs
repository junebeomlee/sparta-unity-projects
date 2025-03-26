// idea: 레벨 데이터도 SO를 통해 관리

using System.Collections.Generic;

public class LevelSystem
{
    private List<Condition> _conditions = new();

    public int currLevel { get; private set; }
    public int experience { get; private set; }
    
    // public Statistic currentStatistic { get; private set; }
    
    public LevelSystem(int level = 1)
    {
        currLevel = level;
        // currentStatistic = new Statistic();
    }

    public void ModifyExperience(int amount)
    {
        experience += amount;
    }

    public void LevelUp()
    {
        currLevel++;
        // currentStatistic
    }
    
}