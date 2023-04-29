using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : State<Player>
{
    public PlayerWallSlideState(Player sm) : base(sm) {
    }


    public override void Enter()
    {
        // Owner.CanDoubleJump = true;
        Owner.PlayAnimation("wall_sliding");
    }

    public override void Tick()
    {
        // since the wall is on the transform right. we force players to hold on opposite input to make character slide
        if (Owner.IsMoveInputParallelWithTransformRight() == false)
        {
            Owner.MovementStateMachine.ChangeState(Owner.LeaveWallSlidingState);
            return;
        }
        
        if(Owner.CheckGround())
        {
            Owner.MovementStateMachine.ChangeState(Owner.IdleState);
            return;
        } 
        
        if(Owner.CheckWall() == false)
        {
            Owner.MovementStateMachine.ChangeState(Owner.FallState);
            return;
        }
        
        if (/*Owner.CheckWall() &&*/ Owner.IsJumpButtonPressed())
        {
            Owner.MovementStateMachine.ChangeState(Owner.WallJumpState);
            return;
        }

        Owner.Rb.velocity = new Vector2(Owner.Rb.velocity.x, -2);
    }
}
