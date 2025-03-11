using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Canvas))]
public class UIManager : Manager
{
    // refactor: dictionary의 경우  SerializeField 되어야 인스펙터에 노출되나, gameObject의 name으로 호출 가능하여 생략.
    public List<GameObject> pages;
    
    // fix: 데이터 바인딩이 안되어 일단 public으로 전달 중
    [HideInInspector] public GameObject currentPage;

    public void TurnOffAll()
    {
        pages.ForEach(page => page.SetActive(false));
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void Disable(string pageName)
    {
        transform.Find(pageName).gameObject.SetActive(false);
    }
    
    // feat: 하나씩 on/off 관리할 것이지? 일괄처리할 것인지.
    // ReSharper disable Unity.PerformanceAnalysis
    public void Show(string pageName)
    {
        TurnOffAll();
        
        Transform instance = transform.Find(pageName);
        // flow: 없을 경우 자식 요소로 생성
        if (!instance)
        {
            GameObject prefab = pages.Find(page => page.name == pageName);
            currentPage = Instantiate(prefab, transform);
            // fix: 이름이 인식되지 않는 문제 발생
            currentPage.name = pageName;
            currentPage.SetActive(true);
            
            return;
        }
        // flow: 있다면 enable
        instance.gameObject.SetActive(true);
        currentPage = instance.gameObject;
    }
}
