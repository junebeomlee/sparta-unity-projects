using Scene.Plane;
using UnityEngine;
using UnityEngine.Serialization;

public class Plane : MonoBehaviour
{
    // 워딩 변경
    GameManager GameManager;
    
    // 애니메이션 파라미터 등록하는 법, 규칙이 필요
    private static readonly int IsDie = Animator.StringToHash("isDie");
    
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    // Start is called before the first frame update

    public float flapForce = 6f;
    public float forwardSeed = 3f;
    public bool isDead = false;
    public float deathCoolDown = 0f;

    private bool _isFlap = false;
    [FormerlySerializedAs("godMode")] public bool infiniteMode = false;
    
    void Start()
    {
        // 게임 매니저 접근
        GameManager = GameManager.Instance;
        
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (!_rigidbody && !_animator) { Debug.LogWarning("No components found!"); }
    }

    // 가장 처음에 인식이 시작 버튼을 누르면 시작되도록 처리 필요.
    void Update()
    {
        if (isDead)
        {
            if (deathCoolDown <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    // 게임 재시작
                    GameManager.Instance.RestartGame();
                }
            }
            else
            {
                deathCoolDown -= Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            _isFlap = true;
        }
        
    }

    private void FixedUpdate()
    {
        if(isDead) { return; }
        
        Vector3 velocity = _rigidbody.linearVelocity;
        velocity.x = flapForce;

        if (_isFlap)
        {
            velocity.y += flapForce;
            _isFlap = false;
        }
        
        _rigidbody.linearVelocity = velocity;
        
        // 물리적 방식으로 회전을 구현
        float angle = Mathf.Clamp(_rigidbody.linearVelocity.y * 10f, -90f, 90f);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    
    // 충돌은 콜리전 또는 트리거(트리거는 물리 충돌은 발생 X)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(infiniteMode) { return; }
        if(isDead) { return; }
        
        isDead = true;
        deathCoolDown = 1f;
        
        _animator.SetBool(IsDie, true);
        
        // 게임 오버
        GameManager.Instance.GameOver();
    }
}
