using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputControler : MonoBehaviour
{
    private PlayerInputActions _input;
    private IPlayer _player;
    private Vector3 _moveVector = Vector2.zero;
    private Vector2 _moveValue;

    private void Awake()
    {
        _input = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Movement.performed += OnMovementPerformed;
        _input.Player.Movement.canceled += OnMovementCanceled;
    }

    public void Init(IPlayer player)
    {
        _player = player;
    }

    #region Movement

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        _moveValue = value.ReadValue<Vector2>();
        _moveVector = new Vector3(_moveValue.x, _moveValue.y, 0);

        _player.MovePlayer(true, _moveVector);
    }

    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        _moveVector = Vector2.zero;
        _player.MovePlayer(false, _moveVector);
    }

    #endregion

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Movement.performed -= OnMovementPerformed;
        _input.Player.Movement.canceled -= OnMovementCanceled;
    }
}
