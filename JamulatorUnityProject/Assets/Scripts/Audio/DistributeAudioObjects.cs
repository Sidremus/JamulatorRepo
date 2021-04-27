using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Randomly distributes prefabs in a given dropArea gameobject (takes scale values of a reference gameobject, eg. with a collider). Intended for looping stems of larger ambiences.
/// </summary>

public class DistributeAudioObjects : MonoBehaviour
{
    [SerializeField] AudioClip clipToDistribute;
    [SerializeField] int numberOfSoundsToDistribute;
    [SerializeField] GameObject audioObject;
    [SerializeField] GameObject dropArea;
    Vector3 areaSize;

    [SerializeField] float pitchOffset;
    [SerializeField] float randomPitchRange;

    [SerializeField] bool PlayOnAwake = false;

    public List<GameObject> createdAudioObjects = new List<GameObject>();


    private void Start()
    {
        areaSize = dropArea.transform.localScale;
        

        for (int i = 0; i < numberOfSoundsToDistribute; ++i)
        {
            Vector3 randPos =  areaSize/2 + new Vector3(Random.Range(-areaSize.x / 2, areaSize.x / 2),
                                            Random.Range(-areaSize.y / 2, areaSize.y / 2),
                                            Random.Range(-areaSize.z / 2, areaSize.z / 2));

            randPos += dropArea.transform.position;

            var go = Instantiate<GameObject>(audioObject, this.transform, true);
            go.transform.position = randPos;
            go.name = this.gameObject.name + "-" + i;

            createdAudioObjects.Add(go);

            var audiosource = go.GetComponent<AudioSource>();
            audiosource.clip = clipToDistribute;
            audiosource.pitch += pitchOffset + Random.Range(-randomPitchRange, randomPitchRange);                       
            audiosource.time = audiosource.clip.length * Random.Range(0, 1);

            if (PlayOnAwake)
                audiosource.Play();
        }

        this.gameObject.name = this.gameObject.name + "(x" + numberOfSoundsToDistribute + ")";

    }

}
