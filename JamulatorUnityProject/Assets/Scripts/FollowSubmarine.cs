using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSubmarine : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position = SubmarineState.Instance.submarine.transform.position;
    }
}
