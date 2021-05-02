using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePlayer : MonoBehaviour
{
    //[SerializeField] AudioClip[] clips;
    [SerializeField] List<AudioClip> clips = new List<AudioClip>();

    [SerializeField] public bool loop;
    [SerializeField] public bool clipPlaying;
    [SerializeField] float intervalBetweenPlays;
    [SerializeField] float intervalRand = 0f;


    [SerializeField] [Range(-4f, 4f)] float pitch = 1f;
    [SerializeField] [Range(-1f, 1f)] float pitchRand = 0f;


    private AudioSource source;


    private void Start()
    {
        source = GetComponent<AudioSource>();

        if (clips.Count == 0)
            clips.Add(source.clip);
        else source.clip = AudioUtility.RandomClipFromList(clips);

        if (loop && source.playOnAwake)
        {
            source.Play();
            PlayLoopWithInterval();
        }   

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
        if (loop)
            StartCoroutine(ClipLooper(source, AudioUtility.RandomClipFromList(clips), interval));
        else return;
    }


    IEnumerator ClipLooper(AudioSource src, AudioClip clip, float interval)
    {
        while (true)
        {    
            if (!clipPlaying)
            {
                interval = Mathf.Clamp(interval + Random.Range(-intervalRand, intervalRand), 0, interval + intervalRand);                

                StartCoroutine(WaitIntervalThenPlayFromList(src, clips, interval));
                clipPlaying = true;
            }
            yield return null;
        }
       
    }

    public IEnumerator WaitIntervalThenPlayFromList (AudioSource src, List<AudioClip> cliplist, float interval)
    {
        interval += src.clip.length;

        yield return new WaitForSeconds(interval);
        clipPlaying = true;

        src.pitch = pitch + Random.Range(-pitchRand, pitchRand);
        src.clip = AudioUtility.RandomClipFromList(cliplist);
        src.Play();

        yield return new WaitForSeconds(src.clip.length);
        clipPlaying = false;
        yield return null;
    }


}
