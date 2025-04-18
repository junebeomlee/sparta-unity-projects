using UnityEngine;

public class MeleeEnemyPatrolState : IBehaviour
{
    public override void Enter()
    {
        stateMachine = GetComponent<MeleeEnemyStateMachine>();
        
        // 현재 좌표에서 이동 가능 범위 계산 시작
    }

    

}