using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Movements : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _direction;
    
    private Animator _animator;

    // refactor: 플레이어의 이동과 관려된 부분
    public float moveSpeed = 3f;
    public float jumpForce = 10f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // if (_direction == Vector3.zero && _animator.GetBool(IsWalking)) _animator.SetBool(IsWalking, false);
        // if(_direction != Vector3.zero && !_animator.GetBool(IsWalking)) _animator.SetBool(IsWalking, true);
        
        var velocity = transform.forward * _direction.z + transform.right * _direction.x;
        velocity *= moveSpeed;
        velocity.y = _rigidbody.velocity.y;

        // bug: 탑승체와 연결되었을 때 MovePosition은 정상동작되지 않으며, velocity를 직접 변경할 경우 tileMap에서 충돌처럼 보이는 현상 발생
        if (!transform.parent)
        {
            _rigidbody.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        }
        else
        {
            _rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
        }

    }
    
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        // fix: 방황 변환을 어디서 해야하는 지?
        _direction = new Vector3(value.x, 0.0f, value.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        
        // refactor : 애니메이션 담당에게 점프 요청(강결합 발생)
        GetComponent<PlayerAnimation>().GetAnimator().SetTrigger(PlayerAnimation.GetHash(PlayerAnimation.AnimParam.IsJumping));
    }
}
