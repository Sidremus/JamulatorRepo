using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepthToText : MonoBehaviour
{

    [SerializeField] float currentDepth;
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        currentDepth = Mathf.RoundToInt(SubmarineState.Instance.submarine.transform.position.y);
        text.text = currentDepth + "m";
    }


}
