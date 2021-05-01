using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionToAudio : MonoBehaviour
{
    private void Start()
    {
        EventManager.Instance.onSubCollision += ProcessCollision;
    }


    void ProcessCollision(Collision collision)
    {
        float impactMagnitude = collision.relativeVelocity.magnitude;
        Vector3 position = collision.contacts[0].point;

        Debug.Log("Collision! at position: " + position + " with an impactMagnitude of " + impactMagnitude);

        AudioManager.Instance.PlayCollisionSound(position, impactMagnitude);

    }

    private void OnDisable()
    {
        EventManager.Instance.onSubCollision -= ProcessCollision;

    }



}
