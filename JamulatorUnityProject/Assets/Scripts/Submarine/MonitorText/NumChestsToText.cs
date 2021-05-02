using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumChestsToText : MonoBehaviour
{
    GameObject[] treasureChests;
    Text text;
    int chestCount;

    private void Start()
    {
        text = GetComponent<Text>();
        chestCount = SubmarineState.Instance.submarine.GetComponent<TreasureChestManager>().collectedTreasureCount;
    }
    private void Update()
    {
        text.text = chestCount.ToString();
    }



}
