using UnityEngine;

// 발사체
public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangeWeaponHandler rangeWeaponHandler;
    
    private float currentDuration;
    private Vector2 direction;
    private bool isReady;
    private Transform pivot;
    
    // 컴포넌트 묶어서 처리
    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;
    private ProjectileManager projectileManager;

    public bool fxOnDestory = true;
    
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0);
    }

    private void Update()
    {
        if (!isReady) 
        { 
            return;
        }

        currentDuration += Time.deltaTime;

        if(currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position, false);
        }

        _rigidbody.linearVelocity = direction * rangeWeaponHandler.Speed;
    }

    // 충돌 시 삭제
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 쉬프트 연산자로 레이어 구분
        if(levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestory);
        }
        else if(rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }
    }

    // 초기화 후,
    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, ProjectileManager projectileManager)
    {
        this.projectileManager = projectileManager;
        
        rangeWeaponHandler = weaponHandler;
        
        this.direction = direction;
        currentDuration = 0;
        transform.localScale = Vector3.one * weaponHandler.BulletSize;
        spriteRenderer.color = weaponHandler.ProjectileColor;

        transform.right = this.direction;

        // 발사체에 따른 피봇의 방향
        if (this.direction.x < 0)
        {
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        }
        else
        {
            pivot.localRotation = Quaternion.Euler(0, 0, 0);
        }

        isReady = true;
    }
    
    // 파괴 시 애니메이션 발생
    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        // 충돌 파티클 애니메이션
        if (createFx)
        {
            projectileManager.createImpactParticleSystem(position, rangeWeaponHandler);
        }
        
        Destroy(this.gameObject);
    }
}
