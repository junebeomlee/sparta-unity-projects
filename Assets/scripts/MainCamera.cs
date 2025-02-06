using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Vector3 _originalPosition;
    public float shakeMagnitude = 0.05f;
    public float shakeDuration = 0.5f;
    private float shakeTimeRemaining = 0f;
    void Start()
    {
        _originalPosition = transform.position; // 카메라의 원래 위치 저장

    }

    void Update()
    {
        if (shakeTimeRemaining > 0)
        {
            // 흔들림이 남아 있다면 카메라 위치를 랜덤으로 이동
            transform.position = _originalPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            
            // 남은 시간 감소
            shakeTimeRemaining -= Time.deltaTime;
        }
        else
        {
            // 흔들림 시간이 끝났으면 원래 위치로 돌아감
            transform.position = _originalPosition;
        }
    }
    
    public void TriggerShake(float magnitude, float duration)
    {
        shakeMagnitude = magnitude;
        shakeDuration = duration;
        shakeTimeRemaining = duration;
    }
}