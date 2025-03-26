using UnityEngine;

public interface IClickable
{
    public void OnClick();
}

// click manager의 역할도 필요 : 클릭된 대상의 이벤트 실행
public class MouseTracer : MonoBehaviour
{
    private GameObject GameMouseCursor;
    // 마우스 숨기기
    private void Start()
    {
        // 할당된 게임 오브젝트를 마우스 오브젝트로 등록
        GameMouseCursor = this.gameObject;
        
        // 커서 클래스로 마우스 접근
        Cursor.visible = false;
        // 마우스 잠금(이러면 이동 불가?)
        // Cursor.lockState = CursorLockMode.Locked;
    }

    // 카메라부터 캐릭터 움직임 까지 FixedUpdate에서 발생해서, 이곳에 두지 않으면 떨림 현상이 발생함.
    private void FixedUpdate()
    {
        Vector3 mousePosition = Input.mousePosition;
        // 마우스의 위치는 카메라와의 거리 계산
        // mousePosition.z = -Camera.main.transform.position.z;
        mousePosition.z = 10;
        
        // 스크린 좌표(화면 픽셀 기준)를 월드 좌표(게임 세계 기준)로 변환
        GameMouseCursor.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);

        // 클릭 매니저: 클릭이 확인된 경우
        if (Input.GetMouseButtonDown(0))
        {
            DetectClick();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void DetectClick()
    {
        // Cameara.main 은 static을 통해 main 카메라를 접근한다.
        Vector2 mousePosition = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        // direction에 Vector2.zero를 넣는 이유는?
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider)
        {
            // 성능적으로 좋지 않을 수 있음
            IClickable clickable = hit.collider.GetComponent<IClickable>();
            if (clickable != null)
            {
                clickable.OnClick();
            }
        }
     }
}
