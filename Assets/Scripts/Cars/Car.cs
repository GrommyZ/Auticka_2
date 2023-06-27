using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public int startHealth;
    public int currentHealth;
    public float laneChangeSpeed;
    [SerializeField] private CarController carController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car") && carController.countdownTime == 2f) //HP LOSS
        {
            // Lose health and start invincibility timer
            carController.HPLoss();

            if (currentHealth <= 0) // 0 HP - Stop the game and update high score
            {
                carController.StopTheGame();
            }

            if (currentHealth > 0) // more then 0 HP - Start countdown of invincibility
            {
                carController.StartInvincibility();
            }
        }

        if (other.CompareTag("PU_Health")) //HEALTH PICK UP
        {
            carController.HealthPickUP();
            Destroy(other.gameObject);
        }
    }
    public void CarChangeLane()
    {
        float targetX = (carController.targetLane - 2) * carController.laneWidth;
        float newX = Mathf.MoveTowards(transform.position.x, targetX, laneChangeSpeed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // If the car has reached the target lane, stop changing lanes
        if (Mathf.Approximately(newX, targetX))
        {
            carController.currentLane = carController.targetLane;
            carController.isChangingLane = false;
        }
    }

}
