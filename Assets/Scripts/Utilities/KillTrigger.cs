using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car") || other.CompareTag("PU_Health"))
        {
            Destroy(other.gameObject);
        }
    }
}
