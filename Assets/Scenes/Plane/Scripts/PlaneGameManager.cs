using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneGameManager : MonoBehaviour
{
    static PlaneGameManager gameManager;

    public static PlaneGameManager Instance
    {
        get { return gameManager; }
    }
    
    private int currentScore = 0;
    
    private void Awake()
    {
        gameManager = this;
    }
    
    public void GameOver()
    {
        Debug.Log("Game Over");
    }
    
    public void RestartGame()
    {
        // 유니티의 씬 매니저
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int score)
    {
        currentScore += score;
                
        Debug.Log("Score: " + currentScore);
    }
    
}