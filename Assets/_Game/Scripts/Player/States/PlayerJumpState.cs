using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : State<Player>
{
    public PlayerJumpState(Player sm) : base(sm) {
    }


    public override void Enter()
    {
        if (Owner.GroundCheck())
        {
            Owner.PlayAnimation("jump_start");
            Owner.Jump();
        }
    }

    public override void Tick()
    {
        Owner.EnterFallStateIfNoGroundAndVelocityYisNegative();
        Owner.EnterWallSlideStateIfThereisWallAndVelocityYisNegative();
    }

    public override void FixedTick()
    {
        if(Owner.JumpInput && Owner.GroundCheck())
        {
            Owner.Jump();
        }
        else if(Owner.JumpInput == false)
        {
            if(IsPlayerFalling() == false)
            {
                Owner.Rb.velocity = new Vector2(Owner.Rb.velocity.x, 0);
            }
        }

        else if(Owner.GroundCheck() == false)
        {
            Owner.MoveHorizontally();

            if (IsPlayerFalling())
            {
                Owner.MovementStateMachine.ChangeState(Owner.FallState);
                Owner.PlayAnimation("falling");
            }
            else
            {
                Owner.PlayAnimation("jump_continue");
            }
        }
        else
        {
            Owner.EnterIdleStateIfThereIsGroundAndVelocityYisNegative();
        }

    }

    bool IsPlayerFalling()
    {
        return Owner.Rb.velocity.y < 0;
    }
}
