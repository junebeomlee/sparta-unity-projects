using System;
using System.Collections.Generic;
using UnityEngine;

// feat: service locator 패턴
// 프리팹을 받아 child로 생성하도록 관리
public class Managers: MonoBehaviour
{
    private static readonly List<Manager> _managers = new List<Manager>();
    private static GameObject _globalObject;
    
    // fix: 런타임 실행하는 기능과 글로벌 싱글톤이 분리되는 것이 나을 듯
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
