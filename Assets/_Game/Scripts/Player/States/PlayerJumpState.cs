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
        Owner.EnterIdleStateIfThereIsGroundAndVelocityYisNegative();
        Owner.EnterMoveStateIfThereIsGroundAndPlayerIsMovingHorizontally();
        Owner.EnterFallStateIfNoGroundAndVelocityYisNegative();
        Owner.EnterWallSlideStateIfThereisWallAndVelocityYisNegative();
//        if (Owner.CheckWall())
//        {
//            if (Owner.Rb.velocity.y < 0)
//            {
//                Owner.MovementStateMachine.ChangeState(Owner.WallSlideState);
//            }
//        }
    }

    public override void FixedTick()
    {
//        if (Owner.GroundCheck())
//        {
//            if(Owner.IsMovingHorizontally())
//            {
//                Owner.MovementStateMachine.ChangeState(Owner.MoveState);
//            }
//        }
        if(Owner.GroundCheck() == false)
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
    }

    bool IsPlayerFalling()
    {
        return Owner.Rb.velocity.y < 0;
    }
}
