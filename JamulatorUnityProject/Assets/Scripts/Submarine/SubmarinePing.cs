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


    private float lastBeepTime = 0;
    [SerializeField]
    float minimumBeepGap = 0.05f;
    
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
                    
                    float pingGap = minimumBeepGap * Mathf.Sqrt(distance);
                    Debug.Log($"Ping! dist {distance} delta {pingGap}" );
                    float timeSincePing = Time.time - lastBeepTime;

                    if (pingGap < timeSincePing)
                    {
                        EventManager.Instance.NotifyOfSonarPing();
                        lastBeepTime = Time.time;
                    }
                }
                else
                {
                    Debug.Log("Ping found no chest!");
                }
            }
            
        }
    }
}
