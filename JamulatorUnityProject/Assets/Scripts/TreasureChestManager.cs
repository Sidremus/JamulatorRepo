using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    List<GameObject> SpawnedChests;
    int currentChestCount = 1;
    System.Random random = new System.Random();
    

    // Start is called before the first frame update
    void Start()
    {
        SpawnedChests = new List<GameObject>();
        collectedTreasureCount = 0;
        if (TreasureChestSpawnPoints.Count == 0)
        {
            PopulateSpawnPointsByTag();
            Debug.Log($"Found {TreasureChestSpawnPoints.Count} spawn points for treasure");
        }
        SpawnChest();
    }

    private void PopulateSpawnPointsByTag()
    {
        TreasureChestSpawnPoints = GameObject.FindGameObjectsWithTag("TreasureSpawn").ToList();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            collectedTreasureCount++;
            if (SpawnedChests.Contains(other.gameObject))
            {
                SpawnedChests.Remove(other.gameObject);
            }
            GameObject.Destroy(other.gameObject);
            currentChestCount--;
            SpawnChest();
        }
    }
    public GameObject GetClosestChest(Vector3 position)
    {
        GameObject closestChest=null;
        float closestDistance = float.MaxValue;
        foreach(var chest in SpawnedChests)
        {
            var distance = Vector3.Distance(position, chest.transform.position);
            if (distance < closestDistance)
            {
                closestChest = chest;
            }
        }
        return closestChest;
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
                var chest = Instantiate(treasureChestToSpawn, transform.position, transform.rotation);
                SpawnedChests.Add(chest);
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
