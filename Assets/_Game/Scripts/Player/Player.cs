using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [field: SerializeField] public Animator MyAnimator { get; private set; }

    [field: SerializeField] public float MovementSpeed { get; private set; } = 16;
    [field: SerializeField] public float JumpSpeed { get; private set; } = 16;
//    [field: SerializeField] public float MinJumpSpeed { get; private set; } = 6;
    [field: SerializeField] public LayerMask JumpableLayers { get; private set; }

    [field: SerializeField] public Transform WallCheckRayOrigin { get; private set; }
    [field: SerializeField] public float WallCheckRayLegth { get; private set; } = 1;
    [field: SerializeField] public LayerMask WallLayer { get; private set; }

    public Vector2 MoveInput { get; private set; }
    public bool JumpInput { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public StateMachine<Player> MovementStateMachine;
    public PlayerMoveState MoveState;
    public PlayerJumpState JumpState;
    public PlayerIdleState IdleState;
    public PlayerWallSlideState WallSlideState;
    public PlayerFallState FallState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }

    InputManager _inputManager;
    float _jumpInputPressedTime;


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

        MovementStateMachine.ChangeState(IdleState);
    }

    void Update()
    {
        MoveInput = _inputManager.GetMovementVectorNormalized();
        JumpInput = _inputManager.IsJumpButtonPressed();

//        CalculateJumpInputPressedTime();
    }

//    void CalculateJumpInputPressedTime()
//    {
//        if(JumpInput)
//        {
//            _jumpInputPressedTime += Time.deltaTime;
//        }
//        else
//        {
//            _jumpInputPressedTime = 0;
//        }
//    }

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

    public bool IsVelocityOnYAxisisNegative()
    {
        return Rb.velocity.y < 0;
    }

    public void Jump()
    {
//        if(_jumpInputPressedTime < 1) _jumpInputPressedTime = 1;
//        var speed = Mathf.Min(MinJumpSpeed * _jumpInputPressedTime, MaxJumpSpeed);
//        Rb.velocity = new Vector2(Rb.velocity.x, JumpSpeed);
        var force = new Vector2(0, JumpSpeed);
        Rb.AddForce(force, ForceMode2D.Impulse);
//        StartCoroutine(JumpCo());
    }

//    IEnumerator JumpCo()
//    {
//        bool continueToJump = true;
//        int iteration = 0;
//        while (continueToJump)
//        {
//            iteration++;
//            if(iteration > 4)
//            {
//                continueToJump = false;
//            }
//            if(JumpInput = false)
//            {
//                continueToJump = false;
//            }
//            Debug.Log(iteration);
//            Rb.velocity = new Vector2(Rb.velocity.x, JumpSpeed * iteration);
//            yield return new WaitForFixedUpdate();
//        }
//    }


    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10f, 10f, 600f, 100f));
        string content = MovementStateMachine.CurrentState != null ? MovementStateMachine.CurrentState.ToString() : "(no current state)";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
        GUILayout.EndArea();
    }

    public void EnterJumpStateIfJumpButtonPressed()
    {
        if (JumpInput)
        {
            MovementStateMachine.ChangeState(JumpState);
        }
    }

    public void EnterFallStateIfNoGroundAndVelocityYisNegative()
    {
        if (GroundCheck() == false)
        {
            if (Rb.velocity.y < 0)
            {
                MovementStateMachine.ChangeState(FallState);
            }
        }
    }

    public void EnterIdleStateIfThereIsGroundAndVelocityYisNegative()
    {
        if(GroundCheck() && Rb.velocity.y <= 0)
        {
            MovementStateMachine.ChangeState(IdleState);
        }
    }

    public void EnterWallSlideStateIfThereisWallAndVelocityYisNegative()
    {
        if (CheckWall())
        {
            if (Rb.velocity.y < 0)
            {
                MovementStateMachine.ChangeState(WallSlideState);
            }
        }
    }

    public void EnterMoveStateIfThereIsGroundAndPlayerIsMovingHorizontally()
    {
        if (GroundCheck())
        {
            if(IsMovingHorizontally())
            {
                MovementStateMachine.ChangeState(MoveState);
            }
        }
    }
}
