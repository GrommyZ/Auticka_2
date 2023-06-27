using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public int startHealth;
    public int currentHealth;
    public float laneChangeSpeed;

    [SerializeField] private GameController gameController;

    private int currentLane = 2;      // The current lane the car is in (1 = left lane, 2 = middle lane, 3 = right lane)
    private float laneWidth = 2.25f;
    private bool isChangingLane = false;
    private int targetLane = 2;

    public float countdownTime = 2f;

    private void Update()
    {
        // Move the car left or right based on input
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKeyDown(KeyCode.A))) && currentLane > 1 && !isChangingLane && currentHealth > 0)
        {
            targetLane = currentLane - 1;
            isChangingLane = true;
        }
        if ((Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.D))) && currentLane < 3 && !isChangingLane && currentHealth > 0)
        {
            targetLane = currentLane + 1;
            isChangingLane = true;
        }
        if (isChangingLane == true) // If the car is changing lanes, move it towards the target lane
        {
            CarChangeLane();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car") && countdownTime == 2f) //HP LOSS
        {
            // Lose health and start invincibility timer
            gameController.HPLoss();

            if (currentHealth <= 0) // 0 HP - Stop the game and update high score
            {
                gameController.StopTheGame();
            }

            if (currentHealth > 0) // more then 0 HP - Start countdown of invincibility
            {
                gameController.StartInvincibility();
            }
        }

        if (other.CompareTag("PU_Health")) //HEALTH PICK UP
        {
            gameController.HealthPickUP();
            Destroy(other.gameObject);
        }
    }
    public void CarChangeLane()
    {
        float targetX = (targetLane - 2) * laneWidth;
        float newX = Mathf.MoveTowards(transform.position.x, targetX, laneChangeSpeed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // If the car has reached the target lane, stop changing lanes
        if (Mathf.Approximately(newX, targetX))
        {
            currentLane = targetLane;
            isChangingLane = false;
        }
    }

}
