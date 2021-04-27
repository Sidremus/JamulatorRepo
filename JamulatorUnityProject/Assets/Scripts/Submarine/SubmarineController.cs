using UnityEngine;
using UnityEngine.InputSystem;

public class SubmarineController : MonoBehaviour
{
    private SubmarineInputAsset controls;
    private Camera mainCamera;
    private Transform currentUIElement;
    public static Vector2 mousePos { get; private set; }
    public static Vector2 mouseDelta { get; private set; }

    private void Awake()
    {
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Confined;

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

        controls.Submarine.MousePosition.performed += (InputAction.CallbackContext ctx) => SetMousePosition(ctx.ReadValue<Vector2>());

        controls.Submarine.MouseDelta.performed += (InputAction.CallbackContext ctx) => MouseMoveStart(ctx.ReadValue<Vector2>());
        controls.Submarine.MouseDelta.canceled += (InputAction.CallbackContext ctx) => MouseMoveStop();

        controls.Submarine.SwitchInterfaceMode.performed += (InputAction.CallbackContext ctx) => SwitchInterfaceMode();

        controls.Submarine.LeftClickInteract.performed += (InputAction.CallbackContext ctx) => LeftClickEnter();
        controls.Submarine.LeftClickInteract.canceled += (InputAction.CallbackContext ctx) => LeftClickExit();

        controls.Submarine.ToggleLight.performed += (InputAction.CallbackContext ctx) => SubmarineState.Instance.ToggleLights();
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

    private void SetMousePosition(Vector2 vec)
    {
        mousePos = mainCamera.ScreenToViewportPoint(vec);
    }

    private void MouseMoveStart(Vector2 vec)
    {
        mouseDelta = vec;
    }
    
    private void MouseMoveStop()
    {
        mouseDelta = Vector2.zero;
    }

    private void SwitchInterfaceMode()
    {
        if (SubmarineState.Instance.interfaceMode == ControlMode.INTERFACE)
        {
            SubmarineState.Instance.SwitchInterfaceMode(ControlMode.STEERING);
            // TODO: Currently there appears to be an error when triggering SendMessage() through input events. More research required.
            // if (currentUIElement != null)
            // {
            //     currentUIElement.SendMessage("OnMouseExit", SendMessageOptions.DontRequireReceiver);
            //     currentUIElement = null;
            // }
        }
        else SubmarineState.Instance.SwitchInterfaceMode(ControlMode.INTERFACE);
    }

    private void LeftClickEnter()
    {
        if (SubmarineState.Instance.interfaceMode == ControlMode.INTERFACE)
        {
            // TODO: Currently there appears to be an error when triggering SendMessage() through input events. More research required.
            // if (Physics.Raycast(mainCamera.ViewportPointToRay(mousePos), out RaycastHit hit, 2000f, LayerMask.GetMask("UI")))
            // {
            //     currentUIElement = hit.transform;
            //     currentUIElement.SendMessage("LeftClickEnter", SendMessageOptions.DontRequireReceiver);
            // }
        }
    }

    private void LeftClickExit()
    {
        // TODO: Currently there appears to be an error when triggering SendMessage() through input events. More research required.
        // if (currentUIElement != null)
        // {
        //     currentUIElement.SendMessage("LeftClickExit", SendMessageOptions.DontRequireReceiver);
        //     currentUIElement = null;
        // }
    }
}
