using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    public static PlayerInputActions inputActions = new PlayerInputActions();
    public static event Action<InputActionMap> actionMapChange;

    // Start is called before the first frame update
    void Start()
    {
        ToggleActionMap(inputActions.Player);
    }

    public static void ToggleActionMap(InputActionMap actionMap)
    {
        if (actionMap.enabled)
            return;

        inputActions.Disable();
        actionMapChange?.Invoke(actionMap);
        actionMap.Enable();
    }
}
