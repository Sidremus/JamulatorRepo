using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineFuel : MonoBehaviour
{
    private float fuel = 100f;
    private float lowPowerMultiplier = 0.5f;
    private float highPowerMultiplier = 1f;
    public float fuelConsumptionTickRate = 0.02f;

    private void Start() 
    {
        SubmarineState.Instance.fuel = fuel;
    }

    private void FixedUpdate() 
    {
        // Check for causes of fuel consumption
        CheckMoving();
    }

    private void CheckMoving()
    {
        // No need to continue if we aren't moving
        if (!SubmarineState.Instance.isMoving)
        {
            return;
        }

        // We're moving; with which power profile?
        float profileMultiplier = 
            SubmarineState.Instance.movePowerProfile == PowerProfile.LOW
            ? lowPowerMultiplier
            : highPowerMultiplier;

        // Consume fuel for moving
        SubmarineState.Instance.fuel -= profileMultiplier * fuelConsumptionTickRate;
    }
}


