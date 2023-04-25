using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _playerInput.Gameplay.Movement.ReadValue<Vector2>();
        inputVector.Normalize();
        return inputVector;
    }

    public bool IsJumpButtonPressed()
    {
        return _playerInput.Gameplay.Jump.IsPressed();
    }
}
