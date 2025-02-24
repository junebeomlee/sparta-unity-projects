using Unity.VisualScripting;
using UnityEngine;

public class Player: MonoBehaviour
{
    public StatManager stats;

    private Rigidbody _rigidbody;
    private Renderer _renderer;

    private void Start()
    {

        // 내부 구현 방식 때문인지 필드 선언과 함께 할당은 되지 않음
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }
    
    // bool IsWithinRange(Vector3 targetPosition)
    // {
    //     // 현재 위치와 클릭한 위치의 차이를 계산하여 범위 내에 있는지 확인
    //     float distance = Vector3.Distance(transform.position, targetPosition);
    //     //최대 이동 범위
    //     return distance <= 2;
    // }
    //     
    // void HighlightMoveRange(Vector3 targetPosition)
    // {
    //     RaycastHit hit;
    //     // 선택된 블록의 상단만 색상을 변경하여 이동 가능한 범위 시각화
    //     Ray ray = new Ray(targetPosition + Vector3.up, Vector3.down);  // 상단에 Ray 발사
    //
    //
    //     if (Physics.Raycast(ray, out hit))
    //     {
    //         //선택된 대상의 renderer 정보
    //         Renderer _rend = hit.transform.GetComponent<Renderer>();
    //         // 플레이어의 렌더러가 아닌 선택된 대상의 렌더러
    //         if (_rend is not null)
    //         {
    //             // 상단 색상을 변경 (예시: 파란색으로)
    //             _rend.material.color = Color.magenta;
    //         }
    //     }
    // }

    public void Update()
    {
        

        // if (Input.GetMouseButton(0))
        // {
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;
        //     
        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         Vector3 targetPosition = hit.transform.position;
        //         // 클릭한 블록이 이동 범위 내에 있는지 확인
        //         if (IsWithinRange(targetPosition))
        //         {
        //             HighlightMoveRange(targetPosition);
        //         }
        //     }
        // }
        
        // [Translate를 이용한 방법] - 내부 구조 알아보기
        // if(Input.GetKey(KeyCode.DownArrow)) {
        //     // Translate는 현재 위치 기반하여 변경을 준다.
        //     // 상하좌우가 X와 Z축 기준이나 Vector3.down은 Y축
        //     transform.Translate(Vector3.back * Time.deltaTime);
        // }
        // if(Input.GetKey(KeyCode.UpArrow)) {
        //     transform.Translate(Vector3.forward * Time.deltaTime);
        // }
        // if(Input.GetKey(KeyCode.LeftArrow)) {
        //     transform.Translate(Vector3.left * Time.deltaTime);
        // }
        // if(Input.GetKey(KeyCode.RightArrow)) {
        //     transform.Translate(Vector3.right * Time.deltaTime);
        // }
      
        // [정확히 한칸만 이동하고 싶은 경우] : KeyDown을 활용
        // if(Input.GetKeyDown(KeyCode.DownArrow)) {
        //     transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        // }
        // if(Input.GetKeyDown(KeyCode.UpArrow)) {
        //     transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        // }
        // if(Input.GetKeyDown(KeyCode.LeftArrow)) {
        //     transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        // }
        // if(Input.GetKeyDown(KeyCode.RightArrow)) {
        //     transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        // }
        
        //[rigidBody 이용]
        if(Input.GetKeyDown(KeyCode.DownArrow)) {
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            _rigidbody.MovePosition(targetPosition);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            _rigidbody.MovePosition(targetPosition);
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            Vector3 targetPosition = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            _rigidbody.MovePosition(targetPosition);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            Vector3 targetPosition = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            _rigidbody.MovePosition(targetPosition);
        }
        
        //[rigidBody 이용 2]
        // if(Input.GetKey(KeyCode.DownArrow)) {
        //     Vector3 startPosition = transform.position;
        //     Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        //     float moveSpeed = 1.0f;  // 이동 속도
        //     transform.position = Vector3.Lerp(startPosition, targetPosition, moveSpeed * Time.deltaTime);
        // }
        // if(Input.GetKey(KeyCode.UpArrow)) {
        //     Vector3 startPosition = transform.position;
        //     Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        //     float moveSpeed = 1.0f;  // 이동 속도
        //     transform.position = Vector3.Lerp(startPosition, targetPosition, moveSpeed * Time.deltaTime);
        // }
        // if(Input.GetKey(KeyCode.LeftArrow)) {
        //     Vector3 startPosition = transform.position;
        //     Vector3 targetPosition = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        //     float moveSpeed = 1.0f;  // 이동 속도
        //     transform.position = Vector3.Lerp(startPosition, targetPosition, moveSpeed * Time.deltaTime);
        // }
        // if(Input.GetKey(KeyCode.RightArrow)) {
        //     Vector3 startPosition = transform.position;
        //     Vector3 targetPosition = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        //     float moveSpeed = 1.0f;  // 이동 속도
        //     transform.position = Vector3.Lerp(startPosition, targetPosition, moveSpeed * Time.deltaTime);
        // }
        
        // [방향에 따른 회전 방향 표시]
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;
        if (moveDirection.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);

            // if(Input.GetKeyDown(KeyCode.DownArrow)) {
            //     Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            //     _rigidbody.MovePosition(targetPosition);
            // }
            // if(Input.GetKeyDown(KeyCode.UpArrow)) {
            //     Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            //     _rigidbody.MovePosition(targetPosition);
            // }
            // if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            //     Vector3 targetPosition = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            //     _rigidbody.MovePosition(targetPosition);
            // }
            // if(Input.GetKeyDown(KeyCode.RightArrow)) {
            //     Vector3 targetPosition = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            //     _rigidbody.MovePosition(targetPosition);
            // }
        }
    }

    // public void OnMouseDown()
    // {
        // _renderer.material.color = Color.green;
    // }
}