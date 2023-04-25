using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public event Action JumpPerformed;

    PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
    }

    void OnEnable()
    {
        _playerInput.Gameplay.Jump.performed += JumpButtonPressed;
    }

    void OnDisable()
    {
        _playerInput.Gameplay.Jump.performed -= JumpButtonPressed;
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

    private void JumpButtonPressed(InputAction.CallbackContext obj)
    {
        JumpPerformed?.Invoke();
    }
}
