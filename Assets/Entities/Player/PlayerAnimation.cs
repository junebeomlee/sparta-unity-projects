using System;
using System.Collections.Generic;
using UnityEngine;

// 애니메이션 관리를 위한 퍼사드 클래스일 뿐 큰 역할을 가지지 않도록 한다.
public class PlayerAnimation: MonoBehaviour
{
    [HideInInspector] public Animator animator;

    public enum AnimParam
    {
        IsJumping,
        IsWalking
    }

    private static readonly Dictionary<AnimParam, int> Hashes = new();

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        foreach (AnimParam param in Enum.GetValues(typeof(AnimParam)))
        {
            Hashes[param] = Animator.StringToHash(param.ToString());
        }
    }

    public Animator GetAnimator() => animator;
    public static int GetHash(AnimParam param) => Hashes[param];
}