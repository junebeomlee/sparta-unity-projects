using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ConversationUIManager : MonoBehaviour
{
    // 대화의 시작됨 파악
    public bool _isConversationStarted = false;
    private bool _isProceedToNext = false;  // 타이핑 중에는 다음으로 넘어갈 수 없음

    Text Title;
    Text Description;
    
    // q: 싱글톤 외 다른 방법은 없을까?
    public static ConversationUIManager Instance;
    
    private void Awake()
    {
        // 이미 인스턴스가 있는지 확인, 없으면 생성
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            // 싱글톤이 아닌 곳에 붙은 경우 삭제
            Destroy(gameObject);
        }
    }

    // 처음 Active 상태가 false이면 시작되지 않음
    void Start()
    {
        // do: 이름으로 찾는 방식이 위험
        Title = transform.Find("Title").GetComponent<Text>();
        Description = transform.Find("Description").GetComponent<Text>();
        gameObject.SetActive(false);
    }

    // 대화 내용 갱신(NPC 이름 부분에 대해서 생각 필요)
    // [Obsolete("Obsolete")]
    public void UpdateConversationContent(string[] description, string title = null)
    {
        if (!gameObject.active) { gameObject.SetActive(true); }
        // if (title != null) { Title.text = title; }
        // Description.text = description;
        
        // 타이핑 효과를 위해, 코루틴 형태로 변경
        // StopCoroutine(TypingSentence(description));
        // 모든 코루틴 중지 -> 정상 동작, stopCoroutine이 잘되지 않은 점 확인해보기.

        if (_isConversationStarted) return;
        
        _isConversationStarted = true;
        StopAllCoroutines();
        StartCoroutine(TypingSentence(description));
    }
    
    // 한글자씩 보여주기 위해서 코루틴
    IEnumerator TypingSentence(string[] context)
    {
        Description.text = "";  // 기존 대사를 초기화
        _isProceedToNext = false;  // 타이핑 중에는 다음으로 넘어갈 수 없음

        // 등록은 한번만 하되, 문맥이 끝나면 다음 클릭을 기다린다.
        foreach (string sentence in context)
        {
            Description.text = "";  // 전 문장 초기화
            foreach (char letter in sentence)
            {
                Description.text += letter;  // 한 글자씩 추가
                yield return new WaitForSeconds(0.1f);  // 0.1초 간격으로 타이핑 효과
            }
            yield return new WaitForSeconds(0.5f);  // 한 문장의 끝
        }
        // 대화 완료 후 해제.
        // 동작은 하나, 대화를 기다려야 함.
        _isConversationStarted = false;
        gameObject.SetActive(false);
        // _isProceedToNext = true;
    }

    // 어디든 클릭 자체를 인식한다.
    private void Update()
    {
        // UpdateConversationContent가 실행되는 단계에서 동시에 실행됨
        if (Input.GetMouseButtonUp(0) && ConversationUIManager.Instance._isConversationStarted && _isProceedToNext)
        {
            Debug.Log("대화중입니다만");
        }
    }
}
