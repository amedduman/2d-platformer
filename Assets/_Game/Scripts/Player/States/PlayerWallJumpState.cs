using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : State<Player>
{
    public PlayerWallJumpState(Player sm) : base(sm) {
    }

    public override void Enter()
    {
        Owner.PlayAnimation("jump_start");
        
        // make player jump and face opposite direction to wall.
        var oppositeDirToWall = Owner.GetOppositeDirectionToCurrentWall();
        Owner.ChangeRotationAccordingToVector(oppositeDirToWall);
        Owner.Rb.velocity = new Vector2(oppositeDirToWall.x * 6, 20);
    }

    public override void FixedTick()
    {
        if (Owner.Rb.velocity.y < 0)
        {
            Owner.MoveHorizontally();
        }

        if (Owner.CheckLedge())
        {
            Owner.MovementStateMachine.ChangeState(Owner.LedgeHangingState);
            return;
        }

        if(Owner.CheckWall())
        {
            Owner.MovementStateMachine.ChangeState( Owner.WallSlideState);
            return;
        }
        if (Owner.CheckGround())
        {
            Owner.MovementStateMachine.ChangeState(Owner.IdleState);
            return;
        }
    }
}
