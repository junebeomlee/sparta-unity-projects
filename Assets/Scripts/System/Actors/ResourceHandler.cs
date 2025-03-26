using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour의 갯수를 줄이는 것이 중요하므로 SO를 활용
public class ResourceHandler: MonoBehaviour
{
    private Controller _controller; // 중재자 패턴으로 핸들러 관리
    
    public StatisticSO statistic;
    private List<Condition> _conditions = new();

    // public ResourceHandler()
    
    private void Awake()
    {
        _controller = GetComponent<Controller>();
        
        foreach (var stat in statistic.Stats)
        {
            _conditions.Add(new Condition(stat.type, stat.value));
        }
    }

    // notice : 코드 중복이 발생하는 것이 아닌 데코레이터 패턴 관점으로 보는 것이 좋다.
    public void Modify(StatType type, int value)
    {
        var selectedCondition = _conditions.Find(condition => condition.type == type);
        selectedCondition.Modify(value);
        
        Debug.Log(value.ToString());
        Debug.Log("현재 값 " + selectedCondition.value);
        
        // notice: 이벤트 알림
        // GlobalManager.GetManager<EventManager>().Notify(selectedCondition.type.ToString(), selectedCondition.value);
        
        // die
        if (selectedCondition.type == StatType.Health && selectedCondition.value <= 0)
        {
            _controller.Die();
        }
    }
    
    
}