using System.Linq;
using UnityEngine;

public interface IProjectile
{
    public AttributeType AttributeType { get; }

    public void OnHit(GameObject currentTarget);
}

public class ThunderProjectile : MonoBehaviour, IProjectile
{
    public int bounceCount = 3;

    public AttributeType AttributeType => AttributeType.Thunder;

    public void OnHit(GameObject currentTarget)
    {
        // 현재 적은 제외해야 함
        var nearEnemies = Physics.OverlapSphere(transform.position, 5f, LayerMask.GetMask(LayerType.Enemy.ToString()));
     

        var nextTarget = nearEnemies
            .Where(enemy => enemy.transform != currentTarget.transform)
            .OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position))
            .FirstOrDefault();
        
        // projectile manager에게 알림
        if (!nextTarget)
        {
            Destroy(gameObject);
            return;
        }

        // 즉시 전의
        transform.position = nextTarget.transform.position; // do: 제귀로 반복
    }
}

// 슬로우로 인한 속도 변화량과 확률 계산은 총알의 역할, 스턴 상태가 되는 것은 컨틀로러 역할 
public class IceProjectile : MonoBehaviour, IProjectile
{
    public AttributeType AttributeType => AttributeType.Ice;

    // 상태를 건드리는 로직은 몬스터의 컨트롤러의 역할
    public void OnHit(GameObject currentTarget)
    {
        var isSlowed = Random.Range(0, 100) <= 40; // do: 확률 시스템 필요 
        // currentTarget.GetComponent<Controller>() // 어디서 작성하는 것이 유지 보수성이 좋을까?
    }
}