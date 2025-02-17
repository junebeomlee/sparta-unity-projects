using UnityEngine;

public class EnemyController : BaseController
{
    private EnemyManager enemyManager;
    // 애너미 입장에선 타겟이 정해져 있음
    private Transform target;
    
    [SerializeField] private float followRange = 15f;
    
    public void Init(EnemyManager enemyManager, Transform target)
    {
        this.enemyManager = enemyManager;
        this.target = target;
    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    protected override void HandleAction()
    {
        base.HandleAction();

        if (!WeaponHandler || !target)
        {
            if(!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;            
            return;
        }

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        isAttacking = false;
        if (distance <= followRange)
        {
            lookDirection = direction;
            
            if (distance <= WeaponHandler.AttackRange)
            {
                int layerMaskTarget = WeaponHandler.target;
                
                // 2d에선 어떻게 발사하는 지?
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, WeaponHandler.AttackRange * 1.5f,
                    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                // 충돌 확인 후, 레이어 체크
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