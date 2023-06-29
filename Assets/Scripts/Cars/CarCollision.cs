using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Car") && gameController.carController.countdownTime == 2f) //HP LOSS
        {
            // Lose health and start invincibility timer
            gameController.HPLoss();

            if (gameController.carController.currentCar.currentHealth <= 0) // 0 HP - Stop the game and update high score
            {
                gameController.StopTheGame();
            }

            if (gameController.carController.currentCar.currentHealth > 0) // more then 0 HP - Start countdown of invincibility
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
}
