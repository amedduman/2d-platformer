using UnityEngine;

public class PlayerFallState : State<Player>
{
    public PlayerFallState(Player sm) : base(sm) {
    }

    float _inAirTimer = 1;
    
    public override void Enter()
    {
        _inAirTimer = 1;
        Owner.PlayAnimation("falling");
    }

    public override void Tick()
    {
        if (Owner.IsJumpButtonPressed() && Owner.CanDoubleJump)
        {
            Owner.MovementStateMachine.ChangeState(Owner.DoubleJumpState);
            return;
        }

        _inAirTimer += Time.deltaTime;
    }

    public override void FixedTick()
    {
        Owner.Fall(100 * _inAirTimer);
        
        Owner.MoveHorizontally();
        
        if(Owner.CheckGround())
        {
            Owner.MovementStateMachine.ChangeState(Owner.IdleState);
            return;
        }
        
        if(Owner.CheckLedge())
        {
            Owner.MovementStateMachine.ChangeState(Owner.LedgeHangingState);
            return;
        }
        
        if (Owner.CheckWall())
        {
            if (Owner.IsMoveInputParallelWithTransformRight())
            {
                Owner.MovementStateMachine.ChangeState(Owner.WallSlideState);
            }
        }
    }
}
