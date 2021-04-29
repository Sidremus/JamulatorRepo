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
    [Range(0, 100)] public float subDriveEnergy;
    [Range(0, 100)] public float subEnergyLevel;
    

    [Header("Environmental Sounds Initialisation")]
    [SerializeField] [Range(-80, 0)] float environmentalStartVol;
    [SerializeField] [Range(0, 15)] float environmentalFadeUpTime;

    [SerializeField] GameObject[] bubbleMakers;
    [SerializeField] [Range(-100, 0)] public float bubbleVol;

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


    [Header("Submarine Actions: Sonar")]
    [SerializeField] [Range(-80, 0)] float sonarVol;
    [SerializeField] GameObject sonarOneShotObject;
    [SerializeField] AudioClip pingSound;
    [SerializeField] AudioClip findSound;
    [SerializeField] GameObject sonarLoop;
    bool pingStart;
    bool pingFind;
    bool isPinging;

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
            // subDamage =
            subDriveEnergy = SubmarineState.Instance.driveEnergyLerp;            
        }

        PingBoolCtrl();

    }


    /// ******* EVENTS ******* ///

    private void SubscribeToEvents()
    {
        EventManager.Instance.onSonarPing += SonarPingStart;

    }
    private void OnDisable()
    {
        EventManager.Instance.onSonarPing += SonarPingStart;
    }

    #region Sonar Ping Control
    void PingBoolCtrl()
    {
        if (pingStart)
        {
            pingStart = false;
            SonarPingStart();
        }

        if (pingFind)
        {
            pingFind = false;
            SonarPingFind();
        }
    }
    void SonarPingStart()
    {
        if (isPinging)
        {
            Debug.Log("isPingin = true; stopping coroutine");
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
            Debug.Log("loopsource.isplaying; stopping..");
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

   






}
