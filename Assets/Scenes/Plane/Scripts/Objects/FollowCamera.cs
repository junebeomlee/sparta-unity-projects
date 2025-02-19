using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// todo: 자연스럽게 따라오는 것은 어떨까?(루퍼에도 영향을 줌)
public class FollowCamera : MonoBehaviour
{
    public Transform target;
    float offsetX;

    void Start()
    {
        if (!target) {return;}
        // 간격
        offsetX = transform.position.x - target.position.x;
    }

    void Update()
    {
        if (!target) {return;}
        // 현 위치 파악
        Vector3 pos = transform.position;
        pos.x = target.position.x + offsetX;
        // 위치 변경
        transform.position = pos;
    }
}