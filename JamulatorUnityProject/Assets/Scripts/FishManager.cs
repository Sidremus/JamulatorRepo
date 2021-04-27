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
            ActivateNearbyFishAI();  
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

    private void ActivateNearbyFishAI() 
    {
        // Spherecast around submarine
        int radius = 200;
        int layerMask = 1 << 6;
        Vector3 origin = SubmarineState.Instance.submarine.transform.position;
        Collider[] colliders = Physics.OverlapSphere(origin, radius, layerMask);
        foreach(Collider col in colliders) 
        {
            col.gameObject.GetComponent<FishAI>().enabled = true;
        }
    }
}
