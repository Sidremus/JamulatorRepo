using System.Collections;
using UnityEngine;

/// <summary>
/// Controls triggering of underwater fauna movement sounds.
/// </summary>
/// 

public class FishMoveSound : MonoBehaviour
{
    [SerializeField] AudioClip[] rippleClips;
    [SerializeField] [Range(0.5f, 10f)] float fishScale = 1f;
    [SerializeField] AudioListener listener;

    bool ripplePlaying;

    float fishPitch = 1f;
    float fishVol = -15f;

    float pitchRandScaleOffset = 0.2f;
    float volRandOffset = 3f;

    float sourceMaxDistance = 30f;
    float distance;

    private void Start()
    {
        listener = GetAudioListener();
    }

    AudioListener GetAudioListener()
    {
        return AudioManager.Instance.listener.GetComponent<AudioListener>();
    }

    public void TriggerRipple()
    {
        if (!listener) listener = GetAudioListener();

        if (ripplePlaying)
            return;

        float pitch = Mathf.Clamp((fishPitch + Random.Range(-pitchRandScaleOffset, pitchRandScaleOffset)) - (fishScale / 12f), 0.3f, 2f);
        float vol = (fishVol + Random.Range(-volRandOffset, volRandOffset)) + fishScale;

        distance = Vector3.Distance(transform.position, listener.transform.position);
        if (distance <= sourceMaxDistance * fishScale)
        {
            StartCoroutine(PlayClipThenDestroySource(AudioUtility.RandomClipFromArray(rippleClips), vol, pitch));

            ripplePlaying = true;
        }
        else
        {
            ripplePlaying = false;
            return;
        }


    }

    IEnumerator PlayClipThenDestroySource(AudioClip clip, float vol, float pitch)
    {
        var newAudio = gameObject.AddComponent<AudioSource>();

        newAudio.playOnAwake = false;
        newAudio.volume = AudioUtility.ConvertDbtoA(vol);
        /*pitch -= fishPitch - (fishScale / 12f);*/
        newAudio.pitch = pitch;
        newAudio.clip = clip;
        newAudio.spatialize = true;
        newAudio.spatialBlend = 1f;
        newAudio.maxDistance = sourceMaxDistance * fishScale;
        newAudio.minDistance = 2f * fishScale;
        newAudio.dopplerLevel = 1f;

        newAudio.Play();

        yield return new WaitForSeconds((clip.length / pitch));
        Destroy(newAudio);

        ripplePlaying = false;
    }
}
