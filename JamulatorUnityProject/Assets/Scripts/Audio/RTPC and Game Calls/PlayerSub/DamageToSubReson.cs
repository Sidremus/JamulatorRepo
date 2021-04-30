using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToSubReson : MonoBehaviour
{


    [SerializeField] GameObject resonLight;
    Gain lightGain;
    [SerializeField] GameObject resonMedium;
    Gain mediumGain;
    [SerializeField] GameObject resonHeavy;
    Gain heavyGain;

    [SerializeField] [Range(0f, 100f)] float subDamage;

    float subDamageRange = 100f;

    float extGain;
    float _gainMin = -39f;
    float _gainMax = 0f;

    float lightDamageMin = 0.1f;
    float mediumDamageMin = 0.3f;
    float heavyDamageMin = 0.8f;

    // Start is called before the first frame update
    void Start()
    {        
        lightGain = resonLight.GetComponent<Gain>();
        mediumGain = resonMedium.GetComponent<Gain>();
        heavyGain = resonHeavy.GetComponent<Gain>();

    }

    // Update is called once per frame
    void Update()
    {

        subDamage = AudioManager.Instance.subDamage;

        float damage = Mathf.Abs(subDamage) / subDamageRange;

        extGain = AudioManager.Instance.extSubSoundsVol;
        float gainMin = _gainMin + extGain;
        float gainMax = _gainMax + extGain;

        if (damage < lightDamageMin)
        {
            lightGain.inputGain = AudioUtility.ScaleValue(damage, 0f, lightDamageMin, gainMin, gainMax);
            mediumGain.inputGain = gainMin;
            heavyGain.inputGain = gainMin;
        }

        else if (damage >= lightDamageMin && damage < mediumDamageMin)
        {
            //Light Damage
            lightGain.inputGain = gainMax;
            mediumGain.inputGain = AudioUtility.ScaleValue(damage, lightDamageMin, mediumDamageMin, gainMin, gainMax);
            heavyGain.inputGain = gainMin;
        }

        else if (damage >= mediumDamageMin && damage < heavyDamageMin)
        {
            //Medium Damage
            lightGain.inputGain = gainMax;
            mediumGain.inputGain = gainMax;
            heavyGain.inputGain = gainMin;
        }

        else if (damage >= heavyDamageMin)
        {
            lightGain.inputGain = AudioUtility.ScaleValue(damage, mediumDamageMin, heavyDamageMin, gainMax, gainMin);
            mediumGain.inputGain = gainMax;
            heavyGain.inputGain = AudioUtility.ScaleValue(damage, heavyDamageMin, 1f, gainMin, gainMax);
        }




    }
}
