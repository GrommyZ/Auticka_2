using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject loseHealthUI;
    [SerializeField] private TextMeshProUGUI loseHealthTimer;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private HealthManager healthUI;
    [SerializeField] private HighScoreManager highScoreManager;
    public CarController carController;

    public void OnPlayButton()
    {
        carController.currentCar.currentHealth = carController.currentCar.startHealth;
    }
    public void HPLoss()
    {
        carController.currentCar.currentHealth--;
        healthUI.RemoveHeart();
    }
    public void StartInvincibility()
    {
        loseHealthUI.SetActive(true);
        StartCoroutine(InvincibilityCountdown());
    }
    IEnumerator InvincibilityCountdown()
    {
        carController.countdownTime -= 0.1f;
        loseHealthTimer.text = carController.countdownTime.ToString("0.0");
        if (carController.countdownTime <= 0)
        {

            loseHealthUI.SetActive(false);
            carController.countdownTime = 2f;
            yield break;
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(InvincibilityCountdown());
    }
    public void HealthPickUP()
    {
        if (carController.currentCar.currentHealth < carController.currentCar.startHealth && carController.currentCar.currentHealth > 0)
        {
            carController.currentCar.currentHealth++;
            healthUI.AddHeart();
        }
    }
    public void StopTheGame()
    {
        gameOverUI.SetActive(true);
        highScoreText.alpha = 1;
    }
}
