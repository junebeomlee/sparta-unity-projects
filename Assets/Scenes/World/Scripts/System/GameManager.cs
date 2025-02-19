
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene.World
{
    // 다른 게임 우선 진행하게 되는 경우, 생성이 되지 않아 접근할 수 없는 문제 발생
    public class GameManager: MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance;
        
        private readonly string _initScenePath = $"Scenes/{MiniGame.World}/_scene";

        private void Awake()
        {
            // 씬 밖의 영역에서 싱글톤으로 존재
            if(_instance) { _instance = this; DontDestroyOnLoad(_instance); }
            else { Destroy(gameObject); }
        }

        public void ExitGame()
        {
            SceneManager.LoadScene(_initScenePath);
        }
    }
}