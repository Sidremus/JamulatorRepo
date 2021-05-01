using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineCollisionListener : MonoBehaviour
{
    [SerializeField] float damageScale = 1f;


    void OnCollisionEnter(Collision collision)
    {
        EventManager.Instance.NotifyOfSubCollision(collision);

        float impactMagnitude = collision.relativeVelocity.magnitude;
        SubmarineState.Instance.subDamage += impactMagnitude * damageScale;

        Vector3 position = collision.contacts[0].point;
        AudioManager.Instance.PlayCollisionSound(position, impactMagnitude);

    }
}
