using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteIfInaudible : MonoBehaviour
{
    // credit to John French for the idea and some of the code //
    [SerializeField] bool paused;
    [SerializeField] AudioSource source;
    AudioSourcePlayer player;
    AudioListener listener;
    float distance;

    bool hasPlayer;
    

    private void Start()
    {
        source = GetComponent<AudioSource>();
        listener = AudioManager.Instance.listener.GetComponent<AudioListener>();

        if (gameObject.GetComponent<AudioSourcePlayer>() != null)
        {
            player = GetComponent<AudioSourcePlayer>();
            hasPlayer = true;
        }
            
        else
        {
            hasPlayer = false;
        }
    }
           

    private void Update()
    {
        distance = Vector3.Distance(transform.position, listener.transform.position);

        if (hasPlayer)
        {
            if (distance <= source.maxDistance)            
                PlayIfNotPlaying(player);
                      
            else            
                StopIfPlaying(player);
            
        }
        else
        {
            if (distance <= source.maxDistance)
                ToggleAudioSource(true);
            else
                ToggleAudioSource(false);
        }

        


    }

    void ToggleAudioSource(bool isAudible)
    {
        if (!isAudible && source.isPlaying)
            source.Stop();
        else if (isAudible && !source.isPlaying)
            source.Play();

        paused = !isAudible;
    }


    private void PlayIfNotPlaying(AudioSourcePlayer source)
    {
        if (source.clipPlaying)
            return;
        else
            source.PlayLoopWithInterval();

    }
    private void StopIfPlaying(AudioSourcePlayer source)
    {
        if (source.clipPlaying)
            source.loop = false;


    }

}
