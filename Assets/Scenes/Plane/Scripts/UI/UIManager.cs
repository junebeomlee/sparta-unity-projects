using TMPro;
using UnityEngine;

namespace Scenes.Plane
{
    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI restartText;

        public void Start()
        {
            if (!restartText || !scoreText) { Debug.LogError("text is null"); return; }
            restartText.gameObject.SetActive(false);
        }

        public void SetRestartPage()
        {
            restartText.gameObject.SetActive(true);
        }

        public void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }

    }
}