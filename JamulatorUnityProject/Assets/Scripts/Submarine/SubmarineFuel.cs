using UnityEngine;

public class SubmarineFuel : MonoBehaviour
{
    public float driveFuelCost = 0.02f;

    private float maxFuel = 100f;
    
    private void Start() 
    {
        SubmarineState.Instance.fuel = maxFuel;
    }

    private void FixedUpdate() 
    {
        // Check for causes of fuel consumption
        CheckDriving();
    }

    private void CheckDriving()
    {
        // No need to continue if we aren't moving
        if (!SubmarineState.Instance.isMoving)
        {
            return;
        }


        // Consume fuel for moving - multiply by the current power applied to drive systems
        SubmarineState.Instance.fuel -= SubmarineState.Instance.driveEnergyLerp * driveFuelCost;
    }
}


