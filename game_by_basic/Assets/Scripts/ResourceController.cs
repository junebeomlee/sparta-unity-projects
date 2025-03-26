using System.Collections.Generic;

public class ResourceController
{
    // info: 정적 순수 스탯을 데이터 클래스로 받아온다.
    public Dictionary<string, Resource> Resources = new();
    private Stats _stats;

    public ResourceController(Stats stats)
    {
        // mean: dictionary로 값을 등록하여 initialize 한다.
        // learn: 
        foreach (var field in stats.GetType().GetFields())
        {
            if (field.FieldType == typeof(float))
            {
                float value = (float)field.GetValue(stats);
                Resources[field.Name] = new Resource(value);
            }
        }
    }

    public void Modify(string name, float amount)
    {
        if (!Resources.ContainsKey(name)) { return; }
        Resources[name].Modify(amount);
    }
    
    public void ApplyStatsChange()
    {
        foreach (var field in _stats.GetType().GetFields())
        {
            if (field.FieldType == typeof(float) && Resources.ContainsKey(field.Name))
            {
                float newValue = (float)field.GetValue(_stats);
                Resources[field.Name].SetMaxValue(newValue);
            }
        }
    }
}