using UnityEngine;
public class TruckCar : Car
{
    public override void CarHonk()
    {
        Debug.Log("GET OUT OF THE WAY!");
    }
    public override void SetCarStats()
    {
        startHealth = 5;
        laneChangeSpeed = 5;
    }
}
