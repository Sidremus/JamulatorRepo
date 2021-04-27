using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthToCreakAudio : MonoBehaviour
{
    private SubmarineState SubState;
    [SerializeField] AudioManager manager;

    [SerializeField] GameObject slightCreak;
    [SerializeField] GameObject mediumCreak;
    [SerializeField] GameObject heavyCreek;

    [SerializeField] [Range(-100, 0)] float subDepth;

    float extGain;
    float subDepthRange = 100;
    float gainMin = -24f;
    float gainMax = 0f;

    float slightDepthMin = 0.2f;
    float mediumDepthMin = 0.5f;
    float deepDepthMin = 0.75f;

    private void Start()
    {
        SubState = SubmarineState.Instance;
        extGain = manager.extSubSoundsVol;
    }

    private void Update()
    {
        float depth = Mathf.Abs(subDepth) / subDepthRange;

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
