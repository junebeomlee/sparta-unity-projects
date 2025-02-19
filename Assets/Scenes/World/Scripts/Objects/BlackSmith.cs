using System;
using UnityEngine;

// NPC 클래스로 변경하기
public class BlackSmith : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    // 2D 콜라이더가 붙은 뒤로 동작이 됨
    private void OnMouseDown()
    {
        Debug.Log("Mouse Down");

        // 배열로 넣어서 context로 만드는 게 낫지 않을까?
        // string[] 과 같은 문법에 대해서 C#에서 다루는 방식이 다름
        ConversationUIManager.Instance.UpdateConversationContent(new string[]{$"안녕하신가? 무기 상점에 온 것을 환영한다네.", $"대화를 한번만 하는 사람이 어디있나?"});

    }

    public void OnClick()
    {
        Debug.Log("Click BlackSmith");
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 태그가 문자열로 
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어랑 부딪힘");
        }
    }
}
