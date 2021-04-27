using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;

    [SerializeField] bool loop;
    [SerializeField] float intervalBetweenPlays;

    private AudioSource source;
    private float clipLength;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        if (clips != null)
            clipLength = source.clip.length;


        if (loop)
            StartCoroutine(WaitNewThenPlayClip(intervalBetweenPlays));
    }


    public IEnumerator WaitNewThenPlayClip(float interval)
    {
        while (true)
        {
            interval += clipLength;
            yield return new WaitForSeconds(interval);

            source.PlayOneShot(ChooseNewAudioClip(clips.Length));
            yield return null;
        }
    }

    private AudioClip ChooseNewAudioClip(int numClips)
    {        
        return clips[Random.Range(0, numClips - 1)];
    }


}
