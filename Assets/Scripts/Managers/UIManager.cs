using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

// CanvasController를 두는 방식은 어떨까 ? - observeElement 스크립트를 찾은 뒤 버튼인지 텍스트인지 체크
public class UIManager : MonoBehaviour
{
    
    public enum PageType { Settings, Inventory, }

    [System.Serializable]
    public class Page
    {
        public PageType type; public GameObject page;
        public Page(PageType type, GameObject page) { this.type = type; this.page = page; }
    }
    
    public List<Page> pageList;
    private List<Page> _openedPages = new();

    private void Start()
    {
    }
    
    // private void Awake()
    // {
    //     GlobalManager.RegisterManager(gameObject);        
    // }

    // void Start()
    // {
    //     _uiPrefabPool = new (
    //         createFunc: CreateUI,
    //         10,
    //         20
    //     )
    // }

    // notice : nested - 창을 한번 더 뛰우고 싶다면? 
    public void Open(PageType type)
    {
        var selectedPage = _openedPages.Find(page => page.type == type);
        if (selectedPage == null)
        {
            var currPage = pageList.Find(page => page.type == type);
            _openedPages.Add(new Page(currPage.type, currPage.page));
            
            var instance = Instantiate(currPage.page, transform, true);
            instance.SetActive(true);
        }
    }

    public void Close(PageType type)
    {
    }

    //
    public void UpdateCurrPage()
    {
        
    }
}