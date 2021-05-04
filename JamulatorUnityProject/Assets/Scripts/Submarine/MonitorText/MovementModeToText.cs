using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementModeToText : MonoBehaviour
{
   
    Text text;
    ControlMode currentControlMode;

    private void Start()
    {
        text = GetComponent<Text>();
       

    }

    private void Update()
    {
        currentControlMode = SubmarineState.Instance.interfaceMode;

        if (currentControlMode == ControlMode.INTERFACE)
            text.text = "Movement Mode is STATIC \n (Toggle: Right Click)";
        else if (currentControlMode == ControlMode.STEERING)
            text.text = "Movement Mode is FREE \n (WASDQE Keys & Mouse)";

    }


}
