using UnityEngine;
using UnityEngine.SceneManagement;

public enum UIState
{
    Home,
    Game,
    Score,
}

namespace Scene.Stack
{
    public class UIManager : MonoBehaviour
    {
        // 각 게임의 매니저마다 중복으로 발생
        private readonly string _initScenePath = $"Scenes/{MiniGame.World}/_scene";
        
        static UIManager instance;
        public static UIManager Instance => instance;
    
        UIState currentState = UIState.Home;
        
        HomeUI homeUI = null;
    
        GameUI gameUI = null;
    
        ScoreUI scoreUI = null;
    
        TheStack theStack = null;
        private void Awake()
        {
            instance = this;
            theStack = FindObjectOfType<TheStack>();
            
            homeUI = GetComponentInChildren<HomeUI>(true);
            homeUI?.Init(this);
            gameUI = GetComponentInChildren<GameUI>(true);
            gameUI?.Init(this);
            scoreUI = GetComponentInChildren<ScoreUI>(true);
            scoreUI?.Init(this);
            
            ChangeState(UIState.Home);
        }
    
    
        // 상태 패턴
        public void ChangeState(UIState state)
        {
            currentState = state;
            homeUI?.SetActive(currentState);
            gameUI?.SetActive(currentState);
            scoreUI?.SetActive(currentState);
        }
        
        public void OnClickStart()
        {
            theStack.Restart();
            ChangeState(UIState.Game);
        }
    
        public void OnClickExit()
        {
            // 시작 씬으로 이동
            World.GameManager.Instance.ExitGame();
            
            // #if UNITY_EDITOR
            //         UnityEditor.EditorApplication.isPlaying = false;
            // #else
            //         Application.Quit(); // 어플리케이션 종료
            // #endif
        }
        
        public void UpdateScore()
        {
            gameUI.SetUI(theStack.Score, theStack.Combo, theStack.MaxCombo);
        }
        
        public void SetScoreUI()
        {
            scoreUI.SetUI(theStack.Score, theStack.MaxCombo, theStack.BestScore, theStack.BestCombo);
        
            ChangeState(UIState.Score);
        }
    }
}
