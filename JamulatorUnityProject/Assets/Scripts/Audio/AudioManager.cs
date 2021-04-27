using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private SubmarineState SubState;


    [Header("Environmental Sounds Initialisation")]
    [SerializeField] [Range(-80, 0)] float environmentalStartVol;
    [SerializeField] [Range(0, 15)] float environmentalFadeUpTime;

    [SerializeField] GameObject[] bubbleMakers;
    [SerializeField] [Range(-80, 0)] float bubbleStartVol;
    GameObject bubblemaker_surface;
    GameObject bubblemaker_regular;
    GameObject bubblemaker_deep;

    [SerializeField] GameObject rumble;
    AudioSourceFader ASFrumble;
    [SerializeField] [Range(-80, 0)] float rumbleStartVol;

    [SerializeField] GameObject finWhale;
    AudioSourceFader ASFfin;
    [SerializeField] [Range(-80, 0)] float finWhaleStartVol;

    [SerializeField] GameObject clicker;
    AudioSourceFader ASFclicker;
    [SerializeField] [Range(-80, 0)] float clickerStartVol;


    [Header("External Submarine Sounds")]
    [SerializeField] [Range(-80, 0)] public float extSubSoundsVol;

    [SerializeField] GameObject[] subResonance;
    GameObject subReson_reson;
    GameObject subReson_tuned;
    GameObject subReson_creak;

    [SerializeField] GameObject[] subCreaker;
    GameObject subCreaker_slight;
    GameObject subCreaker_medium;
    GameObject subCreaker_heavy;
    AudioSource as_subCreaker_slight;
    AudioSource as_subCreaker_medium;
    AudioSource as_subCreaker_deep;
    AudioSourcePlayer asp_subCreaker_slight;
    AudioSourcePlayer asp_subCreaker_medium;
    AudioSourcePlayer asp_subCreaker_deep;
    

    [SerializeField] [Range(-100, 0)] private float subDepth;
    [SerializeField] [Range(0, 100)] private float subDamage;





    private void Start()
    {
        SubState = SubmarineState.Instance;
        FindScriptsAndGameObjects();
        SetParams();
    }

    private void FindScriptsAndGameObjects()
    {

        // Audiosource Fader scripts

        ASFrumble = rumble.GetComponent<AudioSourceFader>();
        ASFfin = finWhale.GetComponent<AudioSourceFader>();
        ASFclicker = clicker.GetComponent<AudioSourceFader>();


        // SubReson
        subReson_reson = subResonance[0];
        subReson_tuned = subResonance[1];
        subReson_creak = subResonance[2];

        // SubCreak
        subCreaker_slight = subCreaker[0];
        subCreaker_medium = subCreaker[1];
        subCreaker_heavy = subCreaker[2];


        // Bubbils
        bubblemaker_surface = bubbleMakers[0];
        bubblemaker_regular = bubbleMakers[1];
        bubblemaker_deep = bubbleMakers[2];


    }


    private void SetParams()
    {
        // Sends params from inspector to relevant gameobjects.

        bubbleStartVol += environmentalStartVol;
        rumbleStartVol += environmentalStartVol;
        finWhaleStartVol += environmentalStartVol;
        clickerStartVol += environmentalStartVol;

        for (int i = 0; i < bubbleMakers.Length; ++i)
        {
            foreach (GameObject obj in bubbleMakers[i].GetComponent<DistributeAudioObjects>().createdAudioObjects)
            {
                obj.GetComponent<AudioSourceFader>().FadeTo(bubbleStartVol, environmentalFadeUpTime, 0.5f);
            }
        }

        ASFrumble.outputGain = rumbleStartVol;
        ASFrumble.UpdateAudioSourceAmplitude();

        ASFfin.outputGain = finWhaleStartVol;
        ASFfin.UpdateAudioSourceAmplitude();

        ASFclicker.outputGain = clickerStartVol;
        ASFclicker.UpdateAudioSourceAmplitude();


    }

    private void Update()
    {
        SetFromDamage();
    }

    void SetFromDamage()
    {
        float subDamageRange = 100;
        float _subDamage = subDamage / subDamageRange;

        if (_subDamage > 0.3f && _subDamage < 0.8f)
        {
            if (subReson_tuned.GetComponent<AudioSource>().isPlaying) return;
            subReson_tuned.GetComponent<AudioSource>().Play();
            subReson_tuned.GetComponent<AudioSourceFader>().FadeUp(2);
        }

        if (_subDamage > 0.8f)
        {
            if (subReson_creak.GetComponent<AudioSource>().isPlaying) return;
            subReson_creak.GetComponent<AudioSourcePlayer>().PlayLoopWithInterval();
            subReson_creak.GetComponent<AudioSourceFader>().FadeUp(2);
        }
    }





}
