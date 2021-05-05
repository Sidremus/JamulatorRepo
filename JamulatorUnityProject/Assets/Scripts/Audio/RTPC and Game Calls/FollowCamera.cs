using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    Camera mainCam;

    private void OnEnable()
    {
        mainCam = Camera.main;
        transform.position = mainCam.transform.position;
    }

    private void Update()
    {
        transform.position = mainCam.transform.position;
    }

}
