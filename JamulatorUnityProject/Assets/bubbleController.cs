using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class bubbleController : MonoBehaviour
{
    [SerializeField]  AudioManager manager;

    [SerializeField] bool isInCave;

    [SerializeField] GameObject[] oceanMakers;
    [SerializeField] GameObject[] caveMakers;

    bool isSwitchingToOcean = false;
    bool isSwitchingToCave = false;

    float extGain;

    float fadeDownLevel = -36f;
    float fadeUpLevel = 0f;


    void Update()
    {

        isInCave = manager.isInCave;
        extGain = manager.bubbleVol;

        if (isInCave && !isSwitchingToCave)
        {
            Debug.Log("1");
            isSwitchingToCave = true;
            isSwitchingToOcean = false;


            for (int i = 0; i < oceanMakers.Length; ++i)
            {
                foreach (GameObject obj in oceanMakers[i].GetComponent<DistributeAudioObjects>().createdAudioObjects)
                {
                    obj.GetComponent<AudioSourceFader>().FadeTo(fadeDownLevel + extGain, 5, 0.3f);
                }
            }

            for (int i = 0; i < caveMakers.Length; ++i)
            {
                foreach (GameObject obj in caveMakers[i].GetComponent<DistributeAudioObjects>().createdAudioObjects)
                {
                    obj.GetComponent<AudioSourceFader>().FadeTo(fadeUpLevel + extGain, 5, 0.8f);
                }
            }
        }

        if (!isInCave && !isSwitchingToOcean)
        {
            Debug.Log("2");
            isSwitchingToOcean = true;
            isSwitchingToCave = false;

            for (int i = 0; i < oceanMakers.Length; ++i)
            {
                foreach (GameObject obj in oceanMakers[i].GetComponent<DistributeAudioObjects>().createdAudioObjects)
                {
                    obj.GetComponent<AudioSourceFader>().FadeTo(fadeUpLevel + extGain, 2 , 0.8f);
                }
            }

            for (int i = 0; i < caveMakers.Length; ++i)
            {
                foreach (GameObject obj in caveMakers[i].GetComponent<DistributeAudioObjects>().createdAudioObjects)
                {
                    obj.GetComponent<AudioSourceFader>().FadeTo(fadeDownLevel + extGain, 5, 0.3f);
                }
            }


        }


    }

    IEnumerator WaitAndSwitchBool(bool b, int t)
    {
        yield return new WaitForSeconds(t);
        b = !b;
        yield break;


    }
}
