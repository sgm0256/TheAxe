using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/Input")]
public class InputReaderSO : ScriptableObject, Controls.IPlayerActions, IPlayerComponent
{
    public event Action<Vector2> MovementEvent;
    public event Action FireEvent;

    public Vector2 MousePos {  get; private set; }

    private Controls _controls;

    private void OnEnable()
    {
        if (_controls == null)
            _controls = new Controls();

        _controls.Player.Enable();
        _controls.Player.SetCallbacks(this);
    }

    public void Initialize(Player player) { }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
            FireEvent?.Invoke();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        MousePos = context.ReadValue<Vector2>();
    }
}
