using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private SubmarineState SubState;


    [Header("Environmental Sounds Initialisation")]
    [SerializeField] [Range(-80, 0)] float environmentalStartVol;
    [SerializeField] [Range(0, 15)] float environmentalFadeUpTime;

    [SerializeField] GameObject[] bubbleMakers;
    [SerializeField] [Range(-80, 0)] float bubbleStartVol;
    GameObject bubblemaker_surface;
    GameObject bubblemaker_regular;
    GameObject bubblemaker_deep;

    [SerializeField] GameObject rumble;
    AudioSourceFader ASFrumble;
    [SerializeField] [Range(-80, 0)] float rumbleStartVol;

    [SerializeField] GameObject finWhale;
    AudioSourceFader ASFfin;
    [SerializeField] [Range(-80, 0)] float finWhaleStartVol;

    [SerializeField] GameObject clicker;
    AudioSourceFader ASFclicker;
    [SerializeField] [Range(-80, 0)] float clickerStartVol;


    [Header("External Submarine Sounds")]
    [SerializeField] [Range(-80, 0)] float extSubSoundsVol;

    [SerializeField] GameObject[] subResonance;
    GameObject subReson_reson;
    GameObject subReson_tuned;
    GameObject subReson_creak;

    [SerializeField] GameObject subCreaker;
    
    [SerializeField] [Range(-100, 0)] private float subDepth;
    [SerializeField] [Range(0, 100)] private float subDamage;





    private void Start()
    {
        SubState = SubmarineState.Instance;
        FindScriptsAndGameObjects();
        SetParams();
    }

    private void FindScriptsAndGameObjects()
    {

        // Audiosource Fader scripts

        ASFrumble = rumble.GetComponent<AudioSourceFader>();
        ASFfin = finWhale.GetComponent<AudioSourceFader>();
        ASFclicker = clicker.GetComponent<AudioSourceFader>();

        subReson_reson = subResonance[0];
        subReson_tuned = subResonance[1];
        subReson_creak = subResonance[2];



        // Bubbils
        bubblemaker_surface = bubbleMakers[0];
        bubblemaker_regular = bubbleMakers[1];
        bubblemaker_deep = bubbleMakers[2];


    }


    private void SetParams()
    {
        // Sends params from inspector to relevant gameobjects.

        bubbleStartVol += environmentalStartVol;
        rumbleStartVol += environmentalStartVol;
        finWhaleStartVol += environmentalStartVol;
        clickerStartVol += environmentalStartVol;

        for (int i = 0; i < bubbleMakers.Length; ++i)
        {
            foreach (GameObject obj in bubbleMakers[i].GetComponent<DistributeAudioObjects>().createdAudioObjects)
            {
                obj.GetComponent<AudioSourceFader>().FadeTo(bubbleStartVol, environmentalFadeUpTime, 0.5f);
            }
        }

        ASFrumble.outputGain = rumbleStartVol;
        ASFrumble.UpdateAudioSourceAmplitude();

        ASFfin.outputGain = finWhaleStartVol;
        ASFfin.UpdateAudioSourceAmplitude();

        ASFclicker.outputGain = clickerStartVol;
        ASFclicker.UpdateAudioSourceAmplitude();


    }

    private void Update()
    {
        SetFromDamage();
        //DepthControl();
    }

    void SetFromDepth()
    {
        //subDepth = SubState.

        float subDepthRange = 100;
        float _subDepth = Mathf.Abs(subDepth) / subDepthRange;


        if (_subDepth < 1f/3f)
        {
            //Surface


        }        
        else if (_subDepth > 2f/3f)
        {
            //Deep
            
        }
        else
        {
            //Middle
            
        }
        


        
        /*foreach (GameObject obj in bubblemaker_surface.GetComponent<DistributeAudioObjects>().createdAudioObjects)
        {
            
        }*/

    }

    void SetFromDamage()
    {
        float subDamageRange = 100;
        float _subDamage = subDamage / subDamageRange;


        if (_subDamage > 0.3f && _subDamage < 0.8f)
        {
            if (subReson_tuned.GetComponent<AudioSource>().isPlaying) return;
            subReson_tuned.GetComponent<AudioSource>().Play();
            subReson_tuned.GetComponent<AudioSourceFader>().FadeUp(2);
        }

        if (_subDamage > 0.8f)
        {
            if (subReson_creak.GetComponent<AudioSource>().isPlaying) return;
            subReson_creak.GetComponent<AudioSourcePlayer>().PlayLoopWithInterval();
            subReson_creak.GetComponent<AudioSourceFader>().FadeUp(2);
        }








        /*if (_subDamage == 0.0f)
        {
            Debug.Log("No damage");
        }
        else if (_subDamage > 0.0f && _subDamage < 1f/ 3f)
        {
            Debug.Log("Light damage: " + _subDamage);
        }
        else if (_subDamage > 1f/ 3f && _subDamage < 2f/3f)
        {
            Debug.Log("Medium damage: " + _subDamage);
        }
        else if (_subDamage > 2f/3f && _subDamage < 4f/5f)
        {
            Debug.Log("Heavy damage");
        }
*/

    }





}
