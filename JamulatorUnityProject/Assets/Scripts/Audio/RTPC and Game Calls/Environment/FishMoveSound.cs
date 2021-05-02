using System.Collections;
using UnityEngine;

/// <summary>
/// Controls triggering of underwater fauna movement sounds.
/// </summary>
/// 

public class FishMoveSound : MonoBehaviour
{
    [SerializeField] AudioClip[] rippleClips;
    const float minFishScale = 0.5f;
    const float maxFishScale = 10f;
    [SerializeField] [Range(minFishScale, maxFishScale)] float fishScale = 1f;

    bool ripplePlaying;

    float fishPitch = 1f;
    float fishVol = -3f;

    float pitchRandScaleFactor = 1.2f;
    float volRandOffset = 3f;

    public void TriggerRipple()
    {
        if (ripplePlaying) return;

        float pitchMultiplier = fishPitch * Random.Range(1/pitchRandScaleFactor, pitchRandScaleFactor);
        float vol = fishVol + Random.Range(-volRandOffset, volRandOffset);

        StartCoroutine(PlayClipThenDestroySource(
            AudioUtility.RandomClipFromArray(rippleClips),
            vol,
            pitchMultiplier));

        ripplePlaying = true;

    }

    IEnumerator PlayClipThenDestroySource(AudioClip clip, float vol, float pitchMultiplier)
    {
        var newAudio = gameObject.AddComponent<AudioSource>();

        newAudio.playOnAwake = false;
        newAudio.volume = AudioUtility.ConvertDbtoA(vol + (1f * fishScale));
        float fishScalePitchMultiplier = fishScale / maxFishScale;
        Debug.Log("Fish pitch before: " + pitchMultiplier);
        Debug.Log($"{pitchMultiplier} * {fishScalePitchMultiplier}");
        pitchMultiplier = pitchMultiplier * fishScalePitchMultiplier;
        Debug.Log("Fish pitch after: " + pitchMultiplier);
        newAudio.pitch = pitchMultiplier;
        newAudio.clip = clip;
        newAudio.spatialize = true;
        newAudio.spatialBlend = 1f;
        newAudio.maxDistance = 15f * fishScale;
        newAudio.minDistance = 3f * fishScale;
        newAudio.dopplerLevel = 1f;

        newAudio.Play();

        yield return new WaitForSeconds((clip.length / pitchMultiplier));
        Destroy(newAudio);

        ripplePlaying = false;
    }
}
