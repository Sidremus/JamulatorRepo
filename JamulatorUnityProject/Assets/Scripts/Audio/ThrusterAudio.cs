using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterAudio : MonoBehaviour
{
    // decides which audiosource will trigger the thruster sound depending on the direction of movement.

    [SerializeField] AudioManager manager;
    [SerializeField] RigidbodyVelocityToMovementAudio rvta;

    [SerializeField] GameObject[] thrusters;
    [SerializeField] GameObject thrusterLeft;
    [SerializeField] GameObject thrusterRight;
    [SerializeField] GameObject thrusterTop;
    [SerializeField] GameObject thrusterBottom;




    private void Update()
    {
        CheckMovementState();
        SetThrustersVolume();

    }

    void SetThrustersVolume()
    {
        for (int i = 0; i < thrusters.Length; ++i)
        {
            var gain = thrusters[i].GetComponent<Gain>();

            gain.inputGain = 0f; // add speed --> vol/pitch stage.

        }


    }

    void PlayAudioSourceOnGameObject(GameObject go, bool on)
    {
        var source = go.GetComponent<AudioSource>();
        

        if (on)
        {
            if (source.isPlaying) return;
            source.Play();
        }
        else if (!on)
        {
            if (!source.isPlaying) return;
            source.Stop();
        }

            
    }



    void CheckMovementState()
    {
        if (SubmarineState.Instance.yMoveState == Direction.UP)
        {
            PlayAudioSourceOnGameObject(thrusterBottom, true);
            PlayAudioSourceOnGameObject(thrusterTop, false);
        }
        else if (SubmarineState.Instance.yMoveState == Direction.DOWN)
        {
            PlayAudioSourceOnGameObject(thrusterTop, true);
            PlayAudioSourceOnGameObject(thrusterBottom, false);
        }

        if (SubmarineState.Instance.strafeState == Direction.LEFT)
        {
            PlayAudioSourceOnGameObject(thrusterRight, true);
            PlayAudioSourceOnGameObject(thrusterLeft, false);
        }
        else if (SubmarineState.Instance.yMoveState == Direction.RIGHT)
        {
            PlayAudioSourceOnGameObject(thrusterLeft, true);
            PlayAudioSourceOnGameObject(thrusterRight, false);

        }
    }




}
