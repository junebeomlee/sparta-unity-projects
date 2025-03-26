using System;
using UnityEngine;

public class Resource
{
    // question: 생성자의 위치는 어디가 좋을까?
    public Resource(float initValue)
    {
        _maxValue = initValue;
        _value = _maxValue;
    }
    
    private float _maxValue;
    public float MaxValue => _maxValue; 
    
    // 자료형을 float으로 하는 것이 맞을까?
    private float _value;
    public float Value
    {
        get => _value;
        // 외부에서 값을 변경 할 수 없다.
        private set {
            // Mathf는 유니티의 Math 클래스이며 Clamp는 min과 max를 두고 제한하는 방식이다.
            _value = Mathf.Clamp(value, 0, _maxValue);
            OnValueChanged?.Invoke(_value);
        } 
    }
    
    // 변경 사항을 감지해야 하는 경우
    public event Action<float> OnValueChanged;
    
    public void Modify(float amount)
    {
        Value += amount;
    }
    
    public void SetMaxValue(float newMaxValue)
    {
        if (newMaxValue <= 0) return;
        _maxValue = newMaxValue;
        _value = Mathf.Clamp(_value, 0, _maxValue);
    }
}