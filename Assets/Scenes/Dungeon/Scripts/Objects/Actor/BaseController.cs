using UnityEngine;

public class BaseController : MonoBehaviour
{
    // 왜 여기는 protected?
    protected Rigidbody2D _rigidbody;
    
    // 직렬화: private을 유니티에서 접근하거나 range 등을 설정할 때 이용
    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    // 이동 방향
    protected Vector2 movementDirection = Vector2.zero;
    
    public Vector2 MovementDirection{get{return movementDirection;}}
    
    // 보는 방향
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection{get{return lookDirection;}}

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;
    
    // 애니메이션 처리
    protected AnimationHandler AnimationHandler;
    // 스탯 처리
    protected StatHandler StatHandler;

    
    // 이건 왜 두개인가?
    [SerializeField] public WeaponHandler WeaponPrefab;
    protected WeaponHandler WeaponHandler;
    
    protected bool isAttacking = false;
    private float timeSinceLastShot = float.MaxValue;
    
    protected virtual void Awake()
    {
        // 초기화
        _rigidbody = GetComponent<Rigidbody2D>();
        AnimationHandler = GetComponent<AnimationHandler>();
        StatHandler = GetComponent<StatHandler>();

        // 프리팹 있는 경우
        if (WeaponPrefab)
        {
            // 피봇은 왜 들어가나? = 이와 같이 2번째 파마리터가 있는 경우 해당 게임 오브젝트의 child 로 들어가기 때문
            WeaponHandler = Instantiate(WeaponPrefab, weaponPivot); 
        }
        else
        {
            WeaponHandler = GetComponentInChildren<WeaponHandler>();
        }
    }

    protected virtual void Start()
    {
        
    }
    
    protected virtual void Update()
    {
        HandleAction();
        
        // 보는 방향에 따라 캐릭터 Flip
        SetFlipAndWeaponRotate(lookDirection);

        HandleAttackDelay();
    }
    
    protected virtual void FixedUpdate()
    {
        Movment(movementDirection);
        if(knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }
    
    protected virtual void HandleAction()
    {
        
    }

    private void Movment(Vector2 direction)
    {
        direction = direction * StatHandler.Speed;
        if(knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }
        
        _rigidbody.velocity = direction;
        AnimationHandler.Move(direction);
    }

    
    // 플립
    private void SetFlipAndWeaponRotate(Vector2 direction)
    {
        // 
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;
        
        // 스프라이트 렌더러 플립
        characterRenderer.flipX = isLeft;
        
        if (weaponPivot)
        {
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }
        
        // 무기 회전
        WeaponHandler.Rotate(isLeft);
    }
    
    // 피격시 밀리는 기능
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }

    public void HandleAttackDelay()
    {
        // 무기 없으면 생략
        if (!WeaponHandler) return;

        // 마지막 공격이 딜레이보다 작음면
        if (timeSinceLastShot <= WeaponHandler.Delay)
        {
            timeSinceLastShot += Time.deltaTime;
        }

        // 딜레이 완료되고 공격 시작되면 공격
        if (isAttacking && timeSinceLastShot > WeaponHandler.Delay)
        {
            timeSinceLastShot = 0;
            Attack();
        }
        
        
    }

    protected virtual void Attack()
    {
        // 바라보는 위치지가 있을 때만(중앙 제외)
        if (lookDirection != Vector2.zero)
        {
            WeaponHandler.Attack();
        }   
    }
}