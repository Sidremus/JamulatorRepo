using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Behaviour affects speed at which fish moves
enum Behaviour
{
    PASSIVE,
    FLEEING
}

public class FishAI : MonoBehaviour
{
    public float passiveMoveSpeed = 2.5f;

    private Vector3 targetDirection;
    private float lastTurned = 0f;
    private float turnTimeout = 8f;
 
    private void Start() 
    {
        targetDirection = transform.forward;    
    }

    private void FixedUpdate() 
    {
        // Increase timers
        lastTurned += Time.deltaTime;

        // Face the target direction
        FaceTarget(targetDirection);

        // Move towards target
        SwimForwards();

        // Might have to turn to avoid collisions
        SetCollisionDirection();

        // Start wandering if haven't turned around in a while
        SetWanderDirection();
    }

    private void FaceTarget(Vector3 targetDir)
    {
        // Stop here if already facing target direction
        if (targetDir.normalized == transform.forward.normalized) {
            return;
        }

        // Turn speed relative to current speed
        float turnSpeed = Mathf.Ceil(passiveMoveSpeed * 0.5f);
        float step = turnSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDir);
    }

    private void SwimForwards()
    {
        transform.Translate(Vector3.forward * passiveMoveSpeed * Time.fixedDeltaTime, Space.Self);
    }

    // Will only set a new target direction if about to collide with something
    private void SetCollisionDirection()
    {
        // TODO use a layer mask
        float rayCastLength = 1f;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayCastLength))
        {            
            Vector3 reflect = Vector3.Reflect(hit.point - transform.position, hit.normal);

            targetDirection = reflect;

            lastTurned = 0f;
        }
    }

    // Will only set a new random target direction if haven't turned in a while
    private void SetWanderDirection()
    {
        // Only wander if fish hasn't turned in a while
        if (lastTurned < turnTimeout) 
        {
            return;
        }

        // Get a target point to travel to
        // More likely to keep going forward
        float dir = Random.Range(0, 1) > 0.7 ? 1 : -1;
        Vector3 targetPoint = transform.position + (dir * transform.forward);

        // Lower the y by a random range amount (leaning towards downward direction)
        float Ymin = 0.25f;
        float Ymax = 1f;
        targetPoint.y -= Random.Range(Ymin, Ymax);

        // Adjust x position by a random range amount
        float Xmin = -2f;
        float Xmax = 2f;
        targetPoint.x += Random.Range(Xmin, Xmax);

        // Get the direction towards the new target
        Vector3 targetDir = targetPoint - transform.position;

        // Set the new direction
        targetDirection = targetDir;

        // Reset turn timeout
        lastTurned = 0f;
    }


}
