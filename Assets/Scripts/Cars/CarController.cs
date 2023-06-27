using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject loseHealthUI;
    [SerializeField] private TextMeshProUGUI loseHealthTimer;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private HealthManager healthUI;
    [SerializeField] private HighScoreManager highScoreManager;

    public int currentLane = 2;      // The current lane the car is in (1 = left lane, 2 = middle lane, 3 = right lane)
    public float laneWidth = 2.25f;
    public bool isChangingLane = false;
    public int targetLane = 2;

    public float countdownTime = 2f;

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
    private void Update()
    {
        // Move the car left or right based on input
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKeyDown(KeyCode.A))) && currentLane > 1 && !isChangingLane && currentCar.currentHealth > 0)
        {
            targetLane = currentLane - 1;
            isChangingLane = true;
        }
        if ((Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.D))) && currentLane < 3 && !isChangingLane && currentCar.currentHealth > 0)
        {
            targetLane = currentLane + 1;
            isChangingLane = true;
        }
        if (isChangingLane == true) // If the car is changing lanes, move it towards the target lane
        {
            currentCar.CarChangeLane();
        }
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
        countdownTime -= 0.1f;
        loseHealthTimer.text = countdownTime.ToString("0.0");
        if (countdownTime <= 0)
        {

            loseHealthUI.SetActive(false);
            countdownTime = 2f;
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
