using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSourceController))]
public class StopAudioIfInaudible : MonoBehaviour
{
    // credit to John French for the idea and some of the code //
    public bool sourceStopped;

    private AudioSource source;
    private AudioSourceController controller;
    private AudioListener listener;

    float distanceFromListener;
    bool wasLooping;
    bool wasFading;
    float fadeTargetWhenStopped;
    float fadeTimeWhenStopped;


    private void Start()
    {
        controller = GetComponent<AudioSourceController>();
        listener = AudioManager.Instance.listener.GetComponent<AudioListener>();        
        source = controller.audioSource;

    }


    private void LateUpdate()
    {
        StopSourceIfFar();
    }

    void StopSourceIfFar()
    {
        distanceFromListener = Vector3.Distance(transform.position, listener.transform.position);

        if (distanceFromListener > source.maxDistance) // outside of maxDistance: stop
        {            
            if (sourceStopped) return; // blocks repeats

            // log params
            wasLooping = controller.loopClips;
            wasFading = controller.isFading;

            if (wasFading)
            {
                fadeTargetWhenStopped = controller.currentFadeTarget;
                fadeTimeWhenStopped = controller.currentFadeTime;
            }


            // fades down and stops (prevents clips if spatial blend is less than 1)
            float fadeOutTime = 1f;

            controller.StopLooping(fadeOutTime);
            controller.FadeTo(AudioUtility.min, fadeOutTime, 0.5f, true);

            sourceStopped = true;

            //Debug.Log(this + "on " + gameObject.name + ": audio stopped, because it is " + distanceFromListener + " away from the listener object. When back in range, will fade up to " + fadeTargetWhenStopped);
        }
        else // within distance: restart
        {
            if (!sourceStopped) return; // only restart clips that have been stopped by this script.
            // within MaxDistance: restart

            if (wasLooping) // restart the loop
            {
                controller.PlayLoopWithInterval();
                //Debug.Log(this + "on " + gameObject.name + ": audio restarted, because it is " + distanceFromListener + " away from the listener object, and loopclip = true. Fading up to " +fadeTargetWhenStopped);
            }

            if (!wasFading) // sets default vals
            {
                fadeTargetWhenStopped = 0f;
                fadeTimeWhenStopped = 1f;
            }

            controller.FadeTo(fadeTargetWhenStopped, fadeTimeWhenStopped, 0.5f, false); // continue the fade where it left off

            sourceStopped = false;
        }


    }

}

