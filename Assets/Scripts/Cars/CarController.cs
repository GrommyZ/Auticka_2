using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Car currentCar = null;
    private GameObject currentCarGO;

    [SerializeField] private GameObject sportsCarGO;
    [SerializeField] private GameObject truckGO;
    [SerializeField] private GameObject busGO;
    [SerializeField] private GameController gameController;

    private int currentLane = 2;      // The current lane the car is in (1 = left lane, 2 = middle lane, 3 = right lane)
    private readonly float laneWidth = 2.25f;
    private bool isChangingLane = false;
    private int targetLane = 2;

    public float countdownTime = 2f;

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
            CarChangeLane();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            currentCar.CarHonk();
        }
    }
    public void CarChangeLane()
    {
        float targetX = (targetLane - 2) * laneWidth;
        float newX = Mathf.MoveTowards(currentCarGO.transform.position.x, targetX, currentCar.laneChangeSpeed * Time.deltaTime);
        currentCarGO.transform.position = new Vector3(newX, currentCarGO.transform.position.y, currentCarGO.transform.position.z);

        // If the car has reached the target lane, stop changing lanes
        if (Mathf.Approximately(newX, targetX))
        {
            currentLane = targetLane;
            isChangingLane = false;
        }
    }
    public void OnPlayButtonClick()
    {
        if (currentCar == null) //if a player didnt specifically select any car with buttons, we assign sports car as a default car
        {
            OnSportsCarButtonClick();
        }
        currentCar.SetCarStats();
    }
    public void OnSportsCarButtonClick()
    {
        Car sportsCar = new SportsCar();
        currentCar = sportsCar;
        currentCarGO = sportsCarGO;
    }
    public void OnTruckButtonClick()
    {
        Car truckCar = new TruckCar();
        currentCar = truckCar;
        currentCarGO = truckGO;
    }
    public void OnBussButtonClick()
    {
        Car busCar = new BusCar();
        currentCar = busCar;
        currentCarGO = busGO;
    }

}
