using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateAudioPrefab : MonoBehaviour
{
    [SerializeField] GameObject prefabToInstantiate;

    private void Start()
    {
        var newAudioObject = Instantiate<GameObject>(prefabToInstantiate, this.transform);
    }

}
