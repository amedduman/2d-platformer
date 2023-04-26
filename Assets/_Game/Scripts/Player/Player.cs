using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
	public Transform Debug_Target;
	public Transform  Debug_Target2;
    [field: SerializeField] public Animator MyAnimator { get; private set; }

    [field: SerializeField] public float MovementSpeed { get; private set; } = 16;
    [field: SerializeField] public float JumpSpeed { get; private set; } = 16;
    [field: SerializeField] public LayerMask JumpableLayers { get; private set; }

    [field: SerializeField] public Transform GroundCheckRayOrigin { get; private set; }
    [field: SerializeField] public Transform WallCheckRayOrigin { get; private set; }
    [field: SerializeField] public Transform LedgeCheckRayOrigin { get; private set; }

    [field: SerializeField] public float WallCheckRayLegth { get; private set; } = 1;
    [field: SerializeField] public float LedgeCheckRayLegth { get; private set; } = 1;
    [field: SerializeField] public LayerMask WallLayer { get; private set; }
    [field: SerializeField] public LayerMask LedgeLayer { get; private set; }

    public Vector2 MoveInput { get; private set; }
    public bool _previousJumpInput { get; private set; }
    public bool JumpInput { get; private set; }
    public Rigidbody2D Rb { get; private set; }

    public StateMachine<Player> MovementStateMachine;
    public PlayerMoveState MoveState;
    public PlayerJumpState JumpState;
    public PlayerIdleState IdleState;
    public PlayerWallSlideState WallSlideState;
    public PlayerFallState FallState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }

    InputManager _inputManager;

    private void Awake()
    {
        _inputManager = ServiceLocator.Get<InputManager>();
        Rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        MovementStateMachine = new StateMachine<Player>();
        MoveState = new PlayerMoveState(this);
        JumpState = new PlayerJumpState(this);
        IdleState = new PlayerIdleState(this);
        WallSlideState = new PlayerWallSlideState(this);
        FallState = new PlayerFallState(this);
        WallJumpState = new PlayerWallJumpState(this);
        LedgeClimbState = new PlayerLedgeClimbState(this);

        MovementStateMachine.ChangeState(IdleState);
    }

//    public bool isClimb;

    void Update()
    {
        MoveInput = _inputManager.GetMovementVectorNormalized();
        _previousJumpInput = JumpInput;
        JumpInput = _inputManager.IsJumpButtonPressed();

        LogStates();

//        if(isClimb)
//        {
//            PlayAnimation("ledge-climbing");
//            transform.position = Debug_Target.position;
//        }
//        else
//        {
//            PlayAnimation("Idle");
//        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(GroundCheckRayOrigin.position, new Vector3(.1f, .1f, .3f));

        Gizmos.DrawLine(WallCheckRayOrigin.position, WallCheckRayOrigin.position + transform.right * WallCheckRayLegth);

        Gizmos.DrawLine(LedgeCheckRayOrigin.position, LedgeCheckRayOrigin.position + transform.right * LedgeCheckRayLegth);
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

    public bool CheckGround()
    {
        if (Physics2D.OverlapBox(GroundCheckRayOrigin.position,
            new Vector2(.1f, .1f), 0, JumpableLayers) != null)
        {
            return true;
        }

        return false;
    }

    public bool WallCheck()
    {
        if (Physics2D.Raycast(WallCheckRayOrigin.position, transform.right, WallCheckRayLegth, WallLayer))
        {
            return true;
        }

        return false;
    }

    public bool IsVelocityOnYAxisisNegative()
    {
        return Rb.velocity.y < 0;
    }

    public void Jump()
    {
//        Rb.velocity = new Vector2(Rb.velocity.x, JumpSpeed);
        var force = new Vector2(0, JumpSpeed);
        Rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10f, 10f, 600f, 100f));
        string content = MovementStateMachine.CurrentState != null ? MovementStateMachine.CurrentState.ToString() : "(no current state)";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
        GUILayout.EndArea();
    }

    public void EnterJumpStateIfJumpButtonPressed()
    {
        if(_previousJumpInput) return;
        if (JumpInput)
        {
            MovementStateMachine.ChangeState(JumpState);
        }
    }

    public void EnterFallStateIfNoGroundAndVelocityYisNegative()
    {
        if (CheckGround() == false)
        {
            if (Rb.velocity.y < 0)
            {
                MovementStateMachine.ChangeState(FallState);
            }
        }
    }

    public void EnterIdleStateIfThereIsGroundAndVelocityYisNegative()
    {
        Debug.Log(Rb.velocity.y);
        if(CheckGround() && Rb.velocity.y <= 0)
        {
            MovementStateMachine.ChangeState(IdleState);
        }
        else
        {
            Debug.Log("can't enter idle state");
        }
    }

    public void EnterWallSlideStateIfThereisWallAndVelocityYisNegative()
    {
        if (WallCheck())
        {
            if (Rb.velocity.y < 0)
            {
                MovementStateMachine.ChangeState(WallSlideState);
            }
        }
    }

    public void EnterMoveStateIfThereIsGroundAndPlayerIsMovingHorizontally()
    {
        if (CheckGround())
        {
            if(IsMovingHorizontally())
            {
                MovementStateMachine.ChangeState(MoveState);
            }
        }
    }

    public bool CheckLedge()
    {
        if (Physics2D.Raycast(LedgeCheckRayOrigin.position, transform.right, LedgeCheckRayLegth, LedgeLayer))
        {
            return false;
        }
        else if(WallCheck())
        {
            return true;
        }

        return false;
    }

    [SerializeField] bool LogStatesToConsole;
    string _previousState = string.Empty;

    private void LogStates()
    {
        if(LogStatesToConsole == false) return;
        var currentState = MovementStateMachine.CurrentState.ToString();
        if(_previousState != currentState)
        {
            Debug.Log(currentState);
        }
        _previousState = currentState;
    }
}
