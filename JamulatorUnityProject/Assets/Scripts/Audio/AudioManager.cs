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

    [SerializeField] GameObject rumble;
    [SerializeField] [Range(-80, 0)] float rumbleStartVol;

    [SerializeField] GameObject finWhale;
    [SerializeField] [Range(-80, 0)] float finWhaleStartVol;

    [SerializeField] GameObject clicker;
    [SerializeField] [Range(-80, 0)] float clickerStartVol;


    private void Start()
    {
        SubState = SubmarineState.Instance;

        SetParams();
    }


    private void SetParams()
    {
        // Sends params from inspector to relevant gameobjects.

        bubbleStartVol += environmentalStartVol;

        for (int i = 0; i < bubbleMakers.Length; ++i)
        {
            foreach (GameObject obj in bubbleMakers[i].GetComponent<DistributeAudioObjects>().createdAudioObjects)
            {
                obj.GetComponent<AudioSourceFader>().FadeTo(bubbleStartVol, environmentalFadeUpTime, 0.5f);
            }
        }

        rumble.GetComponent<AudioSourceFader>().outputGain = rumbleStartVol += environmentalStartVol;
        rumble.GetComponent<AudioSourceFader>().UpdateAudioSourceAmplitude();

        finWhale.GetComponent<AudioSourceFader>().outputGain = finWhaleStartVol += environmentalStartVol;
        finWhale.GetComponent<AudioSourceFader>().UpdateAudioSourceAmplitude();

        clicker.GetComponent<AudioSourceFader>().outputGain = clickerStartVol += environmentalStartVol;
        clicker.GetComponent<AudioSourceFader>().UpdateAudioSourceAmplitude();


    }


    private IEnumerator WaitIntervalThenPlay(AudioSource source, float interval)
    {
        while (true)
        {
            float clipLength = source.clip.length;
            interval += clipLength;
            yield return new WaitForSeconds(interval);
            source.Play();
            yield return null;
        }      


    }




}
