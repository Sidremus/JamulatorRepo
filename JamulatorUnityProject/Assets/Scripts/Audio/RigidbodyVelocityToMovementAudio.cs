using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyVelocityToMovementAudio : MonoBehaviour
{

    [SerializeField] AudioManager manager;
    [SerializeField] GameObject submarine;
    Rigidbody rb;

    public float maxVelocity; // find this out

    public float velocityX;
    public float velocityY;
    public float velocityZ;

    [SerializeField] bool UseInternalControl;

    void Start()
    {
        rb = submarine.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!UseInternalControl)
        {
            velocityX = rb.velocity.x;
            velocityY = rb.velocity.y;
            velocityZ = rb.velocity.z;
        }

        
    }
}
