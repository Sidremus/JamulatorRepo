using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All-in-one audiosource controller.
/// </summary>

public class AudioSourceController : MonoBehaviour
{
    [SerializeField] AudioSource source;

    [Header("Gain")]
    // Processing stage for audio.
    [SerializeField] [Range(-100, 24)] float outputGain = 0f;
    [Range(-100, 0)] float inputGain = 0f;

    [Header("Fader")]
    // Controls fades.
    public float FadeInOnAwakeTime = 0f;
    float fadeVolume;
    bool isFading;

    [Header("Clip Player")]
    // Choosing and playing back clips with variations.
    [SerializeField] List<AudioClip> clips = new List<AudioClip>();
    [SerializeField] bool playOnAwake = false;
    [SerializeField] bool loop = false;
    [SerializeField] bool newClipPerPlay = false;
    [SerializeField] float intervalBetweenPlays = 0f;
    [SerializeField] float intervalRand = 0f;

    [SerializeField] [Range(-4f, 4f)] float pitch = 1f;
    [SerializeField] [Range(-1f, 1f)] float pitchRand = 0f;

    bool isPlaying;



    public void SetGain(float value)
    {
        inputGain = value;
        UpdateParams();
    }

    #region Initialisation

    private void Start()
    {
        GetAudioSourceVolume();

        // Takes over audiosource functions.
        if (source.isPlaying) 
            source.Stop();
        source.loop = false;
        source.playOnAwake = false;
        

        // Chooses/plays clips as set.
        if (clips.Count == 0)
            Debug.LogError(this + "on " + gameObject.name + ": Attach at least one AudioClip to the AudioSourceController");

        if (newClipPerPlay)
            source.clip = AudioUtility.RandomClipFromList(clips);
        else
            source.clip = clips[0];

        if (playOnAwake)
        {
            source.pitch = pitch + Random.Range(-pitchRand, pitchRand);
            source.Play();
        }

        if (loop)
        {
            PlayLoopWithInterval();
        }
        
        if (FadeInOnAwakeTime > 0.0f)
        {
            FadeTo(outputGain + inputGain, FadeInOnAwakeTime, 0.5f, false);
        }


    }

    #endregion

    private void GetAudioSourceVolume()
    {
        if (!source)
            source = GetComponent<AudioSource>();

        fadeVolume = AudioUtility.ConvertAtoDb(source.volume);        
    }
   

    #region Player/Looper

    public void PlayLoopWithInterval()
    {
        loop = true;
        StartCoroutine(ClipLooper(intervalBetweenPlays));
    }
    public void PlayLoopWithInterval(float interval)
    {
        loop = true;
        StartCoroutine(ClipLooper(interval));
    }
    IEnumerator ClipLooper (float interval)
    {
        while (true)
        {
            if (!isPlaying)
            {
                AudioClip newClip;

                if (newClipPerPlay)
                    newClip = AudioUtility.RandomClipFromList(clips);
                else newClip = source.clip;

                interval = Mathf.Clamp(interval + Random.Range(-intervalRand, intervalRand), 0, interval + intervalRand);
                interval += source.clip.length;
                yield return new WaitForSeconds(interval);
                

                float newClipPitch = pitch + Random.Range(-pitchRand, pitchRand);
                source.pitch = newClipPitch;

                source.clip = newClip;
                source.Play();

                isPlaying = true;
                yield return new WaitForSeconds(source.clip.length / newClipPitch);
                isPlaying = false;
                
                yield return null;
            }
            yield return null;
        }
    }

    #endregion

    #region Fader

    public void FadeTo(float targetVol, float fadetime, float curveShape, bool stopAfterFade)
    {
        // Uses an AnimationCurve
        // curveShape 0.0 = linear; curveShape 0.5 = s-curve; curveshape 1.0 (exponential).
        Keyframe[] keys = new Keyframe[2];
        keys[0] = new Keyframe(0, 0, 0, 1f - curveShape, 0, 1f - curveShape);
        keys[1] = new Keyframe(1, 1, 1f - curveShape, 0f, curveShape, 0);
        AnimationCurve animcur = new AnimationCurve(keys);

        if (isFading)
        {
            StopCoroutine(StartFadeInDb(fadetime, targetVol, animcur, stopAfterFade));
            isFading = false;
        }
        StartCoroutine(StartFadeInDb(fadetime, targetVol, animcur, stopAfterFade));
        isFading = true;

    }


    private IEnumerator StartFadeInDb(float fadetime, float targetVol, AnimationCurve animcur, bool stopAfterFade)
    {
        GetAudioSourceVolume();
        float currentTime = 0f;
        float startVol = fadeVolume;

        while (currentTime < fadetime)
        {
            currentTime += Time.deltaTime;
            fadeVolume = Mathf.Lerp(startVol, targetVol, animcur.Evaluate(currentTime / fadetime));

            UpdateParams();
        }

        isFading = false;

        if (stopAfterFade)
        {
            yield return new WaitForSeconds(fadetime);
            source.Stop();
        }

        yield break;
    }

    #endregion

    private void UpdateParams()
    {
        source.volume = AudioUtility.ConvertDbtoA(fadeVolume + outputGain + inputGain);
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

}
