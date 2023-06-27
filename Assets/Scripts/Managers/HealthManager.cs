using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartContainer;
    [SerializeField] private GridLayoutGroup layoutGroup;
    public CarController carController;

    public void InstantiateHearts()
    {
        int health = carController.currentCar.startHealth;
        int numHearts = health;

        for (int i = 0; i < numHearts; i++)
        {
            Instantiate(heartPrefab, heartContainer);
        }

        layoutGroup.enabled = false;
        layoutGroup.enabled = true;
    }
    public void RemoveHeart()
    {
        if (carController.currentCar.currentHealth >= 0)
        {
            int heartCount = heartContainer.childCount;
            if (heartCount > 0)
            {
                Destroy(heartContainer.GetChild(heartCount - 1).gameObject);
            }
        }
    }

    public void AddHeart()
    {
        Instantiate(heartPrefab, heartContainer);

        layoutGroup.enabled = false;
        layoutGroup.enabled = true;
    }
}
