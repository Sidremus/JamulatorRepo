using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarinePing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { 
        Debug.Log("Registering onPingOn for "+EventManager.Instance.EventManagerId);
        EventManager.Instance.onPingOn += TurnOnPing;
        EventManager.Instance.onPingOff += TurnOffPing;
    }

    private void OnDisable()
    {
        EventManager.Instance.onPingOn -= TurnOnPing;
        EventManager.Instance.onPingOff -= TurnOffPing;
    }

    private bool pingIsOn;
    private void TurnOnPing()
    {
        Debug.Log("TurnOnPing()");
        pingIsOn = true;
    }

    private void TurnOffPing()
    {
        pingIsOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pingIsOn)
        {
            Debug.Log("Ping!");
        }
    }
}
