using System.Collections;
using UnityEngine;

/// <summary>
/// Controls triggering of underwater fauna movement sounds.
/// </summary>
/// 

public class FishAudio : MonoBehaviour
{
    [SerializeField] AudioClip[] rippleClips;
    bool ripplePlaying;

    float fishPitch = 1.2f;
    float fishVol = -6f;

    float pitchRandScaleFactor = 1.2f;
    float volRandOffset = 3f;

    public void TriggerRipple()
    {
        if (ripplePlaying) return;

        float pitch = fishPitch * Random.Range(-pitchRandScaleFactor, pitchRandScaleFactor);
        float vol = fishVol + Random.Range(-volRandOffset, volRandOffset);

        StartCoroutine(PlayClipThenDestroySource(
            AudioUtility.RandomClipFromArray(rippleClips),
            vol,
            pitch));

        ripplePlaying = true;

    }

    IEnumerator PlayClipThenDestroySource(AudioClip clip, float vol, float pitch)
    {
        var newAudio = gameObject.AddComponent<AudioSource>();

        newAudio.playOnAwake = false;
        newAudio.volume = AudioUtility.ConvertDbtoA(vol);
        newAudio.pitch = pitch;
        newAudio.clip = clip;
        newAudio.spatialize = true;
        newAudio.spatialBlend = 1f;
        newAudio.maxDistance = 15f;
        newAudio.minDistance = 3f;        

        newAudio.Play();

        yield return new WaitForSeconds((clip.length / pitch));
        Destroy(newAudio);

        ripplePlaying = false;
    }
}
