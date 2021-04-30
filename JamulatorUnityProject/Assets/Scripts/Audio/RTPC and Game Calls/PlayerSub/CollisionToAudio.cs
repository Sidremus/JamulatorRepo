using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionToAudio : MonoBehaviour
{    
    // TODO: need to work out how/whether it can be routed throught the event system //

    void OnCollisionEnter(Collision collision)
    {
        float impactMagnitude = collision.relativeVelocity.magnitude;
        Vector3 position = collision.contacts[0].point;
        AudioManager.Instance.PlayCollisionSound(position, impactMagnitude);

        Debug.Log("Collision! at position: " + position + " with an impactMagnitude of " + impactMagnitude);

        EventManager.Instance.NotifyOfSubCollision(collision);

    }



}
