
using System;

public class EventValue<T>
{
    private T _value;
    private Action<T> OnValueChanged;
    
}

public class EventBusSystem
{
    public EventValue<object>[] EventListeners;    
}