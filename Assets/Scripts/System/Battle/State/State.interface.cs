using UnityEngine;

public abstract class IBehaviour : MonoBehaviour
{
    public StateMachine stateMachine;
    public abstract void Enter();
}

// 랜덤으로 돌아다닌다.
public class EnemyPatrolState
{
    // 시아 안에 들어오면 detectionState로 변경
    public int detectionRange = 5;

    public void Start()
    {
        // 랜덤 이동우 범위 체크 후 타겟 지정 후, Detection으로 변경
    }
}

// Detection이 되면 타겟
public class EnemyDetectionState
{

    // 타겟 다시 체크 후 없으면 Patrol로 변경
    public void Start()
    {
        
    }
}