using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private Transform spawnContainer;
    [SerializeField] private GameController gameController;
    [SerializeField] private float minSpeed = 3;
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float minSpawnInterval = 1;
    [SerializeField] private float maxSpawnInterval = 2;
    [SerializeField] private int[] spawnLane;         // lanes for car spawns (1 = left, 2 = middle, 3 = right)
    [SerializeField] private float spawnIntervalPowerUp = 50f;
    private int laneIndexLast;
    private float spawnVectorY = 0.6f;
    private float spawnVectorZ = 14f;
    private float laneWidth = 2.25f;
    private Vector3 spawnScale;

    private void Start()
    {
        spawnScale = new Vector3(70, 70, 70);
        // Start spawning cars and power ups
        StartCoroutine(SpawnCar());
        StartCoroutine(SpawnPowerUp());
    }
    
    IEnumerator SpawnCar()
    {
        yield return new WaitForSeconds(GetRandomInterval());

        if (gameController.currentCar.currentHealth <= 0)
            yield break;

        // Choose a random lane to spawn the car in
        int laneIndex = Random.Range(0, spawnLane.Length);
        // If the new lane to spawn the car in is the same as the last, we choose a new lane until its different
        while (laneIndexLast == laneIndex)
        {
            laneIndex = Random.Range(0, spawnLane.Length);
        }

        int lane = spawnLane[laneIndex];
        laneIndexLast = laneIndex;

        // Choose a random speed for the car
        float speed = Random.Range(minSpeed, maxSpeed);

        // Spawn the car and set its position, rotation, scale and speed
        GameObject car = Instantiate(carPrefab, new Vector3((lane - 2) * laneWidth, spawnVectorY, spawnVectorZ), Quaternion.Euler(0f, 90f, 0f), spawnContainer);
        car.transform.localScale = spawnScale;
        car.GetComponent<SpawnMovement>().SetSpeed(speed);

        StartCoroutine(SpawnCar());
    }
    IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(spawnIntervalPowerUp);

        if (gameController.currentCar.currentHealth <= 0)
            yield break;

        int laneIndex = Random.Range(0, spawnLane.Length);
        int lane = spawnLane[laneIndex];
        float speed = 4f;
        GameObject powerUp = Instantiate(powerUpPrefab, new Vector3((lane - 2) * laneWidth, spawnVectorY, spawnVectorZ), Quaternion.Euler(0f, 90f, 0f), spawnContainer);
        powerUp.GetComponent<SpawnMovement>().SetSpeed(speed);

        StartCoroutine(SpawnPowerUp());
    }
    private float GetRandomInterval()
    {
        // Choose a random interval between the min and max spawn intervals
        return Random.Range(minSpawnInterval, maxSpawnInterval);
    }
}
