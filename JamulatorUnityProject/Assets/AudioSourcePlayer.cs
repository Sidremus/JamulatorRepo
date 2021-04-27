using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePlayer : MonoBehaviour
{
    //[SerializeField] AudioClip[] clips;
    [SerializeField] List<AudioClip> clips = new List<AudioClip>();



    [SerializeField] bool loop;
    [SerializeField] float intervalBetweenPlays;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        if (clips.Count == 0)
            clips.Add(source.clip);

        else if (clips.Count == 0 && source.clip == null)
            Debug.LogError("Audiosourceplayer on " + this.gameObject.name + ": no clips set in either audiosourceplayer or audiosource");
        else
            source.clip = AudioUtility.RandomClipFromList(clips);



        if (loop)
            PlayLoopWithInterval(intervalBetweenPlays);
        

    }


    public void PlayLoopWithInterval()
    {
        loop = true;
        LoopClip(intervalBetweenPlays);
    }
    public void PlayLoopWithInterval(float interval)
    {
        loop = true;
        LoopClip(interval);
    }



    void LoopClip(float interval)
    {
        StartCoroutine(AudioUtility.WaitIntervalThenPlay(source, AudioUtility.RandomClipFromList(clips), interval));
    }



}
