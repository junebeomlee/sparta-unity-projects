using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Serialization;

public class GlobalManager: MonoBehaviour
{
    private static readonly string Address = "Managers/GlobalManager";
    
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeInitialize()
    {
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(Address);
        handle.Completed += (operation) =>
        {
            if (operation.Status == AsyncOperationStatus.Succeeded)
            {
                var instance = Instantiate(operation.Result);
                
                Debug.Log(1);
                // 외부에서 접근 시, 호출 순서 관련 문제 발생
                RegisterManager(instance.AddComponent<EventManager>());
                RegisterManager(instance.AddComponent<InventoryManager>());
            }
        };
    }
    
    private static GlobalManager _instance;
    private static List<Component> _managers = new();
    public List<GameObject> prefabs;
 
    void Awake()
    {
        if (_instance) return;

        _instance = this;
        foreach (var manager in prefabs) { Instantiate(manager, transform, true); }
        DontDestroyOnLoad(_instance);
    }

    public static void RegisterManager(Component manager)
    {
        // Debug.Log(manager);
        _managers.Add(manager);
    }

    public static T GetManager<T>() where T : Component
    {
        return _managers.FirstOrDefault(m => m is T) as T;
        // return _managers.OfType<T>().FirstOrDefault();
    }
}