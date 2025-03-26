using System.Collections.Generic;
using UnityEngine;

public enum StatType {
    Health,
    Power,
}

[System.Serializable]
public class Stat
{
    public StatType type;
    public int value;
}

// 중복 또는 누락 발생할 수 있음
[CreateAssetMenu(menuName = "SO/statistic")]
public class StatisticSO : ScriptableObject
{
    public List<Stat> Stats;
}