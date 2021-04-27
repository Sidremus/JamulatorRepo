using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class AudioUtility
{
    /// <summary>
    /// # atodb(float amplitude), returns db value
    /// # dbtoa(float db), returns amplitude
    /// # getmixergroup(string groupName), returns first group in a master mixer
    /// #
    /// 
    /// created by blubberbaleen, improved by bemore
    /// </summary>

    public static float ConvertAtoDb(float amp)
    {
        amp = Mathf.Clamp(amp, ConvertDbtoA(-70f), 1f);
        return 20 * Mathf.Log(amp) / Mathf.Log(10);
    }

    public static float ConvertDbtoA(float db)
    {
        return Mathf.Pow(10, db / 20);
    }



    public static AudioClip RandomClipFromArray(AudioClip[] cliplist)
    {
        return cliplist[Random.Range(0, cliplist.Length - 1)];
    }


    public static IEnumerator WaitIntervalThenPlay(AudioSource source, AudioClip clip, float interval)
    {
        while (true)
        {
            interval += source.clip.length;
            yield return new WaitForSeconds(interval);
            source.clip = clip;
            source.Play();
            yield return null;
        }
    }


}

