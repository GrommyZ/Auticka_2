using UnityEngine;

public class SpawnMovement : MonoBehaviour
{
    private float speed;

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.right);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
