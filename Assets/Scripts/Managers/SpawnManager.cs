using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private Transform spawnContainer;
    [SerializeField] private GameController gameController;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpawnInterval;
    [SerializeField] private float maxSpawnInterval;
    [SerializeField] private float spawnIntervalPowerUp;
    private float carSpeed;
    private float powerUpSpeed = 4f;
    private int laneLast;
    private float spawnVectorY = 0.6f;
    private float spawnVectorZ = 14f;
    private float laneWidth = 2.25f;
    private int spawnLaneMin = 1;         // lanes for car spawns (1 = left, 2 = middle, 3 = right)
    private int spawnLaneMax = 4;
    private Vector3 spawnScaleVector;
    private int spawnScale = 70;

    private void Start()
    {
        spawnScaleVector = new Vector3(spawnScale, spawnScale, spawnScale);
        // Start spawning cars and power ups
        StartCoroutine(SpawnCar());
        StartCoroutine(SpawnPowerUp());
    }
    
    IEnumerator SpawnCar()
    {
        yield return new WaitForSeconds(GetRandomInterval());

        if (gameController.carController.currentCar.currentHealth <= 0)
            yield break;

        // Choose a random lane to spawn the car in
        int lane = Random.Range(spawnLaneMin, spawnLaneMax);
        // If the new lane to spawn the car in is the same as the last, we choose a new lane until its different
        while (laneLast == lane)
        {
            lane = Random.Range(spawnLaneMin, spawnLaneMax);
        }

        laneLast = lane;

        // Choose a random speed for the car
        carSpeed = Random.Range(minSpeed, maxSpeed);

        // Spawn the car and set its position, rotation, scale and speed
        GameObject car = Instantiate(carPrefab, new Vector3((lane - 2) * laneWidth, spawnVectorY, spawnVectorZ), Quaternion.Euler(0f, 90f, 0f), spawnContainer);
        car.transform.localScale = spawnScaleVector;
        car.GetComponent<SpawnMovement>().SetSpeed(carSpeed);

        StartCoroutine(SpawnCar());
    }
    IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(spawnIntervalPowerUp);

        if (gameController.carController.currentCar.currentHealth <= 0)
            yield break;

        int lane = Random.Range(spawnLaneMin, spawnLaneMax);
        GameObject powerUp = Instantiate(powerUpPrefab, new Vector3((lane - 2) * laneWidth, spawnVectorY, spawnVectorZ), Quaternion.Euler(0f, 90f, 0f), spawnContainer);
        powerUp.GetComponent<SpawnMovement>().SetSpeed(powerUpSpeed);

        StartCoroutine(SpawnPowerUp());
    }
    private float GetRandomInterval()
    {
        // Choose a random interval between the min and max spawn intervals
        return Random.Range(minSpawnInterval, maxSpawnInterval);
    }
}
