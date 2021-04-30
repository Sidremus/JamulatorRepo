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

    bool ripplePlaying;

    float fishPitch = 1f;
    float fishVol = -3f;

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
        newAudio.volume = AudioUtility.ConvertDbtoA(vol + (1f * fishScale));
        pitch -= fishPitch - (fishScale / 12f);
        newAudio.pitch = pitch;
        newAudio.clip = clip;
        newAudio.spatialize = true;
        newAudio.spatialBlend = 1f;
        newAudio.maxDistance = 30f * fishScale;
        newAudio.minDistance = 5f * fishScale;
        newAudio.dopplerLevel = 3f;

        newAudio.Play();

        yield return new WaitForSeconds((clip.length / pitch));
        Destroy(newAudio);

        ripplePlaying = false;
    }
}
