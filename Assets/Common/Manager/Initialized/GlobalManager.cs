using System;
using System.Collections.Generic;
using UnityEngine;

// feat: service locator 패턴
public class GlobalManager: MonoBehaviour
{
    private static readonly List<Manager> _managers = new List<Manager>();
    private static GameObject _globalObject;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        if (_globalObject) return;
        
        _globalObject = new GameObject("GlobalManager");
        DontDestroyOnLoad(_globalObject);
    }
    
    //do: unregister도 쌍으로 존재하도록 관리 필요
    public static void Register(Manager manager)
    {
        _managers.Add(manager);
        manager.transform.SetParent(_globalObject.transform, false);
    }
    
    // learn: 목적어는 파라미터로 대치(getManager -> get)하여 SRP 원칙도 지키며 컨벤션 관리
    public static T Get<T>() where T : Manager
    {
        foreach (Manager manager in _managers)
        {
            if (manager is T) return (T)manager;
        }
        
        throw new System.Exception($"{typeof(T).Name} not found");
    }
}
