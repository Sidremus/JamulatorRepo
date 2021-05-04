using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMeshColliders : MonoBehaviour
{
    [SerializeField]
    bool DisableColliders;
    // Start is called before the first frame update
    void Start()
    {
        if (DisableColliders)
        {
            var meshColliders = GameObject.FindObjectsOfType<MeshCollider>();
            foreach (var collider in meshColliders)
            {
                collider.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
