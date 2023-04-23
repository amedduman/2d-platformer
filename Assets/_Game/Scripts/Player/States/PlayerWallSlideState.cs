using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : State<Player>
{
    public PlayerWallSlideState(Player sm) : base(sm) {
    }


    public override void Enter()
    {
        Owner.PlayAnimation("wall_sliding");
    }

    public override void Tick()
    {
        if(Owner.GroundCheck())
        {
            Owner.MovementStateMachine.ChangeState(Owner.IdleState);
        }
        else if(Owner.CheckWall() == false)
        {
            Owner.MovementStateMachine.ChangeState(Owner.FallState);
        }
        else if (Owner.CheckWall() && Owner.JumpInput)
        {
            Owner.MovementStateMachine.ChangeState(Owner.WallJumpState);
        }
        else
        {
            Owner.Rb.velocity = new Vector2(Owner.Rb.velocity.x, -2);
        }
    }
}
