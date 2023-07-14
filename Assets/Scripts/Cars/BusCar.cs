using UnityEngine;
public class BusCar : Car
{
    public override void CarHonk()
    {
        Debug.Log("TUUUUUUUUUUUUU!");
    }
    public override void SetCarStats()
    {
        startHealth = 10;
        laneChangeSpeed = 3.5f;
    }
}
