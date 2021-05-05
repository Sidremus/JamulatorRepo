using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterAudio : MonoBehaviour
{
    // decides which audiosource will trigger the thruster sound depending on the direction of movement.

    [SerializeField] GameObject[] thrusters;
    float[] thrusterVols = { 0f, 0f, 0f, 0f, 0f };

    [Header("Internal Controls")]
    [SerializeField] bool UseInternalControl;
    [SerializeField] [Range(0, 100)] float speed;
    bool DirectionLeft;
    bool DirectionRight;
    bool DirectionUp;
    bool DirectionDown;
    bool DirectionBackwards;

    [SerializeField] float acceleration = 100f;
    [SerializeField] float deceleration = 200f;


    void GetSpeed()
    {
        /*subDriveEnergy = AudioManager.Instance.subDriveEnergy;*/        


        if (SubmarineState.Instance.zMoveState == Direction.BACKWARDS |
            SubmarineState.Instance.strafeState != Direction.NONE |
            SubmarineState.Instance.yMoveState != Direction.NONE)
        {
            speed += acceleration * Time.deltaTime;
        }
        else
        {
            speed -= deceleration * Time.deltaTime;
        }
        speed = Mathf.Clamp(speed, 0, 100);


    }
    private void Update()
    {
        GetSpeed();

        if (!UseInternalControl)
        {           
            DirectionLeft = SubmarineState.Instance.strafeState == Direction.LEFT;
            DirectionRight = SubmarineState.Instance.strafeState == Direction.RIGHT;
            DirectionUp = SubmarineState.Instance.yMoveState == Direction.UP;
            DirectionDown = SubmarineState.Instance.yMoveState == Direction.DOWN;
            DirectionBackwards = SubmarineState.Instance.zMoveState == Direction.BACKWARDS;
        }

        // 0 Left; 1 Right; 2 Top; 3 Bottom; 4 Front
        if (DirectionRight)
            thrusterVols[0] = 1;
        else thrusterVols[0] = 0;

        if (DirectionLeft)
            thrusterVols[1] = 1;
        else thrusterVols[1] = 0;

        if (DirectionDown)
            thrusterVols[2] = 1;
        else thrusterVols[2] = 0;

        if (DirectionUp)
            thrusterVols[3] = 1;
        else thrusterVols[3] = 0;

        if (DirectionBackwards)
            thrusterVols[4] = 1;
        else thrusterVols[4] = 0;

        // Set Vols based on direction and driveEnergy
        for (int i = 0; i < thrusters.Length; ++i)
        {
            var gainComp = thrusters[i].GetComponent<Gain>();
            gainComp.inputGain = AudioUtility.ConvertAtoDb(thrusterVols[i] * speed/100);

            var sourceComp = thrusters[i].GetComponent<AudioSource>();

            if (thrusterVols[i] == 0.0f)
            {
                sourceComp.Stop();
            }
            else
            {
                if (!sourceComp.isPlaying)
                {
                    sourceComp.Play();
                }
            }

        }
    }


}
