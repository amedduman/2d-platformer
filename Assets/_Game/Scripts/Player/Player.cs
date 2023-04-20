using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [field: SerializeField] public float MovementSpeed { get; private set; } = 16;
    public Vector2 MoveInputVec { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    StateMachine _movementStateMachine;
    PlayerMoveState _moveState;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _movementStateMachine = new StateMachine();
        _moveState = new PlayerMoveState(this);

        _movementStateMachine.ChangeState(_moveState);
    }

    void Update()
    {
        MoveInputVec = ServiceLocator.Get<InputManager>().GetMovementVectorNormalized();
    }
}
