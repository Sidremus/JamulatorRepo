using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteIfInaudible : MonoBehaviour
{
    // credit to John French for the idea and some of the code //
    [SerializeField] AudioSource source;
    public bool sourceStopped;

    AudioSourcePlayer player;
    AudioListener listener;
    float distance;
    bool hasPlayer;
    bool wasPlaying;
    bool wasLooping;
    

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
        StopSourceIfFar();
    }

    void StopSourceIfFar()
    {
        
        distance = Vector3.Distance(transform.position, listener.transform.position);
        if (distance > source.maxDistance)
        {
            // outside of maxDistance: stop
            if (hasPlayer)
            {
                wasLooping = player.loop;
                player.loop = false;
                source.Stop();
            }
            else
            {
                wasPlaying = source.isPlaying;
                wasLooping = source.loop;
                source.loop = false;
                source.Stop();
                
            }

            sourceStopped = true;
        }
        else
        {
            // within MaxDistance: restart
            if (hasPlayer)
            {
                if (wasLooping)
                    player.PlayLoopWithInterval();
            }
            else
            {
                if (wasPlaying)
                {
                    source.Play();
                }
                source.loop = wasLooping;

            }

            sourceStopped = false;
        }

        
    }


}
