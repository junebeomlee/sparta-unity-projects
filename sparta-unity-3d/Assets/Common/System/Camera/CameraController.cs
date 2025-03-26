using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// fix: 2가지 코드 섞인 점 관리 필요
public class CameraController : MonoBehaviour
{
    public Transform player;
    public float sensitivity = 3f;

    private Vector2 _mouseDirection;
    private float _cameraXRotation;
    
    private bool _isTracing = true;
    
    // 커서 숨김
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // Mouse.current.position.ReadValue();
        // question: normalized 값이 들어가는 건가?
        _mouseDirection = context.ReadValue<Vector2>();
        // Debug.Log(_mouseDirection);
    }

    public void RotateCamera()
    {
        // explain : Y 값을 받아, 카메라 X 회전(곧 상하 회전)으로 적용
        _cameraXRotation -= _mouseDirection.y * sensitivity;
        _cameraXRotation = Mathf.Clamp(_cameraXRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(_cameraXRotation, 0f, 0f);
        
        player.eulerAngles += new Vector3(0, _mouseDirection.x * sensitivity, 0);
    }

    public void ToggleState()
    {
        _isTracing = !_isTracing;
        Cursor.lockState = _isTracing ? CursorLockMode.Locked : CursorLockMode.None;
    }
 

    void LateUpdate()
    {
        if (!_isTracing) return;
        RotateCamera();
    }
}
