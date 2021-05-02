using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarinePing : MonoBehaviour
{
    private GameObject submarine;
    private TreasureChestManager treasureChestManager;
    // Start is called before the first frame update
    void Start()
    { 
        Debug.Log("Registering onPingOn for "+EventManager.Instance.EventManagerId);
        EventManager.Instance.onPingOn += TurnOnPing;
        EventManager.Instance.onPingOff += TurnOffPing;
        submarine = gameObject;
        treasureChestManager = GetComponent<TreasureChestManager>();
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
            if (treasureChestManager != null)
            {
                var chest = treasureChestManager.GetClosestChest(submarine.transform.position);
                if (chest != null)
                {
                    float distance = Vector3.Distance(chest.transform.position, submarine.transform.position);
                    Debug.Log("Ping! " + distance);
                }
                else
                {
                    Debug.Log("Ping found no chest!");
                }
            }
            
        }
    }
}
