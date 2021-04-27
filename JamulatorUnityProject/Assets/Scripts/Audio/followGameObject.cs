using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followGameObject : MonoBehaviour
{
    [SerializeField] Transform gameobjectToFollow;

    private void Update()
    {
        if (gameobjectToFollow != null)
            transform.position = gameobjectToFollow.position;
    }

}
