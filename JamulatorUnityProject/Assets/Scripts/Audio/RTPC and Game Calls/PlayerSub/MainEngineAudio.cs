using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEngineAudio : MonoBehaviour
{

    [Header("Sub Info")]
    [SerializeField] [Range(0, 100)] float speed;

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
    [SerializeField] float engineMin = 0f;
    [SerializeField] float engineOnThreshold = 5f;
    [SerializeField] float engineLowPowerMax = 50f;
    [SerializeField] float engineHighPowerMax = 100f;

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
        speed = 100f * AudioManager.Instance.subDriveEnergy;

        if (speed <= engineOnThreshold)
        {
            // from zero to a very low speed (idling)
            whirrGain.inputGain = AudioUtility.ScaleValue(speed, engineMin, engineOnThreshold, gainMin, gainLowPowerMin);
            engineWhirr.GetComponent<AudioSource>().pitch = 1f;

            humStaticGain.inputGain = AudioUtility.ScaleValue(speed, engineMin, engineOnThreshold, gainMin, gainLowPowerMin);
            engineHumStatic.GetComponent<AudioSource>().pitch = 1f;

            humRisingGain.inputGain = AudioUtility.ScaleValue(speed, engineMin, engineOnThreshold, gainMin, gainLowPowerMin);
            engineHumRising.GetComponent<AudioSource>().pitch = 1f;

            roarGain.inputGain = gainMin;

        }
        else
        {
            // anything above engineOnSpeed            

            if (speed < engineLowPowerMax)
            {
                // lower power
                whirrGain.inputGain = AudioUtility.ScaleValue(speed, engineOnThreshold, engineLowPowerMax, gainLowPowerMin, gainHighPowerMin);
                engineWhirr.GetComponent<AudioSource>().pitch = 1 + speed / 100;

                humStaticGain.inputGain = AudioUtility.ScaleValue(speed, engineOnThreshold, engineLowPowerMax, gainLowPowerMin, gainHighPowerMin);

                humRisingGain.inputGain = AudioUtility.ScaleValue(speed, engineOnThreshold, engineLowPowerMax, gainLowPowerMin, gainHighPowerMin);
                engineHumRising.GetComponent<AudioSource>().pitch = 1 + speed / 100;

                roarGain.inputGain = gainMin;
                engineRoar.GetComponent<AudioSource>().pitch = 1;

            }
            else
            {
                // high power
                whirrGain.inputGain = AudioUtility.ScaleValue(speed, engineLowPowerMax, engineHighPowerMax, gainHighPowerMin, gainMax);
                engineWhirr.GetComponent<AudioSource>().pitch = 1 + speed / 100;

                humStaticGain.inputGain = AudioUtility.ScaleValue(speed, engineLowPowerMax, engineHighPowerMax, gainHighPowerMin, gainMax);

                humRisingGain.inputGain = AudioUtility.ScaleValue(speed, engineLowPowerMax, engineHighPowerMax, gainHighPowerMin, gainMax);
                engineHumRising.GetComponent<AudioSource>().pitch = 1 + speed / 100;

                roarGain.inputGain = AudioUtility.ScaleValue(speed, engineLowPowerMax, engineHighPowerMax, gainMin, gainMax);
                engineRoar.GetComponent<AudioSource>().pitch = 1 - speed / 300;

            }
        }


    }
}
