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

    [field: SerializeField] public Transform GroundCheckRayOrigin { get; private set; }
    [field: SerializeField] public Transform WallCheckRayOrigin { get; private set; }
    [field: SerializeField] public Transform LedgeCheckRayOrigin { get; private set; }
    [field: SerializeField] public Transform LedgeHeightRayCheckOrigin { get; private set; }

    [field: SerializeField] public float WallCheckRayLegth { get; private set; } = 1;
    [field: SerializeField] public float LedgeCheckRayLegth { get; private set; } = 1;
    [field: SerializeField] public LayerMask WallLayer { get; private set; }
    [field: SerializeField] public LayerMask LedgeLayer { get; private set; }

    public Vector2 MoveInput { get; private set; }
    public bool _previousJumpInput { get; private set; }
    public bool JumpInput { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    [HideInInspector] public bool CanDoubleJump;
    
    public StateMachine<Player> MovementStateMachine { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpStartState JumpStartState { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerLedgeHangingState LedgeHangingState { get; private set; }
    public PlayerLeaveWallSlidingState LeaveWallSlidingState { get; private set; }
    public PlayerJumpLaunchingState JumpLaunchingState { get; private set; }
    public PlayerDoubleJumpState DoubleJumpState { get; private set; }
    
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
        JumpStartState = new PlayerJumpStartState(this);
        IdleState = new PlayerIdleState(this);
        WallSlideState = new PlayerWallSlideState(this);
        FallState = new PlayerFallState(this);
        WallJumpState = new PlayerWallJumpState(this);
        LedgeClimbState = new PlayerLedgeClimbState(this);
        LedgeHangingState = new PlayerLedgeHangingState(this);
        LeaveWallSlidingState = new PlayerLeaveWallSlidingState(this);
        JumpLaunchingState = new PlayerJumpLaunchingState(this);
        DoubleJumpState = new PlayerDoubleJumpState(this);

        MovementStateMachine.ChangeState(IdleState);
    }

    void Update()
    {
        MoveInput = _inputManager.GetMovementVectorNormalized();
        _previousJumpInput = JumpInput;
        JumpInput = _inputManager.IsJumpButtonPressed();

        LogStates();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(GroundCheckRayOrigin.position, new Vector3(.1f, .1f, .3f));

        Gizmos.DrawLine(WallCheckRayOrigin.position, WallCheckRayOrigin.position + transform.right * WallCheckRayLegth);

        Gizmos.DrawLine(LedgeCheckRayOrigin.position, LedgeCheckRayOrigin.position + transform.right * LedgeCheckRayLegth);
    }
    
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10f, 10f, 600f, 100f));
        string content = MovementStateMachine.CurrentState != null ? MovementStateMachine.CurrentState.ToString() : "(no current state)";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
        GUILayout.EndArea();
    }

    #region actions

        public void MoveHorizontally()
        {
            Rb.velocity = new Vector2(MoveInput.x * MovementSpeed, Rb.velocity.y);
            ChangeBodyRotationAccordingToMovementDirection();

            void ChangeBodyRotationAccordingToMovementDirection()
            {
                if (Mathf.Approximately(MoveInput.x, 0)) return; // if there is no input on x axis don't change the rotation
                float degree = MoveInput.x >= 0 ? 0 : 180;
                var original = transform.rotation.eulerAngles;
                transform.localEulerAngles = new Vector3(original.x, degree, original.z);
            }   
        }

        public void PlayAnimation(string stateName)
        {
            MyAnimator.Play(Animator.StringToHash(stateName));
        }
        
    #endregion

    #region checks

        public bool CheckGround()
        {
            if (Physics2D.OverlapBox(GroundCheckRayOrigin.position,
                    new Vector2(.1f, .1f), 0, JumpableLayers) != null)
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
        
        public bool CheckLedge()
        {
            if (Physics2D.Raycast(LedgeCheckRayOrigin.position, transform.right, LedgeCheckRayLegth, LedgeLayer))
            {
                return false;
            }
            else if(CheckWall())
            {
                return true;
            }

            return false;
        }

    #endregion

    #region conditions

        public bool IsMoveInputParallelWithTransformRight()
        {
            int dir = (int)transform.right.x;
            int input = (int)MoveInput.x;

            if (dir == input)
            {
                return true;
            }
            return false;
        }

        public bool IsJumpButtonPressed()
        {
            if (_previousJumpInput == false && JumpInput == true)
            {
                return true;
            }

            return false;
        }

    #endregion

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
