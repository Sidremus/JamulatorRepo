using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteIfInaudible : MonoBehaviour
{
    // credit to John French for the idea and some of the code //
    [SerializeField] bool paused;
    [SerializeField] AudioSource source;
    AudioListener listener;
    float distance;

    

    private void Start()
    {
        source = GetComponent<AudioSource>();
        listener = AudioManager.Instance.listener.GetComponent<AudioListener>();
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, listener.transform.position);

        if (distance <= source.maxDistance)
            ToggleAudioSource(true);
        else 
            ToggleAudioSource(false);


    }

    void ToggleAudioSource(bool isAudible)
    {
        if (!isAudible && source.isPlaying)
            source.Pause();
        else if (isAudible && !source.isPlaying)
            source.Play();

        paused = !isAudible;
    }


}
