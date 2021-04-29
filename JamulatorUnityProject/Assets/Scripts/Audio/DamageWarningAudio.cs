using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageWarningAudio : MonoBehaviour
{
    [SerializeField] GameObject beepingLight;
    Gain lightGain;
    [SerializeField] GameObject beepingMedium;
    Gain mediumGain;
    [SerializeField] GameObject beepingHeavy;
    Gain heavyGain;
    [SerializeField] GameObject alarm;
    Gain alarmGain;

    [SerializeField] [Range(0f, 100f)] float subDamage;
    float subDamageRange = 100f;

    float extGain;

    [SerializeField] float lightBeepingThreshold;
    [SerializeField] float mediumBeepingThreshold;
    [SerializeField] float heavyBeepingThreshold;
    [SerializeField] float alarmThreshold;

    [SerializeField] float _minGain = -100f;
    float minGain;
    [SerializeField] float _lowGain = -16f;
    float lowGain;
    [SerializeField] float _highGain = 0f;
    float highGain;

    [SerializeField] float[] gains;

    void Start()
    {
        lightGain = beepingLight.GetComponent<Gain>();
        mediumGain = beepingMedium.GetComponent<Gain>();
        heavyGain = beepingHeavy.GetComponent<Gain>();
        alarmGain = alarm.GetComponent<Gain>();
    }

    float v;
    void Update()
    {
        float damage = AudioManager.Instance.subDamage;
        extGain = AudioManager.Instance.UIVol;

        minGain = _minGain + extGain;
        lowGain = _lowGain + extGain;
        highGain = _highGain + extGain;

        if (damage < lightBeepingThreshold)
        {
            

            lightGain.inputGain = minGain;
            mediumGain.inputGain = minGain;
            heavyGain.inputGain = minGain;
            alarmGain.inputGain = minGain;

        }
        
        else if (damage > lightBeepingThreshold && damage < mediumBeepingThreshold)
        {
            PlayIfNotPlaying(beepingLight.GetComponent<AudioSource>());

            lightGain.inputGain = AudioUtility.ScaleValue(damage, lightBeepingThreshold, mediumBeepingThreshold, lowGain, highGain);
            mediumGain.inputGain = minGain;
            heavyGain.inputGain = minGain;
            alarmGain.inputGain = minGain;
        }

        else if (damage > mediumBeepingThreshold && damage < heavyBeepingThreshold)
        {
            PlayIfNotPlaying(beepingLight.GetComponent<AudioSource>());
            PlayIfNotPlaying(beepingMedium.GetComponent<AudioSource>());

            lightGain.inputGain = highGain;
            mediumGain.inputGain = AudioUtility.ScaleValue(damage, mediumBeepingThreshold, heavyBeepingThreshold, lowGain, highGain);
            heavyGain.inputGain = minGain;
            alarmGain.inputGain = minGain;

        }

        else if (damage > heavyBeepingThreshold && damage < alarmThreshold)
        {
            PlayIfNotPlaying(beepingLight.GetComponent<AudioSource>());
            PlayIfNotPlaying(beepingMedium.GetComponent<AudioSource>());
            PlayIfNotPlaying(beepingHeavy.GetComponent<AudioSource>());
            StopIfPlaying(alarm.GetComponent<AudioSource>());

            lightGain.inputGain = highGain;
            mediumGain.inputGain = highGain;
            heavyGain.inputGain = AudioUtility.ScaleValue(damage, heavyBeepingThreshold, alarmThreshold, lowGain, highGain);
            alarmGain.inputGain = minGain;

        }

        else if (damage > alarmThreshold)
        {            
            PlayIfNotPlaying(alarm.GetComponent<AudioSource>());

            alarmGain.inputGain = highGain;
            lightGain.inputGain = minGain;
            mediumGain.inputGain = minGain;
            heavyGain.inputGain = minGain;
           

        }

        gains = new float[4] { lightGain.inputGain, mediumGain.inputGain, heavyGain.inputGain, alarmGain.inputGain } ;


    }


    private void PlayIfNotPlaying(AudioSource source)
    {
        if (source.isPlaying) return;
        else source.Play();
    }

    private void StopIfPlaying(AudioSource source)
    {
        if (source.isPlaying) source.Stop();
        
    }


}
