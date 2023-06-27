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

    [SerializeField] private List<GameObject> cars;
    public Car currentCar = null;

    public void OnPlayButton()
    {
        int i = 0;
        do
        {
            if (cars[i].activeSelf)
            {
                currentCar = cars[i].GetComponent<Car>();
            }

            i++;
        } while (currentCar == null);
        currentCar.currentHealth = currentCar.startHealth;
    }
    public void HPLoss()
    {
        currentCar.currentHealth--;
        healthUI.RemoveHeart();
    }
    public void StartInvincibility()
    {
        loseHealthUI.SetActive(true);
        StartCoroutine(InvincibilityCountdown());
    }
    IEnumerator InvincibilityCountdown()
    {
        currentCar.countdownTime -= 0.1f;
        loseHealthTimer.text = currentCar.countdownTime.ToString("0.0");
        if (currentCar.countdownTime <= 0)
        {

            loseHealthUI.SetActive(false);
            currentCar.countdownTime = 2f;
            yield break;
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(InvincibilityCountdown());
    }
    public void HealthPickUP()
    {
        if (currentCar.currentHealth < currentCar.startHealth)
        {
            currentCar.currentHealth++;
            healthUI.AddHeart();
        }
    }
    public void StopTheGame()
    {
        gameOverUI.SetActive(true);
        highScoreText.alpha = 1;
    }
}
