using UnityEngine;

public class SpawnMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.right);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
