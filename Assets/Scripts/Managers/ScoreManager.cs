using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private CarController carController;
    [SerializeField] private HighScoreManager highScoreManager;
    public int score = 0;

  public void OnPlayButtonClick()
    {
        StartCoroutine(GameScore());
    }
   IEnumerator GameScore()
    {
        if (carController.currentCar.currentHealth == 0)
        {
            highScoreManager.UpdateHighScore();
            yield break;
        }
        score++;
        scoreText.text = "Score: " + score.ToString("F0");
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(GameScore());
    }
}
