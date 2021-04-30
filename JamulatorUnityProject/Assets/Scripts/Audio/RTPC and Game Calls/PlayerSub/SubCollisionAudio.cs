using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCollisionAudio : MonoBehaviour
{
    // currently a singleton but plan is to integrate it into main scene audiomanager

   
    [SerializeField] AudioClip[] collisionFX;
    [SerializeField] GameObject AOCollisionPrefab;

    private static SubCollisionAudio _instance;
    public static SubCollisionAudio Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        //DontDestroyOnLoad(this.gameObject);

    }

    public void PlayCollisionSound(Vector3 position, float impactMagnitude)
    {
        // chooses clip based on impactMagnitude, gain also based on that, at position
        impactMagnitude = Mathf.Clamp(impactMagnitude, 0f, 5f) / 5f;

        var newAO = Instantiate(AOCollisionPrefab, position, Quaternion.identity);
        newAO.transform.parent = transform;

        var source = newAO.GetComponent<AudioSource>();
        var clip = collisionFX[Mathf.RoundToInt(impactMagnitude * (collisionFX.Length - 1))];
        source.clip = clip;

        newAO.GetComponent<Gain>().inputGain = 0f - ((1f - impactMagnitude) * 24f);
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

}
