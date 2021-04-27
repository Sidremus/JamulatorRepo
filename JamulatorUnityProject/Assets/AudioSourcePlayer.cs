using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;

    [SerializeField] bool loop;
    [SerializeField] float intervalBetweenPlays;

    private AudioSource source;
    private float clipDuration;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        if (clips != null)
            clipDuration = source.clip.length;

        if (loop)
            LoopClip();

    }

    void LoopClip()
    {
        StartCoroutine(AudioUtility.WaitIntervalThenPlay(source, AudioUtility.RandomClipFromArray(clips), intervalBetweenPlays));
    }



}
