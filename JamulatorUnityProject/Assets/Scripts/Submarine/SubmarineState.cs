using UnityEngine;

public enum Direction
{
    NONE,
    FORWARDS,
    BACKWARDS,
    LEFT,
    RIGHT,
    UP,
    DOWN
}

public enum ControlMode
{
    INTERFACE,
    STEERING
}

public class SubmarineState : MonoBehaviour
{
    private static SubmarineState _instance;
    public static SubmarineState Instance { get { return _instance; } }

    public GameObject submarine;

    // These state values represent the movement state of the sub
    private bool _isMoving;
    public bool isMoving { get { return _isMoving; } }

    private Direction _zMoveState;
    public Direction zMoveState { get { return _zMoveState; } }

    private Direction _yMoveState;
    public Direction yMoveState { get { return _yMoveState; } }

    private Direction _strafeState;
    public Direction strafeState { get { return _strafeState; } }

    private ControlMode _interfaceMode;
    public ControlMode interfaceMode { get { return _interfaceMode; } }

    public bool outOfBounds { get; set; }

    // Total damage of the sub from 0-100.

    public float subDamage { get; set; }
    float repairRate = 0.2f;
    public bool inCave { get; set; }
    private InCaveTrigger lastCaveTrigger;



    // These floats are the energy states of the drive and sensor systems. 0 = lowest, 1 = highest
    private float _driveEnergyLerp;
    public float driveEnergyLerp { get { return _driveEnergyLerp; } set { _driveEnergyLerp = Mathf.Clamp(value, 0, 1); } }
    private float _sensorEnergyLerp;
    public float sensorEnergyLerp { get { return _sensorEnergyLerp; } set { _sensorEnergyLerp = Mathf.Clamp(value, 0, 1); } }

    // Subsystem values
    public float fuel { get; set; }
    public bool lightsOn { get; private set; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);


        // set sub health to 100%
        subDamage = 0f;
    }


    private void Update()
    {
        if (subDamage > 0f)
        {
            SelfRepair();
        }
    }

    private void SelfRepair()
    {        
        subDamage -= Time.deltaTime * repairRate;
    }

    private void FixedUpdate()
    {
        CheckIsMoving();
    }
    private void CheckIsMoving()
    {
        _isMoving =
          !(
            _zMoveState == Direction.NONE &&
            _yMoveState == Direction.NONE &&
            _strafeState == Direction.NONE
          );
    }


    /**
     * Called directly from the controller class which reacts to button presses.
     * Decides whether able to take the new incoming direction.
     * @dir the incoming direction to take
     */
    public void SetZMoveState(Direction dir)
    {
        switch (dir)
        {
            case Direction.FORWARDS:
                // Cannot go forwards if currently going backwards
                if (_zMoveState == Direction.BACKWARDS)
                {
                    return;
                }
                break;
            case Direction.BACKWARDS:
                // Cannot go backwards if currently going forwards
                if (_zMoveState == Direction.FORWARDS)
                {
                    return;
                }
                break;
        }

        _zMoveState = dir;
    }

    /**
     * Called directy from the controller class which reacts to button releases.
     * Decides wether the call to stop moving in a given direction is valid.
     * @dir the direction to stop moving in
     */
    public void StopZMoveState(Direction dir)
    {
        // Should only reset direction if currently moving in given direction
        if (_zMoveState == dir)
        {
            _zMoveState = Direction.NONE;
        }
    }

    public void SetYMoveState(Direction dir)
    {
        switch (dir)
        {
            case Direction.UP:
                // Cannot go up if currently going down
                if (_yMoveState == Direction.DOWN)
                {
                    return;
                }
                break;
            case Direction.DOWN:
                // Cannot go down if currently going up
                if (_yMoveState == Direction.UP)
                {
                    return;
                }
                break;
        }

        _yMoveState = dir;
    }

    public void StopYMoveState(Direction dir)
    {
        if (_yMoveState == dir)
        {
            _yMoveState = Direction.NONE;
        }
    }

    public void SetStrafeState(Direction dir)
    {
        switch (dir)
        {
            case Direction.LEFT:
                // Cannot strafe left if currently strafing right
                if (_strafeState == Direction.RIGHT)
                {
                    return;
                }
                break;
            case Direction.RIGHT:
                // Cannot strafe right if currently strafing left
                if (_strafeState == Direction.LEFT)
                {
                    return;
                }
                break;
        }

        _strafeState = dir;
    }

    public void StopStrafeState(Direction dir)
    {
        if (_strafeState == dir)
        {
            _strafeState = Direction.NONE;
        }
    }

    public void SwitchInterfaceMode()
    {
        _interfaceMode = _interfaceMode == ControlMode.INTERFACE
        ? ControlMode.STEERING
        : ControlMode.INTERFACE;
    }

    public void ToggleLights()
    {
      lightsOn = !lightsOn;
      
      if (lightsOn) 
      {
        EventManager.Instance.NotifyOfLightsOn();
      } else 
      {
        EventManager.Instance.NotifyOfLightsOff();
      }
    }

    public void CaveTrigger(InCaveTrigger trigger) 
    {
        if (lastCaveTrigger != InCaveTrigger.MID) {
            return;
        }
        switch(trigger) {
            case InCaveTrigger.START:
                break;
        }
    }
}
