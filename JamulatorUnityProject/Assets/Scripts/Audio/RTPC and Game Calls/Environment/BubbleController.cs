using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BubbleController : MonoBehaviour
{
    [SerializeField] GameObject[] oceanMakers;
    [SerializeField] GameObject[] caveMakers;

    bool isSwitchingToOcean = false;
    bool isSwitchingToCave = false;

    float fadeDownLevel = -36f;
    float fadeUpLevel = 0f;


    void CaveEntered()
    {
        if (isSwitchingToCave) return;

        isSwitchingToCave = true;
        isSwitchingToOcean = false;


        for (int i = 0; i < oceanMakers.Length; ++i)
        {
            foreach (GameObject obj in oceanMakers[i].GetComponent<DistributeAudioObjects>().createdAudioObjects)
            {
                obj.GetComponent<AudioSourceController>().FadeTo(fadeDownLevel + AudioManager.Instance.bubbleVol, 5, 0.3f, false);
            }
        }

        for (int i = 0; i < caveMakers.Length; ++i)
        {
            foreach (GameObject obj in caveMakers[i].GetComponent<DistributeAudioObjects>().createdAudioObjects)
            {
                obj.GetComponent<AudioSourceController>().FadeTo(fadeUpLevel + AudioManager.Instance.bubbleVol, 5, 0.8f, false);
            }
        }


    }
    void CaveExited()
    {
        if (!isSwitchingToOcean)
        {
            isSwitchingToOcean = true;
            isSwitchingToCave = false;

            for (int i = 0; i < oceanMakers.Length; ++i)
            {
                foreach (GameObject obj in oceanMakers[i].GetComponent<DistributeAudioObjects>().createdAudioObjects)
                {
                    obj.GetComponent<AudioSourceController>().FadeTo(fadeUpLevel + AudioManager.Instance.bubbleVol, 2, 0.8f, false);
                }
            }

            for (int i = 0; i < caveMakers.Length; ++i)
            {
                foreach (GameObject obj in caveMakers[i].GetComponent<DistributeAudioObjects>().createdAudioObjects)
                {
                    obj.GetComponent<AudioSourceController>().FadeTo(fadeDownLevel + AudioManager.Instance.bubbleVol, 5, 0.3f, false);
                }
            }


        }

    }

    // Receives Cave events //
    void Start()
    {
        EventManager.Instance.onCaveEntered += CaveEntered;
        EventManager.Instance.onCaveExited += CaveExited;
    }
    void onDisable()
    {
        EventManager.Instance.onCaveEntered -= CaveEntered;
        EventManager.Instance.onCaveExited -= CaveExited;
    }


}

