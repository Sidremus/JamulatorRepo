using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineCollisionListener : MonoBehaviour
{
    [SerializeField] float damageScale = 1f;


    void OnCollisionEnter(Collision collision)
    {
        EventManager.Instance.NotifyOfSubCollision(collision); 

        // changes damage based on material
        if (collision.gameObject.tag == "Plant")
            damageScale *= 0.2f;
        else if (collision.gameObject.tag == "Rock")
            damageScale *= 2f;
        else if (collision.gameObject.tag == "HardFauna")
            damageScale *= 0.8f;
        else if (collision.gameObject.tag == "SoftFauna")
            damageScale *= 0.5f;

        float impactMagnitude = collision.relativeVelocity.magnitude;
        Debug.Log(impactMagnitude);

        SubmarineState.Instance.subDamage += impactMagnitude * damageScale;

        Vector3 position = collision.contacts[0].point;
        AudioManager.Instance.PlayCollisionSound(position, impactMagnitude, SubmarineState.Instance.gameObject);
        AudioManager.Instance.PlayCollisionSound(position, impactMagnitude, collision.gameObject);
        // two collisions: one for the sub, one for whatever it's hit

    }
}
