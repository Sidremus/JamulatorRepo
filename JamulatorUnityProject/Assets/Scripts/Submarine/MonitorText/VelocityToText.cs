using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VelocityToText : MonoBehaviour
{
    
    [SerializeField] float currentVelocity;
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        currentVelocity = Mathf.RoundToInt(SubmarineState.Instance.submarine.GetComponent<Rigidbody>().velocity.magnitude);
        text.text = currentVelocity + "m/s";
    }



}
