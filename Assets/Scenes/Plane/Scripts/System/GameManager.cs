using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Plane
{
    public class GameManager : MonoBehaviour
    {
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void AddScore(int score)
        {
            currentScore += score;
            uiManager.UpdateScore(currentScore);
            Debug.Log("Score: " + currentScore);
        }
    
    }
}