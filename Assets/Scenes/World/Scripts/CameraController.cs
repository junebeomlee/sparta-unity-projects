using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera camera;
    // 플레이어 인스펙터에서 주입 후 추적
    public GameObject player;
    
    private bool isFoundTarget = false;
    
    void Start()
    {
        // 메인 카메라 주입
        this.camera = Camera.main;
        // 대상이 있는 경우, 추적 준비 완료
        if (!player) { this.isFoundTarget = true; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
