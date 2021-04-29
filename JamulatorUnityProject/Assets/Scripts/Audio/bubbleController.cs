using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BubbleController : MonoBehaviour
{
    [SerializeField] bool isInCave;

    [SerializeField] GameObject[] oceanMakers;
    [SerializeField] GameObject[] caveMakers;

    bool isSwitchingToOcean = false;
    bool isSwitchingToCave = false;

    float fadeDownLevel = -36f;
    float fadeUpLevel = 0f;


    void CaveEntered()
    {
        isInCave = true;
    }
    void CaveExited()
    {
        isInCave = false;
    }

    void Update() 
    {

        if (isInCave && !isSwitchingToCave)
        {
            isSwitchingToCave = true;
            isSwitchingToOcean = false;


            for (int i = 0; i < oceanMakers.Length; ++i)
            {
                foreach (GameObject obj in oceanMakers[i].GetComponent<DistributeAudioObjects>().createdAudioObjects)
                {
                    obj.GetComponent<AudioSourceFader>().FadeTo(fadeDownLevel + AudioManager.Instance.bubbleVol, 5, 0.3f);
                }
            }

            for (int i = 0; i < caveMakers.Length; ++i)
            {
                foreach (GameObject obj in caveMakers[i].GetComponent<DistributeAudioObjects>().createdAudioObjects)
                {
                    obj.GetComponent<AudioSourceFader>().FadeTo(fadeUpLevel + AudioManager.Instance.bubbleVol, 5, 0.8f);
                }
            }
        }

        if (!isInCave && !isSwitchingToOcean)
        {
            isSwitchingToOcean = true;
            isSwitchingToCave = false;

            for (int i = 0; i < oceanMakers.Length; ++i)
            {
                foreach (GameObject obj in oceanMakers[i].GetComponent<DistributeAudioObjects>().createdAudioObjects)
                {
                    obj.GetComponent<AudioSourceFader>().FadeTo(fadeUpLevel + AudioManager.Instance.bubbleVol, 2 , 0.8f);
                }
            }

            for (int i = 0; i < caveMakers.Length; ++i)
            {
                foreach (GameObject obj in caveMakers[i].GetComponent<DistributeAudioObjects>().createdAudioObjects)
                {
                    obj.GetComponent<AudioSourceFader>().FadeTo(fadeDownLevel + AudioManager.Instance.bubbleVol, 5, 0.3f);
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

