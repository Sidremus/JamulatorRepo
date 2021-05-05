using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToAudiosourceDistance : MonoBehaviour
{
    AudioSource source;

    private void Awake()
    {
        float scale = Mathf.Clamp(this.transform.parent.transform.localScale.y, 0.1f, 7f); // uses y because y not.
        source = GetComponent<AudioSource>();
        source.maxDistance *= scale;
        source.minDistance *= scale;
        Debug.Log("Scaled " + gameObject.name + "'s distance factor to " + scale + "x its original");
    }
}
