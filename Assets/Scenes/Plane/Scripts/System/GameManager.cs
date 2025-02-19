using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Plane
{

    public class GameManager : MonoBehaviour
    {
        // 게임을 다시 호출할 때 (원인은 비동기 때문인지?) 이전 진입한 씬이 currentActiceScene으로 인식됨
        private readonly string _currentGameScenePath = $"Scenes/{MiniGame.Plane}/_scene";
        private readonly string _initScenePath = $"Scenes/{MiniGame.World}/_scene";

        static GameManager gameManager;

        public static GameManager Instance
        {
            get { return gameManager; }
        }
    
        private int currentScore = 0;
        UIManager uiManager;

        public UIManager UIManager
        {
            get { return uiManager; }
        }
        private void Awake()
        {
            gameManager = this;
            uiManager = FindObjectOfType<UIManager>();
        }
    
        private void Start()
        {
            uiManager.UpdateScore(0);
        }
    
        public void GameOver()
        {
            Debug.Log("Game Over");
            uiManager.SetRestartPage();
        }
    
        public void RestartGame()
        {
            SceneManager.LoadScene(_currentGameScenePath);
        }

        public void ExitGame()
        {
            SceneManager.LoadScene(_initScenePath);
        }

        public void AddScore(int score)
        {
            currentScore += score;
            uiManager.UpdateScore(currentScore);
            Debug.Log("Score: " + currentScore);
        }
    
    }
}