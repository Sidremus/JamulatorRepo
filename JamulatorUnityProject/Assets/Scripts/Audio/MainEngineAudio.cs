using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEngineAudio : MonoBehaviour
{
    [SerializeField] AudioManager manager;
    [SerializeField] RigidbodyVelocityToAudio rvta;

    [Header("Controlled Objects")]

    [SerializeField] GameObject engineWhirr;
    Gain whirrGain;
        
    [SerializeField] GameObject engineHumStatic;
    Gain humStaticGain;

    [SerializeField] GameObject engineHumRising;
    Gain humRisingGain;

    [SerializeField] GameObject engineRoar;
    Gain roarGain;


    [Header("Variables")]
    [SerializeField] [Range(0f, 100f)] float engineSpeed;

    // todo: connect these values to game values //

    [SerializeField] float engineMin = 0f;
    [SerializeField] float engineOnSpeed = 5f;
    [SerializeField] float engineLowPowerSpeedMax = 50f;
    [SerializeField] float engineHighPowerSpeedMax = 100f;

    [SerializeField] float gainMin = -60f;
    [SerializeField] float gainLowPowerMin = -24f;
    [SerializeField] float gainHighPowerMin = -12f;
    [SerializeField] float gainMax = 0f;

    private void Start()
    {
        roarGain = engineRoar.GetComponent<Gain>();
        humStaticGain = engineHumStatic.GetComponent<Gain>();
        humRisingGain = engineHumRising.GetComponent<Gain>();
        whirrGain = engineWhirr.GetComponent<Gain>();

        
    }

    void Update()
    {

        // grab enginespeed from velocity eg  float speed = rvta.velocityZ; //

        float speed = engineSpeed;

        Debug.Log(speed);

        if (speed <= engineOnSpeed)
        {
            // from zero to a very low speed (idling)
            whirrGain.inputGain = AudioUtility.ScaleValue(speed, engineMin, engineOnSpeed, gainMin, gainLowPowerMin);
            engineWhirr.GetComponent<AudioSource>().pitch = 1f;

            humStaticGain.inputGain = AudioUtility.ScaleValue(speed, engineMin, engineOnSpeed, gainMin, gainLowPowerMin);
            engineHumStatic.GetComponent<AudioSource>().pitch = 1f;

            humRisingGain.inputGain = AudioUtility.ScaleValue(speed, engineMin, engineOnSpeed, gainMin, gainLowPowerMin);
            engineHumRising.GetComponent<AudioSource>().pitch = 1f;

            roarGain.inputGain = gainMin;

        }
        else
        {
            // anything above engineOnSpeed            

            if (speed < engineLowPowerSpeedMax)
            {
                // lower power
                whirrGain.inputGain = AudioUtility.ScaleValue(speed, engineOnSpeed, engineLowPowerSpeedMax, gainLowPowerMin, gainHighPowerMin);
                engineWhirr.GetComponent<AudioSource>().pitch = 1 + speed / 100;

                humStaticGain.inputGain = AudioUtility.ScaleValue(speed, engineOnSpeed, engineLowPowerSpeedMax, gainLowPowerMin, gainHighPowerMin);

                humRisingGain.inputGain = AudioUtility.ScaleValue(speed, engineOnSpeed, engineLowPowerSpeedMax, gainLowPowerMin, gainHighPowerMin);
                engineHumRising.GetComponent<AudioSource>().pitch = 1 + speed / 100;

                roarGain.inputGain = gainMin;
                engineRoar.GetComponent<AudioSource>().pitch = 1;

            }
            else
            {
                // high power
                whirrGain.inputGain = AudioUtility.ScaleValue(speed, engineLowPowerSpeedMax, engineHighPowerSpeedMax, gainHighPowerMin, gainMax);
                engineWhirr.GetComponent<AudioSource>().pitch = 1 + speed / 100;

                humStaticGain.inputGain = AudioUtility.ScaleValue(speed, engineLowPowerSpeedMax, engineHighPowerSpeedMax, gainHighPowerMin, gainMax);

                humRisingGain.inputGain = AudioUtility.ScaleValue(speed, engineLowPowerSpeedMax, engineHighPowerSpeedMax, gainHighPowerMin, gainMax);
                engineHumRising.GetComponent<AudioSource>().pitch = 1 + speed / 100;

                roarGain.inputGain = AudioUtility.ScaleValue(speed, engineLowPowerSpeedMax, engineHighPowerSpeedMax, gainMin, gainMax);
                engineRoar.GetComponent<AudioSource>().pitch = 1 - speed / 300;

            }
        }


    }
}
