using UnityEngine;
using UnityEngine.InputSystem;

// 초점 변경
public class CameraTracer : MonoBehaviour
{
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    
    private bool _isFPV = true; // 1인칭
    private float _transitionTime = 1f;
    
    public void OnTogglePOV(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        _isFPV = !_isFPV;
        _transitionTime = 0;
        
        Managers.Get<AudioManager>().PlaySFX("DM-CGS-19");
        // notice: 시점 전환
        if (_isFPV)
        {
            // transform 이 복합 정보를 가지므로
            targetPosition = new Vector3(0, 0, 0);
            targetRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            targetPosition = new Vector3(0, 3, -3);
            targetRotation = Quaternion.Euler(25, 0, 0);
        }
    }

    void LateUpdate()
    {
        if (_transitionTime < 1f)
        {
            _transitionTime += Time.deltaTime;
            // 0부터 1까지 서서히 진행
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, _transitionTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, _transitionTime);

        }
    }
}
