using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private SubmarineState SubState;
    public GameObject submarine;

    [Header("Global Messages")]
    [SerializeField] bool UseInternalControl;
    [Range(-100, 0)] public float subDepth;
    [Range(0, 100)] public float subDamage;
    [Range(0, 100)] public float subDriveEnergy;
    [Range(0, 100)] public float subEnergyLevel;

    public bool isInCave;

    [Header("Environmental Sounds Initialisation")]
    [SerializeField] [Range(-80, 0)] float environmentalStartVol;
    [SerializeField] [Range(0, 15)] float environmentalFadeUpTime;

    [SerializeField] GameObject[] bubbleMakers;
    [SerializeField] [Range(-100, 0)] public float bubbleVol;
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
    [SerializeField] [Range(-80, 0)] public float extSubSoundsVol;




    private void Update()
    {
        if (!UseInternalControl)
        {
            subDepth = submarine.transform.position.y;
            // subDamage =
            subDriveEnergy = SubmarineState.Instance.driveEnergyLerp;
        }
        
        


    }


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



        // Bubbils
        bubblemaker_surface = bubbleMakers[0];
        bubblemaker_regular = bubbleMakers[1];
        bubblemaker_deep = bubbleMakers[2];


    }


    private void SetParams()
    {
        // Sends params from inspector to relevant gameobjects.

        bubbleVol += environmentalStartVol;
        rumbleStartVol += environmentalStartVol;
        finWhaleStartVol += environmentalStartVol;
        clickerStartVol += environmentalStartVol;

       /* for (int i = 0; i < bubbleMakers.Length; ++i)
        {
            foreach (GameObject obj in bubbleMakers[i].GetComponent<DistributeAudioObjects>().createdAudioObjects)
            {
                obj.GetComponent<AudioSourceFader>().FadeTo(bubbleStartVol, environmentalFadeUpTime, 0.5f);
            }
        }*/

        ASFrumble.outputGain = rumbleStartVol;
        ASFrumble.UpdateAudioSourceAmplitude();

        ASFfin.outputGain = finWhaleStartVol;
        ASFfin.UpdateAudioSourceAmplitude();

        ASFclicker.outputGain = clickerStartVol;
        ASFclicker.UpdateAudioSourceAmplitude();


    }



}
