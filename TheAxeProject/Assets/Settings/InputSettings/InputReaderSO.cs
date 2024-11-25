using Core.Entities;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/Input")]
public class InputReaderSO : ScriptableObject, Controls.IPlayerActions, Controls.IUIActions, IEntityComponent
{
    public Vector2 Movement { get; private set; }
    public Vector2 MousePos { get; private set; }

    public event Action<bool> FireEvent;

    private Controls _controls;

    private void OnEnable()
    {
        if (_controls == null)
            _controls = new Controls();

        _controls.Player.Enable();
        _controls.Player.SetCallbacks(this);

        _controls.UI.Enable();
        _controls.UI.SetCallbacks(this);
    }

    public void Initialize(Entity entity) { }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
            FireEvent?.Invoke(true);
        else if (context.canceled)
            FireEvent?.Invoke(false);
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        MousePos = context.ReadValue<Vector2>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {

    }
}
