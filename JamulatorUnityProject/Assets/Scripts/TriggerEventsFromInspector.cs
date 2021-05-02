using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Just wanted a way to trigger events from inspector so I could test audio at runtime
/// - blubberbaleen
/// </summary>


public class TriggerEventsFromInspector : MonoBehaviour
{
    [SerializeField] bool onSonarPing;
    [SerializeField] bool onLightsOn;
    [SerializeField] bool onLightsOff;
    [SerializeField] bool onPingOn;
    [SerializeField] bool onPingOff;
    [SerializeField] bool onCaveEntered;
    [SerializeField] bool onCaveExited;

    void OnValidate()
    {
        if (Application.isPlaying)
        {
            if (onSonarPing)
            {
                EventManager.Instance.NotifyOfSonarPing();
                onSonarPing = false;
            }

            if (onLightsOn)
            {
                EventManager.Instance.NotifyOfLightsOn();
                onLightsOn = false;
            }

            if (onLightsOff)
            {
                EventManager.Instance.NotifyOfLightsOff();
                onLightsOff = false;
            }

            if (onPingOn)
            {
                Debug.Log("EventManager.Instance.NotifyOfPingOn();");
                EventManager.Instance.NotifyOfPingOn();
                onPingOn = false;
            }

            if (onPingOff)
            {
                EventManager.Instance.NotifyOfPingOff();
                onPingOff = false;
            }

            if (onCaveEntered)
            {
                EventManager.Instance.NotifyOfCaveEntered();
                onCaveEntered = false;
            }

            if (onCaveExited)
            {
                EventManager.Instance.NotifyOfCaveExited();
                onCaveExited = false;
            }

        }



    }
    

}
