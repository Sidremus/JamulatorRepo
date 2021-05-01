using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SubDriveSystem : MonoBehaviour
{
    //public Transform steeringStick;// Just for testing / visual feedback

    public float forwardSpeedLowEnergy = 5, forwardSpeedHighEnergy = 30;
    public float lateralSpeedLowEnergy = 5, lateralSpeedHighEnergy = 0;
    public float turningTorqueLowEnergy = 15, turningTorqueHighEnergy = 5;

    Rigidbody rb;
    Vector3 moveVec, torqueVec;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ConstructMovAndTorqueVectors();

        rb.AddRelativeForce(moveVec);
        rb.AddTorque(torqueVec * Time.fixedDeltaTime);
    }

    private void ConstructMovAndTorqueVectors()
    {
        moveVec = Vector3.zero;

        // Z-movement
        if (SubmarineState.Instance.zMoveState == Direction.FORWARDS)
        {
            moveVec.z = Mathf.Lerp(forwardSpeedLowEnergy, forwardSpeedHighEnergy, SubmarineState.Instance.driveEnergyLerp);
        }
        else if (SubmarineState.Instance.zMoveState == Direction.BACKWARDS)
        {
            // The -forwardSpeedHighEnergy is halved so you don't drive backwards at max cruise speed.
            moveVec.z = Mathf.Lerp(-forwardSpeedLowEnergy, -forwardSpeedHighEnergy / 2f, SubmarineState.Instance.driveEnergyLerp);
        }

        // X-movement
        if (SubmarineState.Instance.strafeState == Direction.LEFT)
        {
            moveVec.x = Mathf.Lerp(-lateralSpeedLowEnergy, -lateralSpeedHighEnergy, SubmarineState.Instance.driveEnergyLerp);
        }
        else if (SubmarineState.Instance.strafeState == Direction.RIGHT)
        {
            moveVec.x = Mathf.Lerp(lateralSpeedLowEnergy, lateralSpeedHighEnergy, SubmarineState.Instance.driveEnergyLerp);
        }

        // Y-Movement
        if (SubmarineState.Instance.yMoveState == Direction.UP)
        {
            moveVec.y = Mathf.Lerp(lateralSpeedLowEnergy, lateralSpeedHighEnergy, SubmarineState.Instance.driveEnergyLerp);
        }
        else if (SubmarineState.Instance.yMoveState == Direction.DOWN)
        {
            moveVec.y = Mathf.Lerp(-lateralSpeedLowEnergy, -lateralSpeedHighEnergy, SubmarineState.Instance.driveEnergyLerp);
        }

        // Yaw-Torque
        // This maps the mouse position to a steering wheel angle if SubmarineState is in steering mode.
        if (SubmarineState.Instance.interfaceMode == ControlMode.STEERING)
        {
            torqueVec.y = Mathf.Lerp(
                Mathf.Lerp(-turningTorqueLowEnergy, -turningTorqueHighEnergy, SubmarineState.Instance.driveEnergyLerp),
                Mathf.Lerp(turningTorqueLowEnergy, turningTorqueHighEnergy, SubmarineState.Instance.driveEnergyLerp),
                SubmarineController.mousePos.x);

            //steeringStick.localRotation = Quaternion.Euler(0, Mathf.Lerp(30, -30, SubmarineController.mousePos.x), 0);// Just for testing / visual feedback
        }
        //else torqueVec = Vector3.zero; // If commented out, torque will be applied even if the player lets go of the wheel
    }
}
