using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthToCreakAudio : MonoBehaviour
{
    [SerializeField] AudioManager manager;

    [SerializeField] GameObject slightCreak;
    Gain lightGain;
    [SerializeField] GameObject mediumCreak;
    Gain mediumGain;
    [SerializeField] GameObject heavyCreek;
    Gain heavyGain;

    [SerializeField] [Range(-100, 0)] float subDepth;

    float extGain;
    float subDepthRange = 100;
    float _gainMin = -48f;
    float _gainMax = 0f;

    float slightDepthMin = 0.2f;
    float mediumDepthMin = 0.5f;
    float deepDepthMin = 0.75f;

    private void Start()
    {
        lightGain = slightCreak.GetComponent<Gain>();
        mediumGain = mediumCreak.GetComponent<Gain>();
        heavyGain = heavyCreek.GetComponent<Gain>();

    }

    private void Update()
    {

        subDepth = manager.subDepth;
        float depth = Mathf.Abs(subDepth) / subDepthRange;

        extGain = manager.extSubSoundsVol;
        float gainMin = _gainMin + extGain;
        float gainMax = _gainMax + extGain;

        if (depth < slightDepthMin)
        {
            slightCreak.GetComponent<Gain>().inputGain = AudioUtility.ConvertRange(0, slightDepthMin, gainMin, gainMax, depth);
            mediumCreak.GetComponent<Gain>().inputGain = gainMin;
            heavyCreek.GetComponent<Gain>().inputGain = gainMin;
        }
        else if (depth > slightDepthMin && depth < mediumDepthMin)
        {
            //Slight
            slightCreak.GetComponent<Gain>().inputGain = gainMax;
            mediumCreak.GetComponent<Gain>().inputGain = AudioUtility.ConvertRange(slightDepthMin, mediumDepthMin, gainMin, gainMax, depth);
            heavyCreek.GetComponent<Gain>().inputGain = gainMin;

        }
        else if (depth > mediumDepthMin && depth < deepDepthMin)
        {
            //Medium
            slightCreak.GetComponent<Gain>().inputGain = AudioUtility.ConvertRange(mediumDepthMin, deepDepthMin, gainMax, gainMin, depth);
            mediumCreak.GetComponent<Gain>().inputGain = gainMax;
            heavyCreek.GetComponent<Gain>().inputGain = AudioUtility.ConvertRange(mediumDepthMin, deepDepthMin, gainMin, gainMax, depth);

        }
        else if ( depth > deepDepthMin)
        {
            //Deep
            slightCreak.GetComponent<Gain>().inputGain = gainMin;
            mediumCreak.GetComponent<Gain>().inputGain = AudioUtility.ConvertRange(deepDepthMin, 1f, gainMax, gainMin, depth);
            heavyCreek.GetComponent<Gain>().inputGain = gainMax;

        }


      

    }









}
