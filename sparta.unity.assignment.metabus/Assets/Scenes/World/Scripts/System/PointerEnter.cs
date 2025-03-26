using System;
using UnityEngine;

public class PointerEnter : MonoBehaviour
{
    // 마우스 오버는 올려놓은 동안, 엔터는 일회성이다.
    private SpriteRenderer _spriteRenderer;
    private Color _defaultColor;

    private void Start()
    {
        // 메인 스프라이트에 직접 넣는 것이 인식 안됨.
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _defaultColor = _spriteRenderer.color;
    }

    private void OnMouseEnter()
    {
        _spriteRenderer.color = Color.red;
    }

    private void OnMouseExit()
    {
        _spriteRenderer.color = _defaultColor;
    }
}
