using UnityEngine;

public class MantaAI : BaseFishAI
{
    public Transform rotatePoint;
    public float speed = 10f;

    // Setup: z rotation should be -25
    // move target rotate point child object where you like
    // assign speed accordling
    // to change direction, rotate on y by 180

    private void FixedUpdate() {
        transform.RotateAround(rotatePoint.position, Vector3.up, speed * Time.deltaTime);
    }
}
