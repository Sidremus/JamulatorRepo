using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionToAudio : MonoBehaviour
{

    Collision collision;

    private void OnEnable()
    {
        EventManager.Instance.onSubCollision += ProcessCollision;
    }

    void ProcessCollision()
    {




    }



    private void OnDisable()
    {
        EventManager.Instance.onSubCollision -= ProcessCollision;
    }
    





}
