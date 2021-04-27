using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    private GameObject[] fish;
    private float scanTimer = 0f;
    private float scanDelay = 5f;

    private void Awake() 
    {
        fish = GameObject.FindGameObjectsWithTag("fish");

        DeactivateFishAI();
    }

    private void FixedUpdate() 
    {
        scanTimer += Time.deltaTime;

        if (scanTimer > scanDelay)
        {
            DeactivateFishAI();
            ActivateNearbyFish();  
            scanTimer = 0f;
        }
    }

    private void DeactivateFishAI()
    {
        foreach(GameObject f in fish) 
        {
            f.GetComponent<FishAI>().enabled = false;
        }
    }

    private void ActivateNearbyFish() 
    {
        // Spherecast around submarine
        int layerMask = 1 << 6;
        Vector3 origin = SubmarineState.Instance.submarine.transform.position;

        Collider[] colliders = Physics.OverlapSphere(origin, 10, layerMask);
        foreach(Collider col in colliders) 
        {
            col.gameObject.GetComponent<FishAI>().enabled = true;
        }
    }
}
