using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToAudiosourceDistance : MonoBehaviour
{
    AudioSource source;
    [SerializeField] float scaleMaximum = 5f;


    private void Awake()
    {
        float scale = Mathf.Clamp(this.transform.parent.transform.localScale.y, 0.1f, scaleMaximum); // uses y because y not.
        source = GetComponent<AudioSource>();
        source.maxDistance *= scale;
        source.minDistance *= scale;
    }
}
