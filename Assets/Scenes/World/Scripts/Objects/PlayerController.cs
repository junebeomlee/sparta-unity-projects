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
            _spriteRenderer.flipX = moveInput.x >= 0;
            
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

        // 탈 때 위치 계산이 이상해짐
        public void SetRide(GameObject vehicle)
        {
            vehicle.transform.parent = seat.transform;
            // 위치 초기화
            vehicle.transform.localPosition = Vector3.zero;
            
            _isRide = true;
            _additionSpeed = 5;
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
    }

}
