using System;
using System.Collections.Generic;
using UnityEngine;

// battle system
/// <summary>
/// 쉬운 몬스터는 무작정 가까운 대상에게 달려든다.
/// 방패를 든 적은 방어를 하다 때를 노린다. (총이라면 리로드 타임을 노린다.)
/// 고스트는 은신 스킬을을 가진다.
/// 장애물이 있으면 그곳에 숨는다.(그렇다면 맵을 매우 경사지게 만든다.  
/// </summary>
public class NavigationHandler: MonoBehaviour
{
    public List<Transform> targets;

    private void Update()
    {
        var target = targets[0];

        var currDistance = Vector3.Distance(transform.position, target.position);
    }
}