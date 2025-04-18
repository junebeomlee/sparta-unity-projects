using System;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationTriggerType
{
    isGrounded,
}

// 이와 같은 형태를 가질 때, MonoBehaviour가 필요 없을 수도 있음
public class AnimationHandler: MonoBehaviour
{
    private Animator _animator;

    private static readonly Dictionary<int, int> Triggers = new();

    static AnimationHandler()
    {
        // notice: 박싱/언박싱 고려해서 개선하기
        foreach (AnimationTriggerType triggerType in Enum.GetValues(typeof(AnimationTriggerType)))
        {
            Triggers[(int)triggerType] = Animator.StringToHash(triggerType.ToString());
        }
    }

    public static int GetTrigger(AnimationTriggerType triggerType)
    {
        return Triggers[(int)triggerType];
    }

    // notice: animator를 숨기고 핸들러의 역할은 내부에서 동작한다.
    public void SetTrigger<T>(AnimationTriggerType triggerType, T value)
    {
        int hash = Triggers[(int)triggerType];

        if (typeof(T) == typeof(bool))
        {
            _animator.SetBool(hash, Convert.ToBoolean(value));
        }
        else if (typeof(T) == typeof(float))
        {
            _animator.SetFloat(hash, Convert.ToSingle(value));
        }
        else if (typeof(T) == typeof(int))
        {
            _animator.SetInteger(hash, Convert.ToInt32(value));
        }
        else if (typeof(T) == typeof(void))
        {
            _animator.SetTrigger(hash);
        }
    }
    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}