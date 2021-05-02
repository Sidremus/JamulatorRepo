using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }


    public GameObject submarine;
    public GameObject listener;

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

    [SerializeField] GameObject[] rumble;
    [Range(-90, 12)] public float rumbleStartVol;

    [SerializeField] GameObject[] finWhale;
    [Range(-90, 12)] public float finWhaleStartVol;


    [Header("External Submarine Sounds")]
    [Range(-80, 0)] public float extSubSoundsVol;

    [Header("Submarine Collision")]
    [SerializeField] AudioClip[] collisionFX_Sub;
    [SerializeField] AudioClip[] collisionFX_Rock;
    [SerializeField] AudioClip[] collisionFX_Plant;
    [SerializeField] AudioClip[] collisionFX_SoftFauna;
    [SerializeField] AudioClip[] collisionFX_HardFauna;
    [SerializeField] GameObject AOCollisionPrefab;

    [Header("Submarine Actions: Sonar")]
    [SerializeField] [Range(-90, 12)] float sonarVol;
    [SerializeField] GameObject sonarOneShotObject;
    [SerializeField] AudioClip pingSound;
    [SerializeField] AudioClip findSound;
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

    float worldDepth = 800f;

    GameObject collisionsContainer;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        SetParams();

    }

    private void Start()
    {        
        SubscribeToEvents();
    }
    private void SetParams()
    {
        // Sends params from inspector to relevant gameobjects.
        bubbleVol += environmentalStartVol;

        rumbleStartVol += environmentalStartVol;
        finWhaleStartVol += environmentalStartVol;

        for (int i = 0; i < rumble.Length; ++i)
        {
            rumble[i].GetComponent<AudioSourceFader>().outputGain = rumbleStartVol;
        }

        for (int i = 0; i < finWhale.Length; ++i)
        {
            finWhale[i].GetComponent<AudioSourceFader>().outputGain = finWhaleStartVol;
        }

        collisionsContainer = new GameObject();
        collisionsContainer.name = "Collisions Container";
        collisionsContainer.transform.parent = this.transform;

    }


    private void Update()
    {
        if (!UseInternalControl)
        {
            subDepth = Mathf.Clamp((submarine.transform.position.y / worldDepth) * 100f, 0, 100);
            subSensorEnergy = SubmarineState.Instance.sensorEnergyLerp;
            subDriveEnergy = SubmarineState.Instance.driveEnergyLerp;
            subDamage = SubmarineState.Instance.subDamage;
        }


    }


    /// ******* EVENTS ******* ///

    private void SubscribeToEvents()
    {
        EventManager.Instance.onSonarPing += SonarPingFind;
        EventManager.Instance.onLightsOn += LightsOn;
        EventManager.Instance.onLightsOff += LightsOff;

    }
    private void OnDisable()
    {
        EventManager.Instance.onSonarPing -= SonarPingFind;
        EventManager.Instance.onLightsOn -= LightsOn;
        EventManager.Instance.onLightsOff -= LightsOff;
    }

    #region Sonar Ping Control
    
    void PingSonar()
    {
        sonarOneShotObject.GetComponent<Gain>().inputGain = sonarVol;
        sonarOneShotObject.GetComponent<AudioSource>().PlayOneShot(pingSound);
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

    #region Ping
    private void PingOn()
    {

    }

    private void PingOff()
    {

    }
    #endregion Ping

    #region Collision SFX

    public void PlayCollisionSound(Vector3 position, float impactMagnitude, GameObject other)
    {
        float sensitivity = 50f;
        // chooses clip, sets gain and pitch based on impactMagnitude, plays at position
        impactMagnitude = Mathf.Clamp(impactMagnitude, 0f, sensitivity) / sensitivity;

        var newAO = Instantiate(AOCollisionPrefab, position, Quaternion.identity);
        newAO.transform.parent = collisionsContainer.transform;

        // Attach source to new prefab, choose a clip depending on the tag of the other game object
        var source = newAO.GetComponent<AudioSource>();
        AudioClip[] collisionFX = new AudioClip[0];

        if (other.tag == "Submarine" || other.tag == "Wreck")
            collisionFX = collisionFX_Sub;
        else if (other.tag == "Plant")
            collisionFX = collisionFX_Plant;
        else if (other.tag == "Rock")
            collisionFX = collisionFX_Rock;
        else if (other.tag == "HardFauna")
            collisionFX = collisionFX_HardFauna;
        else if (other.tag == "SoftFauna")
            collisionFX = collisionFX_SoftFauna;
        else
        {
            Debug.Log("I've collided into something untagged.");
            return;
        }
        // if it's crashed into something untagged, it shouldn't make a sound.

        // clips are ordered in order of impact magnitude, where -01 is the lightest
        var clip = collisionFX[Mathf.RoundToInt(impactMagnitude * (collisionFX.Length - 1))];
        source.clip = clip;

        var gain = newAO.GetComponent<Gain>();
        gain.inputGain = 0f + (AudioUtility.ScaleValue(impactMagnitude, 0f, 1f, -24f, 0f));
                
        var pitch = Random.Range(0.85f, 1.15f) - (impactMagnitude / 2);
        source.pitch = pitch;

        source.Play();
        StartCoroutine(WaitThenDestroy(newAO, clip, pitch));

    }


    IEnumerator WaitThenDestroy(GameObject obj, AudioClip clip, float pitch)
    {
        float duration = clip.length / pitch;
        yield return new WaitForSeconds(duration);
        Destroy(obj);
    }
    #endregion

}
