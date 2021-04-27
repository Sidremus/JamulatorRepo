using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static float Remap(float iMin, float iMax, float oMin, float oMax, float value)
    {
        float t = Mathf.InverseLerp(iMin, iMax, value);
        return Mathf.Lerp(oMin, oMax, t);
    }
}
