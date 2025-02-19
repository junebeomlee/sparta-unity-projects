using UnityEngine;
using UnityEngine.SceneManagement;

public class ConversationMiniGame : MonoBehaviour
{
    private bool _isTriggerEnter = false;
    // 인스펙터에서 설정할 씬 이름
    [SerializeField] private MiniGame miniGameName;

    private void Start()
    {
        // 콜라이더 여부 체크
        if (!this.GetComponent<Collider2D>()) { Debug.LogError($"Missing collider: {this.gameObject.name}"); }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isTriggerEnter)
        {
            string scenePath = $"Scenes/{miniGameName.ToString()}/_scene";
            Debug.Log($"현재 씬: {SceneManager.GetActiveScene().name}, 이동할 씬: {miniGameName}");
            
            SceneManager.LoadScene(scenePath);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isTriggerEnter = true;
            Debug.Log("플레이어가 트리거에 진입");
        }
    }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isTriggerEnter = false;
            Debug.Log("플레이어가 트리거에서 나감");
        }
    }
}
