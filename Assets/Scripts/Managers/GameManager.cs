using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    private Controls _controls;
    public List<Store> stores = new();

    private void Awake()
    {   
        _controls = new Controls();
        _controls.Enable();
      
    }

    private void Start()
    {
        _controls.Player.ESC.performed += ctx =>   
        {
            GlobalManager.GetManager<UIManager>().Open(UIManager.PageType.Settings);    
        };

        stores.Add(new Store(new[] {"9fb23e42-cd49-4fa8-ad5c-037b2043a14d"}));
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0; // UI와 사운드 제외
    }
    

    public void SaveToJson(string filePath)
    {
        // stateMachine을 JSON 형식으로 직렬화하여 파일로 저장
        // string jsonString = JsonSerializer.
        // string jsonString = JsonUtility.ToJson();

    }

    
    private List<Controller> _playerControllers = new();

    // 글로벌 매니저 역할....?
    public void RegisterPlayerController(Controller controller)
    {
        _playerControllers.Add(controller);
    }

    public void SaveGame()
    {
        
    }
}