using UnityEngine;

public class EnemyController : BaseController
{
    private EnemyManager enemyManager;
    // 애너미 입장에선 타겟이 정해져 있음
    private Transform target;
    
    // 추적 거리
    [SerializeField] private float followRange = 15f;
    
    // 초기화 - 어디서 해야하나?
    public void Init(EnemyManager enemyManager, Transform target)
    {
        this.enemyManager = enemyManager;
        this.target = target;
    }

    // 공격 대상과의 거리 계산
    protected float DistanceToTarget()
    {
        // 해당 메소드 알아보기
        return Vector3.Distance(transform.position, target.position);
    }

    protected override void HandleAction()
    {
        base.HandleAction();

        // 무기도 대상도 없으면
        if (!WeaponHandler || !target)
        {
            // 정위치?
            if(!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;            
            return;
        }

        // 거리 계산
        float distance = DistanceToTarget();
        // 방향 계산
        Vector2 direction = DirectionToTarget();

        isAttacking = false;
        if (distance <= followRange)
        {
            lookDirection = direction;
            
            if (distance <= WeaponHandler.AttackRange)
            {
                // 공격할 수 잇는 대상의 레이어 마스크..?
                int layerMaskTarget = WeaponHandler.target;
                
                // 2d에선 어떻게 발사하는 지?
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, WeaponHandler.AttackRange * 1.5f,
                    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                // 충돌 확인 후, 레이어 체크
                // 공격 대상이 있음이 확인 되면 발사체 발사 시작
                if (hit.collider && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    isAttacking = true;
                }
                
                movementDirection = Vector2.zero;
                return;
            }
            
            movementDirection = direction;
        }

    }

    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }
    
}