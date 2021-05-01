using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class WanderFishAI : BaseFishAI
{
    protected float moveSpeed = 2.5f;
    protected float minMoveSpeed = 2.5f;
    private Vector3 targetDirection;
    private float lastTurned = 0f;
    private float turnTimeout = 8f;
    private bool canCheckCols = false;
 
    private void OnEnable() 
    {
        targetDirection = transform.forward;
        canCheckCols = Random.Range(0, 1) > 0.5;   
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
        if (targetDir.normalized == transform.forward.normalized) return;
        
        // Turn speed relative to current speed
        float turnSpeed = Mathf.Ceil(moveSpeed * 0.5f);
        float step = turnSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.1f);

        transform.rotation = Quaternion.LookRotation(newDir);
    }

    private void SwimForwards()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.fixedDeltaTime, Space.Self);
    }

    // Will only set a new target direction if about to collide with something
    private void SetCollisionDirection()
    {
        // Only raycast every other frame
        canCheckCols = !canCheckCols;
        if (!canCheckCols) return;
        

        // TODO use a layer mask
        float rayCastLength = 1f;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayCastLength))
        {            
            Vector3 reflect = Vector3.Reflect(hit.point - transform.position, hit.normal);

            Turn(reflect);
        }
    }

    // Will only set a new random target direction if haven't turned in a while
    private void SetWanderDirection()
    {
        if (lastTurned < turnTimeout) return;

        Vector2 rng = Random.insideUnitCircle.normalized;
        // Lower the y by a random range amount (leaning towards downward direction)
        float Ymin = 0.25f;
        float Ymax = -0.75f;
        float y = Random.Range(Ymin, Ymax);

        Vector3 newDir = new Vector3(rng.x, y, rng.y);

        Turn(newDir);
    }

    private void Turn(Vector3 turnDir)
    {
        GetComponent<FishMoveSound>().TriggerRipple();

        // Set new target facing direction
        targetDirection = turnDir;

        // Reset turn timer
        lastTurned = 0f;

        // Assign new speed til next turn
        moveSpeed = minMoveSpeed * Random.Range(0.8f, 1.2f);

        // How long til fish gets bored?
        turnTimeout = Random.Range(3f, 8f);
    }
}
