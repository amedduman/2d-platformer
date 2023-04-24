using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : State<Player>
{
    public PlayerJumpState(Player sm) : base(sm) {
    }

    bool _hasReachedMaxVerticalVelocity = false;

    public override void Enter()
    {
        _hasReachedMaxVerticalVelocity = false;
        if (Owner.GroundCheck())
        {
            Owner.PlayAnimation("jump_start");
            Owner.Jump();
        }
    }

    public override void Tick()
    {
//        Owner.EnterIdleStateIfThereIsGroundAndVelocityYisNegative();
//        Owner.EnterMoveStateIfThereIsGroundAndPlayerIsMovingHorizontally();
        Owner.EnterFallStateIfNoGroundAndVelocityYisNegative();
        Owner.EnterWallSlideStateIfThereisWallAndVelocityYisNegative();
    }

    public override void FixedTick()
    {
        if(Owner.JumpInput && _hasReachedMaxVerticalVelocity == false)
        {
            if (Owner.Rb.velocity.y < 15)
            {
                Owner.Jump();
            }
            else
            {
                _hasReachedMaxVerticalVelocity = true;
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
