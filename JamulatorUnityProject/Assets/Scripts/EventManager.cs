using UnityEngine;
using System;

/**
 * The event manager handles subscription to, and notification of, our custom game events.
 * To subscribe to an event:
 * In your script, in OnEnable or Start, you can subscibe to an event via:
 * - EventManager.Instance.onSonarPing += MyFunctionName
 * Where MyFunctionName is a function in your script.
 * 
 * IMPORTANT! Remember to unsubscribe from the event in OnDisalbe via:
 * - EventManager.Instance.onSonarPing -= MyFunctionName
 *
 * For each event there is a corresponding function that notifies subscribers of that event.
 * These functions are named 'NotifyOf<eventName>', and can be called from anywhere (typically SubmarineState).
 *
 * If you need an event that doesn't yet exist, you can add in the Action and NotifyOf function here,
 * and call the NotifyOf function from wherever makes sense.
 */

public class EventManager : MonoBehaviour
{
    private static EventManager _instance;
    public static EventManager Instance { get { return _instance; } }

    public event Action onSubCollision;
    public void NotifyOfSubCollision()
    {
        if (onSubCollision != null)
        {
            onSubCollision();
        }
    }

    public event Action onSonarPing;
    public void NotifyOfSonarPing()
    {
        if (onSonarPing != null)
        {
            onSonarPing();
        }
    }

    public event Action onLightsOn;
    public void NotifyOfLightsOn()
    {
        if (onLightsOn != null)
        {
            onLightsOn();
        }
    }

    public event Action onLightsOff;
    public void NotifyOfLightsOff()
    {
        if (onLightsOff != null)
        {
            onLightsOff();
        }
    }

    public event Action onCaveEntered;
    public void NotifyOfCaveEntered()
    {
        if (onCaveEntered != null)
        {
            onCaveEntered();
        }
    }

    public event Action onCaveExited;
    public void NotifyOfCaveExited()
    {
        if (onCaveExited != null)
        {
            onCaveExited();
        }
    }



    private void Awake() 
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;    
        DontDestroyOnLoad(this.gameObject);
    }
}
