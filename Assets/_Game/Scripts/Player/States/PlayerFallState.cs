using UnityEngine;

public class PlayerFallState : State<Player>
{
    public PlayerFallState(Player sm) : base(sm) {
    }

    public override void Enter()
    {
        Owner.PlayAnimation("falling");
    }

    public override void Tick()
    {
        if (Owner.IsJumpButtonPressed() && Owner.CanDoubleJump)
        {
            Owner.MovementStateMachine.ChangeState(Owner.DoubleJumpState);
            return;
        }
    }

    public override void FixedTick()
    {
        // limit the y velocity when falling
        if (Owner.Rb.velocity.y < -25)
            Owner.Rb.velocity = new Vector2(Owner.Rb.velocity.x, -25);
        
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
