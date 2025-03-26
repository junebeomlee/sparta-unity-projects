using Scene.World;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject ScoreView;
    public Button ScoreViewShowButton;
    public Button ScoreViewCloseButton;
    public Text ScoreText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if(!ScoreView || !ScoreViewShowButton || !ScoreViewCloseButton) { Debug.LogError("cannot found start or exit button"); return; }
        
        ScoreViewShowButton.onClick.AddListener(() => ShowScoreView(true));
        ScoreViewCloseButton.onClick.AddListener(() => ShowScoreView(false));
    }

    public void ShowScoreView(bool state)
    {
        GameScores gameScores = GameManager.Instance.GameScores;
        
        ScoreText.text = $"플라잉 비행기: {gameScores.planeMaxScore.ToString()}점\n스택 쌓기 : {gameScores.stackMaxScore.ToString()}점";
        ScoreView.SetActive(state);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
