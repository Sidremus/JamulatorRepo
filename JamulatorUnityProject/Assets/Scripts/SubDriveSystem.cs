using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SubDriveSystem : MonoBehaviour
{
    public float forwardSpeedLowEnergy = 5, forwardSpeedHighEnergy = 30;
    public float lateralSpeedLowEnergy = 5, lateralSpeedHighEnergy = 0;
    public float turningTorqueLowEnergy = 15, turningTorqueHighEnergy = 5;

    public Transform mapBoundsCenter; 
    public float offMapPushForce = 5f;

    Rigidbody rb;
    Vector3 moveVec, torqueVec;

    private float mouseDeadzone = 0.05f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ConstructMovAndTorqueVectors();

        if (SubmarineState.Instance.outOfBounds) {
            MoveInBounds();
        } else {
            rb.AddRelativeForce(moveVec);
            rb.AddRelativeTorque(torqueVec * Time.fixedDeltaTime);
        }


        Level();
    }

    private void MoveInBounds() {
        GameObject sub = SubmarineState.Instance.submarine;
        // Push player back towards map
        Vector3 mapDir = (mapBoundsCenter.position - sub.transform.position);

        Rigidbody rb = sub.GetComponent<Rigidbody>();
        rb.AddRelativeForce(mapDir * offMapPushForce);
    }

    private void Level()
    {
        GameObject sub = SubmarineState.Instance.submarine;

        if (sub.transform.rotation.z == 0f)
        {
            return;
        } 

        sub.transform.rotation = Quaternion.Euler(sub.transform.eulerAngles.x, sub.transform.eulerAngles.y, 0f);
    }

    private void Turn()
    {
        Vector2 mouse = SubmarineController.mousePos;
        GameObject sub = SubmarineState.Instance.submarine;
        // Mouse values are 0-1, where 0.5 is the middle, account for deadzone
        float xOffset = mouse.x;
        int dir;
        float turnSpeed;
        float leftThreshold = 0.5f - mouseDeadzone;
        float rightThreshold = 0.5f + mouseDeadzone;

        if (xOffset < leftThreshold) 
        {
            dir = -1;
            turnSpeed = leftThreshold - xOffset;
        } else if (xOffset > rightThreshold) 
        {
            dir = 1;
            turnSpeed = xOffset - rightThreshold;
        } else {
            dir = 0;
            turnSpeed = 0f;
        }

        float rotateVal = dir * turnSpeed * Time.deltaTime * 40f;
        sub.transform.Rotate(0f, rotateVal, 0f, Space.Self);
    }

    private void Pitch()
    {
        Vector2 mouse = SubmarineController.mousePos;
        GameObject sub = SubmarineState.Instance.submarine;

        float yOffset = mouse.y;
        int dir;
        float turnSpeed;
        float leftThreshold = 0.5f - mouseDeadzone;
        float rightThreshold = 0.5f + mouseDeadzone;

        if (yOffset < leftThreshold) 
        {
            dir = -1;
            turnSpeed = leftThreshold - yOffset;
        } else if (yOffset > rightThreshold) 
        {
            dir = 1;
            turnSpeed = yOffset - rightThreshold;
        } else {
            dir = 0;
            turnSpeed = 0f;
        }

        float rotateVal = dir * turnSpeed * Time.deltaTime * 40f;
        sub.transform.Rotate(rotateVal, 0f, 0f, Space.Self);
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

            torqueVec.x = Mathf.Lerp(
                Mathf.Lerp(turningTorqueLowEnergy, turningTorqueHighEnergy, SubmarineState.Instance.driveEnergyLerp),
                Mathf.Lerp(-turningTorqueLowEnergy, -turningTorqueHighEnergy, SubmarineState.Instance.driveEnergyLerp),
                SubmarineController.mousePos.y
            );
        }
    }
}
