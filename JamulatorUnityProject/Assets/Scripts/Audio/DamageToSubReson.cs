using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToSubReson : MonoBehaviour
{

    private SubmarineState SubState;
    [SerializeField] AudioManager manager;

    [SerializeField] GameObject resonLight;
    Gain lightGain;
    [SerializeField] GameObject resonMedium;
    Gain mediumGain;
    [SerializeField] GameObject resonHeavy;
    Gain heavyGain;

    [SerializeField] [Range(0f, 100f)] float subDamage;

    float subDamageRange = 100f;

    float extGain;
    float gainMin = -36f;
    float gainMax = 0f;

    float lightDamageMin = 0.1f;
    float mediumDamageMin = 0.3f;
    float heavyDamageMin = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        SubState = SubmarineState.Instance;
        extGain = manager.extSubSoundsVol;

        lightGain = resonLight.GetComponent<Gain>();
        mediumGain = resonMedium.GetComponent<Gain>();
        heavyGain = resonHeavy.GetComponent<Gain>();

    }

    // Update is called once per frame
    void Update()
    {
        float damage = Mathf.Abs(subDamage) / subDamageRange;

        if (damage < lightDamageMin)
        {
            lightGain.inputGain = AudioUtility.ConvertRange(0f, lightDamageMin, gainMin, gainMax, damage);
            mediumGain.inputGain = gainMin;
            heavyGain.inputGain = gainMin;
        }

        else if (damage >= lightDamageMin && damage <= mediumDamageMin)
        {
            //Light Damage
            lightGain.inputGain = gainMax;
            mediumGain.inputGain = AudioUtility.ConvertRange(lightDamageMin, mediumDamageMin, gainMin, gainMax, damage);
            heavyGain.inputGain = gainMin;
        }

        else if (damage >= mediumDamageMin && damage <= heavyDamageMin)
        {
            //Medium Damage
            lightGain.inputGain = AudioUtility.ConvertRange(mediumDamageMin, heavyDamageMin, gainMax, gainMin, damage);
            mediumGain.inputGain = gainMax;
            heavyGain.inputGain = gainMin;
        }

        else if (damage <= heavyDamageMin)
        {
            lightGain.inputGain = gainMin;
            mediumGain.inputGain = gainMax;
            heavyGain.inputGain = AudioUtility.ConvertRange(heavyDamageMin, 1f, gainMin, gainMax, damage);
        }




    }
}
