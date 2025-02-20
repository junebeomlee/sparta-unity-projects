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
            SceneManager.LoadScene(scenePath);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isTriggerEnter = true;
        }
    }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isTriggerEnter = false;
        }
    }
}
