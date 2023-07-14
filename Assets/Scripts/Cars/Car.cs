using UnityEngine;
public class Car
{
    public int startHealth;
    public int currentHealth;
    public float laneChangeSpeed;

    public virtual void CarHonk()
    {
        Debug.Log("Base car HONK!");
    }
    public virtual void SetCarStats()
    {

    }
}
