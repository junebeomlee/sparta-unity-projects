using UnityEngine;

// 네임 스페이스로 동일한 이름에 대한 제한을 해결할 수 있지만, 스코프가 한번 더 발생
namespace Meta
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;  // 이동 속도

        // private float currentY;
        // private bool isJumping = false;

        private SpriteRenderer _spriteRenderer;  // 스프라이트 렌더러
        private Rigidbody2D _rigidbody2D;  // 2D Rigidbody
        
        private void Start()
        {
            // main sprite 에서 탐색
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            
            // [방향에 따라 뒤집기]
            // 멈췄을 때 방향도 고려 필요.(마지막 방향을 기억해야 함)
            _spriteRenderer.flipX = horizontal >= 0;
            
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
            transform.Translate(new Vector3(horizontal, vertical) * (Time.deltaTime * moveSpeed));
            
            // 물리적으로 이동 구현
        }
    }

}
