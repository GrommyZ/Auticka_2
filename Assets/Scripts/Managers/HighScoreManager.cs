using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private ScoreManager scoreManager;
    private float highScore;

    private void Start()
    {
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        highScoreText.text = "High Score: " + highScore.ToString("F0");
    }
    public void UpdateHighScore()
    {
        if (scoreManager.score > highScore)
        {
            highScore = scoreManager.score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            highScoreText.text = "New High Score: " + highScore.ToString("F0");
        }
    }
}
