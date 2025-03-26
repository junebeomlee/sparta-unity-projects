using UnityEngine;

// properties
public class Condition
{
    public StatType type {get; private set;}
    public int value { get; private set; }
    private int maxValue;
    
    public Condition(StatType type, int initialValue)
    {
        type = type;
        value = initialValue;
        maxValue = initialValue;
    }

    public void Modify(int amount)
    {
        value += amount;
        value = Mathf.Clamp(value, 0, maxValue);
    }
}