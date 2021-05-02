// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Submarine/SubmarineInputAsset.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @SubmarineInputAsset : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @SubmarineInputAsset()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""SubmarineInputAsset"",
    ""maps"": [
        {
            ""name"": ""Submarine"",
            ""id"": ""4db06fbd-9f8a-4718-9b01-8e28ef9f5ab8"",
            ""actions"": [
                {
                    ""name"": ""MoveForward"",
                    ""type"": ""Button"",
                    ""id"": ""ec8bb694-eb36-4adb-bd7d-1567f99ca1d7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveBackward"",
                    ""type"": ""Button"",
                    ""id"": ""6a856ad1-58cf-4bfe-9b25-1198031e37d5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveUp"",
                    ""type"": ""Button"",
                    ""id"": ""10e7653d-a8a0-40f7-b582-46578d9a0285"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveDown"",
                    ""type"": ""Button"",
                    ""id"": ""7a6969ae-2453-48a4-b5db-2815c7a092d5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StrafeLeft"",
                    ""type"": ""Button"",
                    ""id"": ""153b3d07-fd74-426d-a15e-570cb65058b6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StrafeRight"",
                    ""type"": ""Button"",
                    ""id"": ""a64db25e-f5f9-4f4b-a4ab-ef3eb22c08fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""c0ad373e-022b-4751-a472-b9af2e101c19"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseDelta"",
                    ""type"": ""Value"",
                    ""id"": ""df38a865-66f3-47df-b07f-697e59ca46d7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchInterfaceMode"",
                    ""type"": ""Button"",
                    ""id"": ""33e31cb8-2587-4776-b9f3-7ff910589f9a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftClickInteract"",
                    ""type"": ""Button"",
                    ""id"": ""5cad4648-5795-40f8-b70e-0d95abcc9c67"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleLight"",
                    ""type"": ""Button"",
                    ""id"": ""6108bf33-46f7-437c-9a2c-a1db708e2630"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ping"",
                    ""type"": ""Button"",
                    ""id"": ""7fbd2ded-7f47-4282-a58b-40fe491e4ba6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f67747d9-4469-45ff-afbe-88f6a043ef11"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c418f5a-0d9b-4ade-8e69-a7d93c0db979"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveBackward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23d8272b-5a77-4bcb-ab08-86048b9d7d1c"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cad3238e-88ce-44b1-8dcd-e1303896ab0f"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9f15ed1-bac5-4b15-ac29-b1d20cfee2ca"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""StrafeLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4934d5e-baef-4758-ac71-6c9a8fb06cc0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""StrafeRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1fc1aad-c243-4835-a9a5-c0bb65db5519"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f323f344-260e-447e-8522-c85155d1cc02"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MouseDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8536a345-ea50-4190-933f-b649a6c49a27"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""SwitchInterfaceMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""73278567-0008-4b48-a8e8-84ea8e5f511c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""LeftClickInteract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72ddb897-0fb1-46bf-ae2e-d7004b757916"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""ToggleLight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""925995f8-3cdf-4742-b6f2-17a5dd55f9a8"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Ping"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XR"",
            ""bindingGroup"": ""XR"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Submarine
        m_Submarine = asset.FindActionMap("Submarine", throwIfNotFound: true);
        m_Submarine_MoveForward = m_Submarine.FindAction("MoveForward", throwIfNotFound: true);
        m_Submarine_MoveBackward = m_Submarine.FindAction("MoveBackward", throwIfNotFound: true);
        m_Submarine_MoveUp = m_Submarine.FindAction("MoveUp", throwIfNotFound: true);
        m_Submarine_MoveDown = m_Submarine.FindAction("MoveDown", throwIfNotFound: true);
        m_Submarine_StrafeLeft = m_Submarine.FindAction("StrafeLeft", throwIfNotFound: true);
        m_Submarine_StrafeRight = m_Submarine.FindAction("StrafeRight", throwIfNotFound: true);
        m_Submarine_MousePosition = m_Submarine.FindAction("MousePosition", throwIfNotFound: true);
        m_Submarine_MouseDelta = m_Submarine.FindAction("MouseDelta", throwIfNotFound: true);
        m_Submarine_SwitchInterfaceMode = m_Submarine.FindAction("SwitchInterfaceMode", throwIfNotFound: true);
        m_Submarine_LeftClickInteract = m_Submarine.FindAction("LeftClickInteract", throwIfNotFound: true);
        m_Submarine_ToggleLight = m_Submarine.FindAction("ToggleLight", throwIfNotFound: true);
        m_Submarine_Ping = m_Submarine.FindAction("Ping", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Submarine
    private readonly InputActionMap m_Submarine;
    private ISubmarineActions m_SubmarineActionsCallbackInterface;
    private readonly InputAction m_Submarine_MoveForward;
    private readonly InputAction m_Submarine_MoveBackward;
    private readonly InputAction m_Submarine_MoveUp;
    private readonly InputAction m_Submarine_MoveDown;
    private readonly InputAction m_Submarine_StrafeLeft;
    private readonly InputAction m_Submarine_StrafeRight;
    private readonly InputAction m_Submarine_MousePosition;
    private readonly InputAction m_Submarine_MouseDelta;
    private readonly InputAction m_Submarine_SwitchInterfaceMode;
    private readonly InputAction m_Submarine_LeftClickInteract;
    private readonly InputAction m_Submarine_ToggleLight;
    private readonly InputAction m_Submarine_Ping;
    public struct SubmarineActions
    {
        private @SubmarineInputAsset m_Wrapper;
        public SubmarineActions(@SubmarineInputAsset wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveForward => m_Wrapper.m_Submarine_MoveForward;
        public InputAction @MoveBackward => m_Wrapper.m_Submarine_MoveBackward;
        public InputAction @MoveUp => m_Wrapper.m_Submarine_MoveUp;
        public InputAction @MoveDown => m_Wrapper.m_Submarine_MoveDown;
        public InputAction @StrafeLeft => m_Wrapper.m_Submarine_StrafeLeft;
        public InputAction @StrafeRight => m_Wrapper.m_Submarine_StrafeRight;
        public InputAction @MousePosition => m_Wrapper.m_Submarine_MousePosition;
        public InputAction @MouseDelta => m_Wrapper.m_Submarine_MouseDelta;
        public InputAction @SwitchInterfaceMode => m_Wrapper.m_Submarine_SwitchInterfaceMode;
        public InputAction @LeftClickInteract => m_Wrapper.m_Submarine_LeftClickInteract;
        public InputAction @ToggleLight => m_Wrapper.m_Submarine_ToggleLight;
        public InputAction @Ping => m_Wrapper.m_Submarine_Ping;
        public InputActionMap Get() { return m_Wrapper.m_Submarine; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SubmarineActions set) { return set.Get(); }
        public void SetCallbacks(ISubmarineActions instance)
        {
            if (m_Wrapper.m_SubmarineActionsCallbackInterface != null)
            {
                @MoveForward.started -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnMoveForward;
                @MoveForward.performed -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnMoveForward;
                @MoveForward.canceled -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnMoveForward;
                @MoveBackward.started -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnMoveBackward;
                @MoveBackward.performed -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnMoveBackward;
                @MoveBackward.canceled -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnMoveBackward;
                @MoveUp.started -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnMoveUp;
                @MoveUp.performed -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnMoveUp;
                @MoveUp.canceled -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnMoveUp;
                @MoveDown.started -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnMoveDown;
                @MoveDown.performed -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnMoveDown;
                @MoveDown.canceled -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnMoveDown;
                @StrafeLeft.started -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnStrafeLeft;
                @StrafeLeft.performed -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnStrafeLeft;
                @StrafeLeft.canceled -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnStrafeLeft;
                @StrafeRight.started -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnStrafeRight;
                @StrafeRight.performed -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnStrafeRight;
                @StrafeRight.canceled -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnStrafeRight;
                @MousePosition.started -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnMousePosition;
                @MouseDelta.started -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnMouseDelta;
                @MouseDelta.performed -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnMouseDelta;
                @MouseDelta.canceled -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnMouseDelta;
                @SwitchInterfaceMode.started -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnSwitchInterfaceMode;
                @SwitchInterfaceMode.performed -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnSwitchInterfaceMode;
                @SwitchInterfaceMode.canceled -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnSwitchInterfaceMode;
                @LeftClickInteract.started -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnLeftClickInteract;
                @LeftClickInteract.performed -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnLeftClickInteract;
                @LeftClickInteract.canceled -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnLeftClickInteract;
                @ToggleLight.started -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnToggleLight;
                @ToggleLight.performed -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnToggleLight;
                @ToggleLight.canceled -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnToggleLight;
                @Ping.started -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnPing;
                @Ping.performed -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnPing;
                @Ping.canceled -= m_Wrapper.m_SubmarineActionsCallbackInterface.OnPing;
            }
            m_Wrapper.m_SubmarineActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveForward.started += instance.OnMoveForward;
                @MoveForward.performed += instance.OnMoveForward;
                @MoveForward.canceled += instance.OnMoveForward;
                @MoveBackward.started += instance.OnMoveBackward;
                @MoveBackward.performed += instance.OnMoveBackward;
                @MoveBackward.canceled += instance.OnMoveBackward;
                @MoveUp.started += instance.OnMoveUp;
                @MoveUp.performed += instance.OnMoveUp;
                @MoveUp.canceled += instance.OnMoveUp;
                @MoveDown.started += instance.OnMoveDown;
                @MoveDown.performed += instance.OnMoveDown;
                @MoveDown.canceled += instance.OnMoveDown;
                @StrafeLeft.started += instance.OnStrafeLeft;
                @StrafeLeft.performed += instance.OnStrafeLeft;
                @StrafeLeft.canceled += instance.OnStrafeLeft;
                @StrafeRight.started += instance.OnStrafeRight;
                @StrafeRight.performed += instance.OnStrafeRight;
                @StrafeRight.canceled += instance.OnStrafeRight;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @MouseDelta.started += instance.OnMouseDelta;
                @MouseDelta.performed += instance.OnMouseDelta;
                @MouseDelta.canceled += instance.OnMouseDelta;
                @SwitchInterfaceMode.started += instance.OnSwitchInterfaceMode;
                @SwitchInterfaceMode.performed += instance.OnSwitchInterfaceMode;
                @SwitchInterfaceMode.canceled += instance.OnSwitchInterfaceMode;
                @LeftClickInteract.started += instance.OnLeftClickInteract;
                @LeftClickInteract.performed += instance.OnLeftClickInteract;
                @LeftClickInteract.canceled += instance.OnLeftClickInteract;
                @ToggleLight.started += instance.OnToggleLight;
                @ToggleLight.performed += instance.OnToggleLight;
                @ToggleLight.canceled += instance.OnToggleLight;
                @Ping.started += instance.OnPing;
                @Ping.performed += instance.OnPing;
                @Ping.canceled += instance.OnPing;
            }
        }
    }
    public SubmarineActions @Submarine => new SubmarineActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    private int m_JoystickSchemeIndex = -1;
    public InputControlScheme JoystickScheme
    {
        get
        {
            if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
            return asset.controlSchemes[m_JoystickSchemeIndex];
        }
    }
    private int m_XRSchemeIndex = -1;
    public InputControlScheme XRScheme
    {
        get
        {
            if (m_XRSchemeIndex == -1) m_XRSchemeIndex = asset.FindControlSchemeIndex("XR");
            return asset.controlSchemes[m_XRSchemeIndex];
        }
    }
    public interface ISubmarineActions
    {
        void OnMoveForward(InputAction.CallbackContext context);
        void OnMoveBackward(InputAction.CallbackContext context);
        void OnMoveUp(InputAction.CallbackContext context);
        void OnMoveDown(InputAction.CallbackContext context);
        void OnStrafeLeft(InputAction.CallbackContext context);
        void OnStrafeRight(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnMouseDelta(InputAction.CallbackContext context);
        void OnSwitchInterfaceMode(InputAction.CallbackContext context);
        void OnLeftClickInteract(InputAction.CallbackContext context);
        void OnToggleLight(InputAction.CallbackContext context);
        void OnPing(InputAction.CallbackContext context);
    }
}
