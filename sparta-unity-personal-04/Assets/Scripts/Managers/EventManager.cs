using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class observed
{
    public string type;
    public Action<string> callback;
}

public class EventManager: MonoBehaviour
{
    private List<observed> observedElements = new List<observed>();

    public void Subscribe(string type, Action<string> callback)
    {
        observedElements.Add(new observed { type = type, callback = callback });
    }

    public void UnSubscribe(observed observed)
    {
        observedElements.Remove(observed);
    }
    
    public void Notify(string typeValue, string changedValue)
    {
        var elements = observedElements.FindAll(observed => observed.type == typeValue);

        foreach (var element in elements)
        {
            element.callback(changedValue);
        }
    }
}