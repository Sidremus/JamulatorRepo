using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineCollisionListener : MonoBehaviour
{
    [SerializeField] float damageScale = 1f;


    void OnCollisionEnter(Collision collision)
    {
        //EventManager.Instance.NotifyOfSubCollision(collision);

        // changes damage based on material
        if (collision.gameObject.tag == "Plant")
            damageScale *= 0.2f;
        else if (collision.gameObject.tag == "Rock")
            damageScale *= 2.5f;
        else if (collision.gameObject.tag == "HardFauna")
            damageScale *= 0.8f;
        else if (collision.gameObject.tag == "SoftFauna")
            damageScale *= 0.4f;
        else if (collision.gameObject.tag == "Wreck")
            damageScale *= 2f;

        float impactMagnitude = collision.relativeVelocity.magnitude;
        SubmarineState.Instance.subDamage += impactMagnitude * damageScale;
        
        Vector3 position = collision.contacts[0].point;

        //Debug.Log("SubmarineCollisionListener: collision with " + collision.gameObject.name + ", with tag " + collision.gameObject.tag + " at position " + position + ", magnitude " + impactMagnitude);

        AudioManager.Instance.PlayCollisionSound(position, impactMagnitude, SubmarineState.Instance.gameObject);
        AudioManager.Instance.PlayCollisionSound(position, impactMagnitude, collision.gameObject);
        // two collisions: one for the sub, one for whatever it's hit

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Plant")
        {
            float magnitude = GetComponent<Rigidbody>().velocity.magnitude / 2f;
            Vector3 position = transform.position;

            AudioManager.Instance.PlayCollisionSound(position, magnitude, other.gameObject);
        }

    }
}
