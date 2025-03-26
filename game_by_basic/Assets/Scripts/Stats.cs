using System;
using UnityEngine;
using UnityEngine.Serialization;

// 데이터 클래스 : JSON과 연결될 수 있음
// 인스펙터 노출을 위해서 MonoBehaviour 붙음
// learn: 만약 기본 타입이 아닌 경우, 직렬화하면 인스펙터에서 표시 가능
[Serializable]
public class Stats : MonoBehaviour
{
    // why: 레벨 또한 스탯으로 관리하는 점에 대해서
    public float Level;
    public float Health;
    public float Power;
    public float Speed;
}