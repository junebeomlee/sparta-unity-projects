using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("Attack Info")]
    // 딜레이
    [SerializeField] private float delay = 1f;
    public float Delay { get => delay; set => delay = value; }
    
    // 캡슐화와 값 변경의 안전성을 유지하기 위해 사용하는 패턴
    // 나중에 코드 변경 시, 직접 weaponSize를 수정하는 대신 해당 프로퍼티에만 변경을 가하면 되므로 코드의 유지보수성
    [SerializeField] private float weaponSize = 1f;
    public float WeaponSize { get => weaponSize; set => weaponSize = value; }
    
    // 데미지
    [SerializeField] private float power = 1f;
    public float Power { get => power; set => power = value; }
    
    // 속도
    [SerializeField] private float speed = 1f;
    public float Speed { get => speed; set => speed = value; }

    [SerializeField] private float attackRange = 10f;
    public float AttackRange { get => attackRange; set => attackRange =value; }

    public LayerMask target;

    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }
    
    [SerializeField] private float knockbackPower = 0.1f;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }
    
    [SerializeField] private float knockbackTime = 0.5f;
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }
    
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    
    public BaseController Controller { get; private set; }
    
    private Animator animator;
    private SpriteRenderer weaponRenderer;

    protected virtual void Awake()
    {
        // 컴포넌트 ㅇ녀결
        Controller = GetComponentInParent<BaseController>();
        animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();
        
        animator.speed = 1.0f / delay; 
        transform.localScale = Vector3.one * weaponSize;
    }

    protected virtual void Start()
    {
        
    }
    
    public virtual void Attack()
    {
        AttackAnimation();
    }

    // 공격 애니메이션 트리거
    public void AttackAnimation()
    {
        animator.SetTrigger(IsAttack);
    }

    public virtual void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }
    
    
}
