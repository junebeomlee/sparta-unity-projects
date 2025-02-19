using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scenes.Plane
{
    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI ScoreText;
        
        public TextMeshProUGUI RestartText;
        public Button ExitButton; 

        public void Start()
        {
            if (!RestartText || !ScoreText) { Debug.LogError("text is null"); return; }
            RestartText.gameObject.SetActive(false);
            ExitButton.gameObject.SetActive(false);
        }

        public void SetRestartPage()
        {
            RestartText.gameObject.SetActive(true);
            ExitButton.gameObject.SetActive(true);
        }

        public void UpdateScore(int score)
        {
            ScoreText.text = score.ToString();
        }

    }
}