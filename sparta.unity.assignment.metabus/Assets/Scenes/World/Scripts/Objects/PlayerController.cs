using UnityEngine;
using UnityEngine.Serialization;

// 네임 스페이스로 동일한 이름에 대한 제한을 해결할 수 있지만, 스코프가 한번 더 발생
namespace Scene.World
{

    public class PlayerController : MonoBehaviour
    {
        public float moveOriginSpeed = 5f;  // 이동 속도
        // private float currentY;
        // private bool isJumping = false;
        // public bool canMove = true;

        private bool _isRide = false;
        private float _additionSpeed = 0f;

        private SpriteRenderer _spriteRenderer;  // 스프라이트 렌더러
        private Rigidbody2D _rigidbody2D;  // 2D Rigidbody
        
        // q: 에디터 컨벤션에서 소문자를 권장
        public GameObject seat;
        public GameObject helmetPivot;
        
        private Vector2 moveInput;
        
        private void Start()
        {
            // main sprite 에서 탐색
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.y = Input.GetAxis("Vertical");
            moveInput.Normalize(); // 대각선 이동 시 속도 균일화

            
            // [방향에 따라 뒤집기]
            // 멈췄을 때 방향도 고려 필요.(마지막 방향을 기억해야 함)
            // 방향을 뒤집는게 아니라 해당 오브젝트의 Y 회전을 가진다.
            transform.rotation = Quaternion.Euler(0, Mathf.Sign(moveInput.x) < 0 ? 0 : 180, 0);
            // _spriteRenderer.flipX = moveInput.x >= 0;
            
            // 기호로 확인(부드러운 이동)
            // spriteRenderer.flipX = Mathf.Sign(horizontal) < 0;
            // 두 값이 거의 값을 때 실행
            // spriteRenderer.flipX = Mathf.Approximately(horizontal, 1);
            
            // [점프 애니메이션 - 코드로 구현]
            // if (!isJumping)
            // {
            //     transform.position = new Vector3(transform.position.x, transform.position.y + 1);
            //     currentY = transform.position.y - 1;
            //     isJumping = true;
            // }
            // else
            // {
            //     transform.position = new Vector3(transform.position.x, currentY);
            //     isJumping = false;
            // }
            
            // [이동]
            // Space 클래스
            // Vector3는 파라미터가 4개이며 default 값에 따라 생략될 수 있음.
            // 벡터에 float 값을 직접 곱할 수 없음
            // transform.Translate(new Vector3(horizontal, vertical) * (Time.deltaTime * moveOriginSpeed));
            
        }

       

        // 물리적으로 이동 구현
        private void FixedUpdate()
        {
            // MovePosition을 사용해 부드럽게 이동
            _rigidbody2D.MovePosition(_rigidbody2D.position + moveInput * ((moveOriginSpeed + _additionSpeed) * Time.fixedDeltaTime));
        }
        
        // private void OnCollisionEnter2D(Collision2D collision)
        // {
        //     int indexOfLayer = LayerMask.NameToLayer("Water");
        //     if (collision.gameObject.layer == indexOfLayer)
        //     {
        //         
        //     }
        // }
        
        // 피봇 위치만 다름
        public void SetEquip(GameObject equipment)
        {
            // 회전이 발생한 상태에서 추가해버리면 위치가 이상해지는 현상 발생, 둘의 방향을 통일 시킴
            transform.rotation = Quaternion.Euler(Vector3.zero);
            equipment.transform.rotation = Quaternion.Euler(Vector3.zero);
            
            equipment.transform.parent = helmetPivot.transform;
            // 위치 초기화
            equipment.transform.localPosition = Vector3.zero;
            
        }
        
        // 탈 때 위치 계산이 이상해짐
        public void SetRide(GameObject vehicle)
        {
            // 회전이 발생한 상태에서 추가해버리면 위치가 이상해지는 현상 발생, 둘의 방향을 통일 시킴
            transform.rotation = Quaternion.Euler(Vector3.zero);
            vehicle.transform.rotation = Quaternion.Euler(Vector3.zero);
            
            vehicle.transform.parent = seat.transform;
            // 위치 초기화
            vehicle.transform.localPosition = Vector3.zero;
            
            _isRide = true;
            _additionSpeed = 5;
        }
        
        public void GetOffVehicle(GameObject vehicle)
        {
            // 완전히 외부로 빠지면, 정리해둔 구조에서는 어긋나게 됨
            vehicle.transform.parent = null;
            _isRide = false;
            _additionSpeed = 0;

            // throw new System.NotImplementedException();
        }
    }

}
