using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }


    public GameObject submarine;

    [Header("Global Messages")]
    [SerializeField] bool UseInternalControl;
    [Range(-100, 0)] public float subDepth;
    [Range(0, 100)] public float subDamage;
    [Range(0, 1)] public float subDriveEnergy;
    [Range(0, 1)] public float subSensorEnergy;
    

    [Header("Environmental Sounds Initialisation")]
    [SerializeField] [Range(-90, 12)] float environmentalStartVol;
    [SerializeField] [Range(0, 15)] float environmentalFadeUpTime;

    [SerializeField] GameObject[] bubbleMakers;
    [Range(-100, 0)] public float bubbleVol;

    [SerializeField] GameObject rumble;
    AudioSourceFader ASFrumble;
    [SerializeField] [Range(-90, 12)] float rumbleStartVol;

    [SerializeField] GameObject finWhale;
    AudioSourceFader ASFfin;
    [SerializeField] [Range(-90, 12)] float finWhaleStartVol;

    [SerializeField] GameObject clicker;
    AudioSourceFader ASFclicker;
    [SerializeField] [Range(-90, 12)] float clickerStartVol;

    [Header("External Submarine Sounds")]
    [Range(-80, 0)] public float extSubSoundsVol;

    [Header("Submarine Actions: Sonar")]
    [SerializeField] [Range(-90, 12)] float sonarVol;
    [SerializeField] GameObject sonarOneShotObject;
    [SerializeField] AudioClip pingSound;
    [SerializeField] AudioClip findSound;
    [SerializeField] GameObject sonarLoop;
    bool isPinging;

    [Header("Submarine Actions: Lights")]
    [SerializeField] [Range(-90, 12)] float lightsVol;
    [SerializeField] GameObject lightObject;
    [SerializeField] AudioClip[] lightSounds; // 0: lights low on; 1: lights low off; 2 lights high on; 3 lights high off

    float highPowerThreshold = 0.6f;
    bool highPower;

    [Header("Cockpit UI")]
    [Range(-80, 0)] public float UIVol;


    // Sonar ping duration controls //
    float sonarHumPreWait = 0.8f;
    float sonarHumFadeTime = 16f;
    float sonarFindPitchRandRange = 0.01f;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

    }

    private void Start()
    {
        FindScriptsAndGameObjects();
        SetParams();

        SubscribeToEvents();

    }
    private void FindScriptsAndGameObjects()
    {

        // Audiosource Fader scripts

        ASFrumble = rumble.GetComponent<AudioSourceFader>();
        ASFfin = finWhale.GetComponent<AudioSourceFader>();
        ASFclicker = clicker.GetComponent<AudioSourceFader>();


    }
    private void SetParams()
    {
        // Sends params from inspector to relevant gameobjects.

        bubbleVol += environmentalStartVol;
        rumbleStartVol += environmentalStartVol;
        finWhaleStartVol += environmentalStartVol;
        clickerStartVol += environmentalStartVol;

        ASFrumble.outputGain = rumbleStartVol;
        ASFrumble.UpdateAudioSourceAmplitude();

        ASFfin.outputGain = finWhaleStartVol;
        ASFfin.UpdateAudioSourceAmplitude();

        ASFclicker.outputGain = clickerStartVol;
        ASFclicker.UpdateAudioSourceAmplitude();


    }


    private void Update()
    {
        if (!UseInternalControl)
        {
            subDepth = submarine.transform.position.y;
            subSensorEnergy = SubmarineState.Instance.sensorEnergyLerp;
            subDriveEnergy = SubmarineState.Instance.driveEnergyLerp;            
        }


    }


    /// ******* EVENTS ******* ///

    private void SubscribeToEvents()
    {
        EventManager.Instance.onSonarPing += SonarPingStart;
        EventManager.Instance.onLightsOn += LightsOn;
        EventManager.Instance.onLightsOff += LightsOff;

    }
    private void OnDisable()
    {
        EventManager.Instance.onSonarPing -= SonarPingStart;
        EventManager.Instance.onLightsOn -= LightsOn;
        EventManager.Instance.onLightsOff -= LightsOff;
    }

    #region Sonar Ping Control
    // TODO: something in the fade control isn't cancelling right and there's pops/jumps in volume sometimes, especially when sonar pings are triggered at quick intervals.
    
    void SonarPingStart()
    {
        if (isPinging)
        {            
            StartCoroutine(FadeAndStop(sonarLoop));
            StopCoroutine(PlaySonarPing());
        }
            
        
        StartCoroutine(PlaySonarPing());

    }

    IEnumerator FadeAndStop(GameObject audioObject)
    {
        var loopASF = audioObject.GetComponent<AudioSourceFader>();
        var loopSource = audioObject.GetComponent<AudioSource>();

        loopASF.FadeTo(-80f, 0.1f, 0.9f);
        yield return new WaitForSeconds(0.1f);
        loopSource.Stop();
    }

    IEnumerator PlaySonarPing()
    {
        isPinging = true;
        // Play ping sound
        sonarOneShotObject.GetComponent<Gain>().inputGain = sonarVol;
        sonarOneShotObject.GetComponent<AudioSource>().PlayOneShot(pingSound);
        
        var loopSource = sonarLoop.GetComponent<AudioSource>();
        var loopASF = sonarLoop.GetComponent<AudioSourceFader>();

        // Fade it down if it's already playing
        if (loopSource.isPlaying)
        {
            loopASF.FadeTo(-80f, 0.1f, 0.9f);
            yield return new WaitForSeconds(0.1f);
            loopSource.Stop();
        }

        // Play & fade up sonar hum loop
        loopSource.Play();
        loopASF.FadeTo(sonarVol, sonarHumPreWait, 0.8f);

        // Wait, then fade down and stop sonar hum loop
        yield return new WaitForSeconds(sonarHumPreWait);
        loopASF.FadeTo(-80f, sonarHumFadeTime, 0.1f);
        yield return new WaitForSeconds(sonarHumFadeTime);
        loopSource.Stop();

        isPinging = false;
        yield break;


    }
    void SonarPingFind()
    {
        sonarOneShotObject.GetComponent<Gain>().inputGain = sonarVol;
        sonarOneShotObject.GetComponent<AudioSource>().pitch = 1 + Random.Range(-sonarFindPitchRandRange, sonarFindPitchRandRange);
        sonarOneShotObject.GetComponent<AudioSource>().PlayOneShot(findSound);
    }
    #endregion

    #region Lights Control
    private void LightsOn()
    {
        if (subSensorEnergy >= highPowerThreshold)
            highPower = true;
        else highPower = false;
       
        var source = lightObject.GetComponent<AudioSource>();
        var gain = lightObject.GetComponent<Gain>();
        
        if (highPower)
        {
            gain.inputGain = lightsVol +  AudioUtility.ScaleValue(subSensorEnergy, highPowerThreshold, 1f, -24f, 0f);
            source.PlayOneShot(lightSounds[2]);
        }
             
        else
        {
            gain.inputGain = lightsVol + AudioUtility.ScaleValue(subSensorEnergy, 0, highPowerThreshold, -24f, 0f);
            source.PlayOneShot(lightSounds[0]);
        }

    }

    private void LightsOff()
    {
        if (subSensorEnergy >= highPowerThreshold)
            highPower = true;
        else highPower = false;

        var source = lightObject.GetComponent<AudioSource>();
        var gain = lightObject.GetComponent<Gain>();
        gain.inputGain = lightsVol;

        if (highPower)
        {
            gain.inputGain = lightsVol + AudioUtility.ScaleValue(subSensorEnergy, highPowerThreshold, 1f, -24f, 0f);
            source.PlayOneShot(lightSounds[3]);
        }
           
        else
        {
            gain.inputGain = lightsVol + AudioUtility.ScaleValue(subSensorEnergy, 0, highPowerThreshold, -24f, 0f);
            source.PlayOneShot(lightSounds[1]);
        }
           

    }
    #endregion Lights



}
