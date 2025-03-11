using System;
using JetBrains.Annotations;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField] private float _distance = 3f;
    [SerializeField] private float _interpolation = 0.5f;
    private float _lastExecutedTime = 0f;
    
    [CanBeNull] private ObjectNotification _previousObjectNotification;
    
    
    void Update()
    {
        // learn 주기적 실행법 : Time.time 체크, Time.deltaTime 누적, 코루틴 
        if(Time.time - _lastExecutedTime < _interpolation) return;
        _lastExecutedTime = Time.time;

        // fix: 카메라와 플레이어 액션을 분리하려먼서 문제 발생 - 카메라를 기준으로 레이 발사로 일시 해결
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        
        // learn: transform.forward(현재 바라보는 방향)
        if (!Physics.Raycast(ray, out RaycastHit hit, _distance) ||
            !hit.collider.TryGetComponent<ObjectNotification>(out var notification))
        {
            if (_previousObjectNotification)
            {
                _previousObjectNotification = null;
                Managers.Get<UIManager>().Disable("ObjectNotification");
            }

            return;
        }
        // notice: 캐싱으로 중복을 줄이기 위함
        if(Equals(_previousObjectNotification, notification)) return;
        
        Managers.Get<UIManager>().Show("ObjectNotification");
        Managers.Get<UIManager>().currentPage.GetComponent<ObjectNotifyPage>().Set(notification);
        Managers.Get<AudioManager>().PlaySFX("DM-CGS-03");
        _previousObjectNotification = notification;
    }

    private void OnDrawGizmos()
    {
        if (!Camera.main) return;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(ray.origin, ray.direction * _distance);
        // Gizmos.DrawRay(transform.position, transform.forward * _distance);
    }
}
