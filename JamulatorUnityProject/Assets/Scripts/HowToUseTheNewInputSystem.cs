using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HowToUseTheNewInputSystem : MonoBehaviour
{
    // If you haven't used the new InputSystem, fear not, this is how to do it!
    // The new input system is a lot more flexible than the old one. It allows for things like easily rebinding keys and hot-swapping between different mappings.
    // To get started, however, here's how to use it in basically the same way you'd use the old one.

    Keyboard kb; // This is a reference to our keyboard. We store it so we don't have to "find" it every time we're checking a key.
    Mouse ms; // Same for the mouse. This is recommended for performance reasons.

    private void Awake()
    {
        kb = Keyboard.current; // This could also be done in OnEnable()
        ms = Mouse.current;
    }

    private void Update()
    {
        // Now, to get a key we just ask our Keyboard kb or Mouse ms for a state and get a bool. Example:
        if (ms.leftButton.wasPressedThisFrame) Debug.Log("A left click was issued!");

        // For digital inputs, there are also the following:
        if (kb.spaceKey.isPressed) Debug.Log("Space key is held down!");
        // And:
        if (ms.rightButton.wasReleasedThisFrame) Debug.Log("The right mouse button was just left go off");

        // Other inputs, like gamepad or mouse axes, we read them as follows:
        transform.position += new Vector3(0, ms.scroll.ReadValue().y, 0) * Time.deltaTime;
        // We reference the device, the Mouse ms in this case. Then the axis, here the scroll wheel.
        // And with .ReadValue() we get a vector output (Vector2 in this case).
    }
}
