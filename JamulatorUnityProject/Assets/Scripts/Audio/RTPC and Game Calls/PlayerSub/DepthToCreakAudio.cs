using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthToCreakAudio : MonoBehaviour
{
    [SerializeField] GameObject slightCreak;
    AudioSourceController slightCtrl;

    [SerializeField] GameObject mediumCreak;
    AudioSourceController mediumCtrl;

    [SerializeField] GameObject heavyCreak;
    AudioSourceController heavyCtrl;
    

    [SerializeField] [Range(-100, 0)] float subDepth;

    float extGain;
    float subDepthRange = 100;
    float _gainMin = -48f;
    float _gainMax = 0f;

    [SerializeField] float slightDepthMin = 40f;
    [SerializeField] float mediumDepthMin = 60f;
    [SerializeField] float deepDepthMin = 80f;

    [SerializeField] float[] vols;

    private void Start()
    {
        slightCtrl = slightCreak.GetComponent<AudioSourceController>();
        mediumCtrl = mediumCreak.GetComponent<AudioSourceController>();
        heavyCtrl = heavyCreak.GetComponent<AudioSourceController>();

    }

    private void Update()
    {

        subDepth = AudioManager.Instance.subDepth;
        float depth = (Mathf.Abs(subDepth) / subDepthRange) * 100f;

        extGain = AudioManager.Instance.extSubSoundsVol;
        float gainMin = _gainMin + extGain;
        float gainMax = _gainMax + extGain;

        float slightVol;
        float mediumVol;
        float heavyVol;

        float fadeOutTime = 3f;

        if (depth < slightDepthMin)
        {
            slightVol = AudioUtility.ScaleValue(depth, 0, slightDepthMin, gainMin, gainMax);
            slightCtrl.PlayLoopWithInterval();

            mediumVol = gainMin;
            mediumCtrl.StopLooping(fadeOutTime);

            heavyVol = gainMin;
            heavyCtrl.StopLooping(fadeOutTime);
        }
        else if (depth > slightDepthMin && depth < mediumDepthMin)
        {
            //Slight
            slightVol = gainMax;
            slightCtrl.PlayLoopWithInterval();

            mediumVol = AudioUtility.ScaleValue(depth, slightDepthMin, mediumDepthMin, gainMin, gainMax);
            mediumCtrl.PlayLoopWithInterval();

            heavyVol = gainMin;
            heavyCtrl.StopLooping(fadeOutTime);

        }
        else if (depth > mediumDepthMin && depth < deepDepthMin)
        {
            //Medium
            slightVol = AudioUtility.ScaleValue(depth, mediumDepthMin, deepDepthMin, gainMax, gainMin);
            slightCtrl.PlayLoopWithInterval();

            mediumVol = gainMax;
            mediumCtrl.PlayLoopWithInterval();

            heavyVol = AudioUtility.ScaleValue(depth, mediumDepthMin, deepDepthMin, gainMin, gainMax);
            heavyCtrl.PlayLoopWithInterval();

        }
        else if ( depth > deepDepthMin)
        {
            //Deep
            slightVol = gainMin;
            slightCtrl.StopLooping(fadeOutTime);

            mediumVol = AudioUtility.ScaleValue(depth, deepDepthMin, 1f, gainMax, gainMin);
            mediumCtrl.PlayLoopWithInterval();

            heavyVol = gainMax;
        }
        else
        {
            Debug.Log("Depth to creak audio is reading depth wrong. Sending some values and wishing you the best");
            slightVol = gainMin;
            mediumVol = gainMin;
            heavyVol = gainMin;
        }

        vols = new float[3];
        vols[0] = slightVol;
        vols[1] = mediumVol;
        vols[2] = heavyVol;

        slightCtrl.SetInputGain(slightVol);
        mediumCtrl.SetInputGain(mediumVol);
        heavyCtrl.SetInputGain(heavyVol);



    }




}
