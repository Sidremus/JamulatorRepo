using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthToCreakAudio : MonoBehaviour
{
    [SerializeField] GameObject slightCreak;
    Gain slightGain;
    AudioSourcePlayer slightSource;

    [SerializeField] GameObject mediumCreak;
    Gain mediumGain;
    AudioSourcePlayer mediumSource;

    [SerializeField] GameObject heavyCreak;
    Gain heavyGain;
    AudioSourcePlayer heavySource;
    

    [SerializeField] [Range(-100, 0)] float subDepth;

    float extGain;
    float subDepthRange = 100;
    float _gainMin = -48f;
    float _gainMax = 0f;

    [SerializeField] float slightDepthMin = 0.5f;
    [SerializeField] float mediumDepthMin = 0.6f;
    [SerializeField] float deepDepthMin = 0.8f;

    private void Start()
    {
        slightGain = slightCreak.GetComponent<Gain>();
        mediumGain = mediumCreak.GetComponent<Gain>();
        heavyGain = heavyCreak.GetComponent<Gain>();

        slightSource = slightCreak.GetComponent<AudioSourcePlayer>();
        mediumSource = mediumCreak.GetComponent<AudioSourcePlayer>();
        heavySource = heavyCreak.GetComponent<AudioSourcePlayer>();

    }

    private void Update()
    {

        subDepth = AudioManager.Instance.subDepth;
        float depth = Mathf.Abs(subDepth) / subDepthRange;

        extGain = AudioManager.Instance.extSubSoundsVol;
        float gainMin = _gainMin + extGain;
        float gainMax = _gainMax + extGain;

        if (depth < slightDepthMin)
        {
            slightGain.inputGain = AudioUtility.ScaleValue(depth, 0, slightDepthMin, gainMin, gainMax);
            PlayIfNotPlaying(slightSource);

            mediumGain.inputGain = gainMin;
            StopIfPlaying(mediumSource);

            heavyGain.inputGain = gainMin;
            StopIfPlaying(heavySource);
        }
        else if (depth > slightDepthMin && depth < mediumDepthMin)
        {
            //Slight
            slightGain.inputGain = gainMax;
            PlayIfNotPlaying(slightSource);

            mediumGain.inputGain = AudioUtility.ScaleValue(depth, slightDepthMin, mediumDepthMin, gainMin, gainMax);
            PlayIfNotPlaying(mediumSource);

            heavyGain.inputGain = gainMin;
            StopIfPlaying(heavySource);

        }
        else if (depth > mediumDepthMin && depth < deepDepthMin)
        {
            //Medium
            slightGain.inputGain = AudioUtility.ScaleValue(depth, mediumDepthMin, deepDepthMin, gainMax, gainMin);
            PlayIfNotPlaying(slightSource);

            mediumGain.inputGain = gainMax;
            PlayIfNotPlaying(mediumSource);

            heavyGain.inputGain = AudioUtility.ScaleValue(depth, mediumDepthMin, deepDepthMin, gainMin, gainMax);
            PlayIfNotPlaying(heavySource);

        }
        else if ( depth > deepDepthMin)
        {
            //Deep
            slightCreak.GetComponent<Gain>().inputGain = gainMin;
            PlayIfNotPlaying(slightSource);

            mediumCreak.GetComponent<Gain>().inputGain = AudioUtility.ScaleValue(depth, deepDepthMin, 1f, gainMax, gainMin);
            PlayIfNotPlaying(mediumSource);

            heavyCreak.GetComponent<Gain>().inputGain = gainMax;
            PlayIfNotPlaying(heavySource);

        }


      

    }

    private void PlayIfNotPlaying(AudioSourcePlayer source)
    {
        if (source.clipPlaying)
            return;
        else
            source.PlayLoopWithInterval();

    }
    private void StopIfPlaying(AudioSourcePlayer source)
    {
        if (source.clipPlaying)
            source.loop = false;


    }









}
