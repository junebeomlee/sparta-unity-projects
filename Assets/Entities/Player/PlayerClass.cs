using Actor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClass : MonoBehaviour
{
    // private Resource _resource;
    // private Status _status;
    private PlayerAnimation _animation;
    private Movements _movements;
    
    private CameraController _cameraController;
    private CameraTracer _cameraTracer;
    private GroundDetector _groundDetector;

    void Start()
    {
        // _resource = GetComponent<Resource>();
        // _status = GetComponent<Status>();
        _movements = GetComponent<Movements>();
        _animation = GetComponent<PlayerAnimation>();
        
        _cameraController = GetComponentInChildren<CameraController>();
        // 모든 하위 객체 검사
        _cameraTracer = GetComponentInChildren<CameraTracer>(true);
    }
    
    
    // 인터페이스 관련 단축키는 어디서 관리되어야 하는가?
    public void ShowInventoryPage(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        UIManager uiManager = Managers.Get<UIManager>();
        
        bool currentState = uiManager.transform.Find("Inventory")?.gameObject.activeSelf ?? false;

        _cameraController.ToggleState();
        if (!currentState)
        {
            uiManager.Show("Inventory");
        }
        else
        {
            uiManager.Disable("Inventory");
        }
    }
}
