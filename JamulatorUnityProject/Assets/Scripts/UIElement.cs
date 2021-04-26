using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIElementType
{
    STEERINGLERP,
    DRIVEENERGYLERP,
    SEONSORENERGYLERP,
    CLICKBUTTON,
    TOGGLEBUTTON
}

public class UIElement : MonoBehaviour
{
    private bool isActive;
    public UIElementType uIElementType;

    public void LeftClickEnter()
    {
        Debug.Log("ping");
        isActive = true;
    }
    public void LeftClickExit()
    {
        Debug.Log("pong");
        isActive = false;
    }

    private void Update()
    {
        if (isActive)
        {

        }
    }
}
