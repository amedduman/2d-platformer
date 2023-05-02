using DG.Tweening;
using UnityEngine;

public class PlayerJumpLaunchingState : State<Player>
{
    public PlayerJumpLaunchingState(Player owner) : base(owner) {
    }

    float _inAirTime;
    float _allowedMinTimeForInAir = .15f;

    // bool _launchPeakPauseFinished = false;
    // bool _launchPeakLogicCalled = false;
    // float _originalGravityY;
    
    public override void Enter()
    {
        _inAirTime = 0;
        // _launchPeakPauseFinished = false;
        // _launchPeakLogicCalled = false;
        // _originalGravityY = Physics2D.gravity.y;
        Owner.PlayAnimation("jump_continue");
    }

    public override void Tick()
    {
        _inAirTime += Time.deltaTime;
        
        if (Owner.IsJumpButtonPressed())
        {
            Owner.MovementStateMachine.ChangeState(Owner.DoubleJumpState);
            return;
        }
    }

    public override void FixedTick()
    {
        Owner.MoveHorizontally();

        // if (Owner.IsJumpButtonPressed())
        // {
        //     Owner.MovementStateMachine.ChangeState(Owner.DoubleJumpState);
        //     return;
        // }

        // prevent really short jumps when players pres very quickly to jump button.
        if (_inAirTime < _allowedMinTimeForInAir)
        {
            Owner.Rb.velocity = new Vector2(Owner.Rb.velocity.x, Owner.JumpSpeed);
            return;
        }

        if (Owner.CheckGround())
        {
            Owner.MovementStateMachine.ChangeState(Owner.IdleState);
            return;
        }
        
        if (Owner.Rb.velocity.y < 0)
        {
            Owner.MovementStateMachine.ChangeState(Owner.FallState);
            return;
        }

        // reset y velocity if jump button pressed to make variable jump height.
        if (Owner.JumpInput == false)
        {
            Owner.Rb.velocity = new Vector2(Owner.Rb.velocity.x, 0);
        }
        
        if (Owner.Rb.velocity.y < .1f)
        {
            Owner.MovementStateMachine.ChangeState(Owner.JumpPeakState);
            
            // _launchPeakLogicCalled = true;
            // Physics2D.gravity = new Vector2(Physics.gravity.x, 0);
            // DOVirtual.DelayedCall(.9f, () =>
            // {
            //     Physics2D.gravity = new Vector2(Physics.gravity.x, _originalGravityY);
            //     _launchPeakPauseFinished = true;
            // });
        }
    }
}