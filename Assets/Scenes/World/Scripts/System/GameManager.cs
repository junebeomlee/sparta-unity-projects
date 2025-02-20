
using System;
using System.Reflection;
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
        
        // 기록 저장
        private const string _maxScoresKey = "GameMaxScores";
        public  GameScores GameScores;

        private void Awake()
        {
            // 씬 밖의 영역에서 싱글톤으로 존재
            if(!_instance) { 
                _instance = this; 
                DontDestroyOnLoad(_instance);
                // 맨 처음에 한번 불러오기
                Debug.Log("가장 먼저 생성되");
                _instance.GameScores = LoadScores();
                if (_instance.GameScores == null)
                {
                    Debug.LogError("GameScores 로딩 실패: 기본값을 설정합니다.");
                    GameScores = new GameScores();  // 기본값 설정
                }
                else
                {
                    Debug.Log("로딩 성공");
                }
            }
            // else { Destroy(gameObject); }
        }
        public void ExitGame()
        {
            SceneManager.LoadScene(_initScenePath);
        }
        
        // 각 미니 게임 최고 점수 저장(직렬화 방식이라 부적합 수 있음)

        // 점수 불러오기 
        public GameScores LoadScores()
        {
            // 만약 없다면?
            string json = PlayerPrefs.GetString(_maxScoresKey, "{}");
            // 없는 경우 초기화
            GameScores loadedScores = JsonUtility.FromJson<GameScores>(json) ?? new GameScores();
            return loadedScores;
        }
        
        // 게임 종료시 한번에 저장
        private void OnApplicationQuit()
        {
            Debug.Log("종료 될 때 저장");
            // save games sore
            string json = JsonUtility.ToJson(GameScores);
            PlayerPrefs.SetString(_maxScoresKey, json);
            PlayerPrefs.Save();
        }
    }
}