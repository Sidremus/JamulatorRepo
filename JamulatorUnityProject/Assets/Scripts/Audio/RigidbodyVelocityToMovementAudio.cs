using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyVelocityToMovementAudio : MonoBehaviour
{

    [SerializeField] AudioManager manager;
    GameObject submarine;
    Rigidbody rb;

    public Vector3 velocityVector;
    [SerializeField] bool UseInternalControl;

    void Start()
    {
        submarine = manager.submarine;
        rb = submarine.GetComponent<Rigidbody>();        
    }

    void Update()
    {
        if (!UseInternalControl)
        {
            velocityVector = rb.velocity;
        }

        
    }
}
