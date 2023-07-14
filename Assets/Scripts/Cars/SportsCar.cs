using UnityEngine;
public class SportsCar : Car
{
    public override void CarHonk()
    {
        Debug.Log("HONK HONK!");
    }
    public override void SetCarStats()
    {
        startHealth = 3;
        laneChangeSpeed = 6;
    }
}
