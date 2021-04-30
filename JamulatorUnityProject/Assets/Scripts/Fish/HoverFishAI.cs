using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverFishAI : WanderFishAI
{
    private void OnEnable() 
    {
        base.moveSpeed = 5f;
        base.minMoveSpeed = 5f;
    }
}
