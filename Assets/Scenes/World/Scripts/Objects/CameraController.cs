using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 플레이어 인스펙터에서 주입 후 추적
    public GameObject Target;
    public float smoothTime = 0.3f;  // 부드럽게 이동하는 시간
    public Vector3 offset;  // 카메라와 목표 간의 오프셋
    private Vector3 velocity = Vector3.zero;  // 속도 변수 (SmoothDamp에 필요)

    // fix: update로 진행할 경우, rigidBody를 통한 FixedUpdate 이동 방식과 충돌하면서 캐릭터가 떨리듯이 보임
    void FixedUpdate()
    {
        // 대상이 있는 경우, 추적 준비 완료
        if (!Target) { return; }

        // 목표 추적 간의 간격
        Vector3 newPos = Target.transform.position + offset;
        // 카메로 Z는 고정
        newPos.z = transform.position.z;

        // camera.transform.position = newPos;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);
    }
}
