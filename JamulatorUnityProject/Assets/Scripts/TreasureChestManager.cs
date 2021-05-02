using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestManager : MonoBehaviour
{


    /// <summary>
    /// Number of treasure chests picked up this game
    /// </summary>
    public int collectedTreasureCount;
    [SerializeField]
    GameObject treasureChestToSpawn;

    [SerializeField]
    List<GameObject> TreasureChestSpawnPoints;

    [SerializeField]
    int MaxChests = 1;

    int currentChestCount = 1;
    System.Random random = new System.Random();
    

    // Start is called before the first frame update
    void Start()
    {
        collectedTreasureCount = 0;
        SpawnChest();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            collectedTreasureCount++;

            GameObject.Destroy(other.gameObject);
            currentChestCount--;
            SpawnChest();
        }
    }

    private void SpawnChest()
    {
        //TODO: code to spawn a chest
        if (TreasureChestSpawnPoints?.Count > 0)
        {
            Debug.Log("Spawning chest. Current chest count: " + currentChestCount);
            if (currentChestCount < MaxChests)
            {
                currentChestCount++;
                int i = random.Next(TreasureChestSpawnPoints.Count);
                Transform transform = TreasureChestSpawnPoints[i].transform;
                Instantiate(treasureChestToSpawn, transform.position, transform.rotation);
            }
            else
            {
                Debug.Log("Max chests reached");
            }
        }
        else
        {
            Debug.Log("No treasure chest spawn points!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
