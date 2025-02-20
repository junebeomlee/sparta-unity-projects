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
        // 배열로 넣어서 context로 만드는 게 낫지 않을까?
        // string[] 과 같은 문법에 대해서 C#에서 다루는 방식이 다름
        ConversationUIManager.Instance.UpdateConversationContent(
            new string[]
            {
                $"그대는 스파르타 정신이 부족하다. 이 곳에서 수련하라.", 
                $"X키를 누르면 투구를 장착하거나, 말을 탑승할 수 있으며 속도가 증가된다.",
                $"오른쪽 고블린들 근처에서 스페이스를 누르면 2가지 미니 게임에 각각 접속할 수 있다."
            }
            );

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
