using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene.Plane
{

    public class GameManager : MonoBehaviour
    {
        // 게임을 다시 호출할 때 (원인은 비동기 때문인지?) 이전 진입한 씬이 currentActiceScene으로 인식됨
        private readonly string _currentGameScenePath = $"Scenes/{MiniGame.Plane}/_scene";
        private readonly string _initScenePath = $"Scenes/{MiniGame.World}/_scene";

        private static GameManager _gameManager;
        public static GameManager Instance => _gameManager; 
    
        private int _currentScore = 0;
        UIManager uiManager;

        public UIManager UIManager
        {
            get { return uiManager; }
        }
        private void Awake()
        {
            _gameManager = this;
            uiManager = FindObjectOfType<UIManager>();
            Cursor.visible = true;
        }
    
        private void Start()
        {
            uiManager.UpdateScore(0);
        }
    
        public void GameOver()
        {
            uiManager.ShowResultUI();

            
            // 최고 기록 저장
            GameScores gameScores = World.GameManager.Instance.GameScores;
            if (gameScores.planeMaxScore < _currentScore)
            {
                gameScores.planeMaxScore = _currentScore;
            }
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
            _currentScore += score;
            uiManager.UpdateScore(_currentScore);
        }
    
    }
}