using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyVelocityToAudio : MonoBehaviour
{

    [SerializeField] AudioManager manager;
    [SerializeField] GameObject submarine;
    Rigidbody rb;

    public float velocityX;
    public float velocityY;
    public float velocityZ;
   
    void Start()
    {
        rb = submarine.GetComponent<Rigidbody>();
    }

    void Update()
    {
        velocityX = rb.velocity.x;
        velocityY = rb.velocity.y;
        velocityZ = rb.velocity.z;
    }
}
