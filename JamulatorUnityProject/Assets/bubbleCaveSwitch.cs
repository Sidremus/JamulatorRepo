using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class bubbleCaveSwitch : MonoBehaviour
{

    [SerializeField] bool isInCave;


    [SerializeField] GameObject[] oceanMakers;
    [SerializeField] GameObject[] caveMakers;


    [SerializeField] bool isSwitchingToOcean = false;
    [SerializeField] bool isSwitchingToCave = false;


    void Update()
    {
        
        if (isInCave && !isSwitchingToCave)
        {
            Debug.Log("1");
            isSwitchingToCave = true;
            isSwitchingToOcean = false;


            for (int i = 0; i < oceanMakers.Length; ++i)
            {
                foreach (GameObject obj in oceanMakers[i].GetComponent<DistributeAudioObjects>().createdAudioObjects)
                {
                    obj.GetComponent<AudioSourceFader>().FadeDown(5);
                }
            }

            for (int i = 0; i < caveMakers.Length; ++i)
            {
                foreach (GameObject obj in caveMakers[i].GetComponent<DistributeAudioObjects>().createdAudioObjects)
                {
                    obj.GetComponent<AudioSourceFader>().FadeUp(2);
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
                    obj.GetComponent<AudioSourceFader>().FadeUp(2);
                }
            }

            for (int i = 0; i < caveMakers.Length; ++i)
            {
                foreach (GameObject obj in caveMakers[i].GetComponent<DistributeAudioObjects>().createdAudioObjects)
                {
                    obj.GetComponent<AudioSourceFader>().FadeDown(5);
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
