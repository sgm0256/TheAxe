using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/Input")]
public class InputReaderSO : ScriptableObject, Contorls.IPlayerActions
{
    public event Action<Vector2> MovementEvent;
    public event Action<bool> FireEvent;
    
    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed)
            FireEvent?.Invoke(true);
        else if(context.canceled)
            FireEvent?.Invoke(false);
    }
}
