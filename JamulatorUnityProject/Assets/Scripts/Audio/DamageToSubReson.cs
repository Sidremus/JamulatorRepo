using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToSubReson : MonoBehaviour
{

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
    float _gainMin = -48f;
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

        subDamage = manager.subDamage;

        float damage = Mathf.Abs(subDamage) / subDamageRange;

        extGain = manager.extSubSoundsVol;
        float gainMin = _gainMin + extGain;
        float gainMax = _gainMax + extGain;

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
