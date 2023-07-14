using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartContainer;
    [SerializeField] private GridLayoutGroup layoutGroup;
    [SerializeField] private GameController gameController;
    [SerializeField] private RectTransform layoutGroupRecTransform;

    public void InstantiateHearts()
    {
        int health = gameController.carController.currentCar.startHealth;
        int numHearts = health;

        for (int i = 0; i < numHearts; i++)
        {
            AddHeart();
        }

    }
    public void RemoveHeart()
    {
        if (gameController.carController.currentCar.currentHealth >= 0)
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

        LayoutRebuilder.MarkLayoutForRebuild(layoutGroupRecTransform);
    }
}
