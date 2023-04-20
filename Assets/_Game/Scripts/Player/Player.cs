using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [field: SerializeField] public float MovementSpeed { get; private set; } = 16;
    [field: SerializeField] public float JumpSpeed { get; private set; } = 16;
    [field: SerializeField] public LayerMask JumpableLayers;
    [field: SerializeField] public Transform Body;
    [field: SerializeField] public Animator MyAnimator;
    public Vector2 MoveInput { get; private set; }
    public bool JumpInput { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public StateMachine MovementStateMachine;
    public PlayerMoveState MoveState;
    public PlayerJumpState JumpState;
    public PlayerIdleState IdleState;

    InputManager _inputManager;

    


    private void Awake()
    {
        _inputManager = ServiceLocator.Get<InputManager>();
        Rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        MovementStateMachine = new StateMachine();
        MoveState = new PlayerMoveState(this);
        JumpState = new PlayerJumpState(this);
        IdleState = new PlayerIdleState(this);

        MovementStateMachine.ChangeState(IdleState);
    }

    void Update()
    {
        MoveInput = _inputManager.GetMovementVectorNormalized();
        JumpInput = _inputManager.IsJumpButtonPressed();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(.3f, .3f, .3f));
    }

    public void MoveHorizontally()
    {
        Rb.velocity = new Vector2(MoveInput.x * MovementSpeed, Rb.velocity.y);
    }

    public void ChangeBodyRotataionAccordingToMovementDirection()
    {
        float degree = MoveInput.x >= 0 ? 0 : 180;
        var original = Body.transform.rotation.eulerAngles;
        Body.localEulerAngles = new Vector3(original.x, degree, original.z);
    }

    public bool IsMovingHorizontally()
    {
        return Mathf.Approximately(0, Rb.velocity.magnitude);
    }

    public void PlayAnimation(string stateName)
    {
        MyAnimator.Play(Animator.StringToHash(stateName));
    }

}
