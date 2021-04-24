using UnityEngine;
using UnityEngine.InputSystem;

public class SubmarineController : MonoBehaviour
{
    private SubmarineInputAsset controls;

    private void Awake()
    {
        controls = new SubmarineInputAsset();

        controls.Submarine.MoveForward.performed += (InputAction.CallbackContext ctx) => MoveForwardStart();
        controls.Submarine.MoveForward.canceled += (InputAction.CallbackContext ctx) => MoveForwardStop();

        controls.Submarine.MoveBackward.performed += (InputAction.CallbackContext ctx) => MoveBackwardStart();
        controls.Submarine.MoveBackward.canceled += (InputAction.CallbackContext ctx) => MoveBackwardStop();

        controls.Submarine.MoveUp.performed += (InputAction.CallbackContext ctx) => MoveUpStart();
        controls.Submarine.MoveUp.canceled += (InputAction.CallbackContext ctx) => MoveUpStop();

        controls.Submarine.MoveDown.performed += (InputAction.CallbackContext ctx) => MoveDownStart();
        controls.Submarine.MoveDown.canceled += (InputAction.CallbackContext ctx) => MoveDownStop();

        controls.Submarine.StrafeLeft.performed += (InputAction.CallbackContext ctx) => StrafeLeftStart();
        controls.Submarine.StrafeLeft.canceled += (InputAction.CallbackContext ctx) => StrafeLeftStop();

        controls.Submarine.StrafeRight.performed += (InputAction.CallbackContext ctx) => StrafeRightStart();
        controls.Submarine.StrafeRight.canceled += (InputAction.CallbackContext ctx) => StrafeRightStop();
    }

    private void OnEnable()
    {
        controls.Submarine.Enable();
    }

    private void OnDisable() 
    {
        controls.Submarine.Disable();
    }

    private void MoveForwardStart() 
    {
        SubmarineState.Instance.SetZMoveState(Direction.FORWARDS);
    }

    private void MoveForwardStop()
    {
        SubmarineState.Instance.StopZMoveState(Direction.FORWARDS);
    }

    private void MoveBackwardStart()
    {
        SubmarineState.Instance.SetZMoveState(Direction.BACKWARDS);
    }

    private void MoveBackwardStop()
    {
        SubmarineState.Instance.StopZMoveState(Direction.BACKWARDS);
    }

    private void MoveUpStart()
    {
        SubmarineState.Instance.SetYMoveState(Direction.UP);
    }

    private void MoveUpStop()
    {
         SubmarineState.Instance.StopYMoveState(Direction.UP);
    }

    private void MoveDownStart() 
    {
        SubmarineState.Instance.SetYMoveState(Direction.DOWN);
    }

    private void MoveDownStop()
    {
        SubmarineState.Instance.StopYMoveState(Direction.DOWN);
    }

    private void StrafeLeftStart()
    {
        SubmarineState.Instance.SetStrafeState(Direction.LEFT);
    }

    private void StrafeLeftStop() 
    {
        SubmarineState.Instance.StopStrafeState(Direction.LEFT);
    }

    private void StrafeRightStart()
    {
        SubmarineState.Instance.SetStrafeState(Direction.RIGHT);
    }

    private void StrafeRightStop() 
    {
        SubmarineState.Instance.StopStrafeState(Direction.RIGHT);
    }

}
