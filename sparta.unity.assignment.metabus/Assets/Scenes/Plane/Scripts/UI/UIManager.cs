using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scene.Plane
{
    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI ScoreText;
        
        public Button RestartButton;
        public Button ExitButton; 

        public void Start()
        {
            if (!RestartButton || !ScoreText) { Debug.LogError("text is null"); return; }
            
            RestartButton.onClick.AddListener(GameManager.Instance.RestartGame);
            ExitButton.onClick.AddListener(GameManager.Instance.ExitGame);
            
            RestartButton.gameObject.SetActive(false);
            ExitButton.gameObject.SetActive(false);
        }

        public void ShowResultUI()
        {
            RestartButton.gameObject.SetActive(true);
            ExitButton.gameObject.SetActive(true);
        }

        public void UpdateScore(int score)
        {
            ScoreText.text = score.ToString();
        }

    }
}