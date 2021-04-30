using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionToAudio : MonoBehaviour
{    
    // TODO: need to work out how/whether it can be routed throught the event system //


    private void OnEnable()
    {
        //EventManager.Instance.onSubCollision += ProcessCollision;
    }

    void OnCollisionEnter(Collision collision)
    {
        ProcessCollision(collision);       

    }

    void ProcessCollision(Collision collision)
    {
        float impactMagnitude = collision.relativeVelocity.magnitude;
        Vector3 position = collision.contacts[0].point;
        AudioManager.Instance.PlayCollisionSound(position, impactMagnitude);

    }



    private void OnDisable()
    {
        //EventManager.Instance.onSubCollision -= ProcessCollision;
    }
    





}
