using Scene.Plane;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene.Fishing
{
    public class GameManager: MonoBehaviour
    {
        // 중복 코드 발생
        private readonly string _initScenePath = $"Scenes/{MiniGame.World}/_scene";

        
        private static GameManager _gameManager; // static 변수로 인스턴스 유지
        // static 프로퍼티로 싱글톤 인스턴스를 반환
        public static GameManager Instance => _gameManager;


        private void Awake()
        {
            if (!_gameManager) { _gameManager = this;}
            else { Destroy(gameObject); }
        }
        
        // 모든 매니저에서 발생하므로 전역 게임 매니저에게 요청 (동작 안됨, 원인 찾아보기)
        public void ExitGame()
        {
            // 빌드 인덱스 숫자로 접근 가능한지 확인해보기
            SceneManager.LoadScene(_initScenePath);
            // World.GameManager.Instance.ExitGame();
        }
    }
}