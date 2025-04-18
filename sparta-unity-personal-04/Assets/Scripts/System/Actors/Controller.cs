using System;
using UnityEngine;

public class Controller: MonoBehaviour
{
    private ResourceHandler _resourceHandler;
    private RewardHandler _rewardHandler;
    public StateMachine stateMachine;
    
    public float coolTime = 3f;
    private float _currCoolTime = 0f;

    private void Awake()
    {
        _resourceHandler = GetComponent<ResourceHandler>();

        // if (!GetComponent<RewardHandler>())
        // {
            _rewardHandler = GetComponent<RewardHandler>();
        // }
    }

    private void Update()
    {
        _currCoolTime += Time.deltaTime;
        if (_currCoolTime >= coolTime)
        {
            // Debug.Log("cool time interval");
            _currCoolTime = 0f;
        }
    }
    
    // 각 상태에 대해서
    public void ChangeStatus(int damage, AttributeType attributeType)
    {
        
    }
    
    public void OnCollisionEnter(Collision other)
    {
    }

    // 외부에서의 접근은 Controller를 통해서만 발생한다.
    public void SetDamage(int amount)
    {
        _resourceHandler.Modify(StatType.Health, -amount);
        // GlobalManager.GetManager<ParticleManager>().playOneShot("");
    }

    // 플레이어와 몬스터 로직 충돌
    public void Die()
    {
        var (gold, item) = _rewardHandler.GetReward();
        GlobalManager.GetManager<InventoryManager>().ModifyGold(gold);

        if (!_resourceHandler) return;
    }
}