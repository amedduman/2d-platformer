using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [field: SerializeField] public Animator MyAnimator { get; private set; }

    [field: SerializeField] public float MovementSpeed { get; private set; } = 16;
    [field: SerializeField] public float JumpSpeed { get; private set; } = 16;
    [field: SerializeField] public LayerMask JumpableLayers { get; private set; }

    [field: SerializeField] public Transform WallCheckRayOrigin { get; private set; }
    [field: SerializeField] public float WallCheckRayLegth { get; private set; } = 1;
    [field: SerializeField] public LayerMask WallLayer { get; private set; }

    public Vector2 MoveInput { get; private set; }
    public bool JumpInput { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public StateMachine MovementStateMachine;
    public PlayerMoveState MoveState;
    public PlayerJumpState JumpState;
    public PlayerIdleState IdleState;
    public PlayerWallSlideState WallSlideState;
    public PlayerFallState FallState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }

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
        WallSlideState = new PlayerWallSlideState(this);
        FallState = new PlayerFallState(this);
        WallJumpState = new PlayerWallJumpState(this);

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

        Gizmos.DrawLine(WallCheckRayOrigin.position, WallCheckRayOrigin.position + transform.right * WallCheckRayLegth);
    }

    public void MoveHorizontally()
    {
        Rb.velocity = new Vector2(MoveInput.x * MovementSpeed, Rb.velocity.y);
        ChangeBodyRotataionAccordingToMovementDirection();

        void ChangeBodyRotataionAccordingToMovementDirection()
        {
            float degree = MoveInput.x >= 0 ? 0 : 180;
            var original = transform.rotation.eulerAngles;
            transform.localEulerAngles = new Vector3(original.x, degree, original.z);
        }
    }

    public bool IsMovingHorizontally()
    {
        return Mathf.Approximately(0, Rb.velocity.magnitude);
    }

    public void PlayAnimation(string stateName)
    {
        MyAnimator.Play(Animator.StringToHash(stateName));
    }

    public bool GroundCheck()
    {
        if (Physics2D.OverlapBox(transform.position,
            new Vector2(.3f, .3f), 0, JumpableLayers) != null)
        {
            return true;
        }

        return false;
    }

    public bool CheckWall()
    {
        if (Physics2D.Raycast(WallCheckRayOrigin.position, transform.right, WallCheckRayLegth, WallLayer))
        {
            return true;
        }

        return false;
    }

    public void Jump()
    {
        Rb.velocity = new Vector2(Rb.velocity.x, JumpSpeed);
    }


    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10f, 10f, 600f, 100f));
        string content = MovementStateMachine.CurrentState != null ? MovementStateMachine.CurrentState.ToString() : "(no current state)";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
        GUILayout.EndArea();
    }
}
