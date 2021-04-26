using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineLights : MonoBehaviour
{
    public Light submarineLight;

    void Start()
    {
        EventManager.Instance.onLightsOn += TurnOnLight;
        EventManager.Instance.onLightsOff += TurnOffLight;
    }

    private void OnDisable() {
        EventManager.Instance.onLightsOn -= TurnOnLight;
        EventManager.Instance.onLightsOff -= TurnOffLight;
    }

    private void TurnOnLight()
    {
        submarineLight.enabled = true;
    }

    private void TurnOffLight()
    {
        submarineLight.enabled = false;
    }
}
