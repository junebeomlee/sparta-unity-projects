using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // static readonly를 통한 통일(해시를 통해 int로 변환하는 점 확인)
    // q: 왜 파라미터는 대문자일까?
    private static readonly int IsMoving = Animator.StringToHash("IsRun");
    private static readonly int IsDamage = Animator.StringToHash("IsHit");

    protected Animator animator;

    protected virtual void Awake()
    {
        // 하위 요소에서 찾기, 하위 요소가 여러개라면?
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);
    }

    public void Damage()
    {
        animator.SetBool(IsDamage, true);
    }

    public void InvincibilityEnd()
    {
        animator.SetBool(IsDamage, false);
    }
}