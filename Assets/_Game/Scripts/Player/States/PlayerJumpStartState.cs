using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpStartState : State<Player>
{
    public PlayerJumpStartState(Player sm) : base(sm) {
    }


    public override void Enter()
    {
        Owner.PlayAnimation("jump_start");
    }

    public override void Tick()
    {
        // Owner.EnterFallStateIfNoGroundAndVelocityYisNegative();
        
        // if (Owner.WallCheck())
        // {
        //     if (Owner.MoveInput.magnitude != 0)
        //     {
        //         if (Owner.IsMoveInputParallelWithTransformRight())
        //         {
        //             if (Owner.Rb.velocity.y < 0)
        //             {
        //                 Owner.MovementStateMachine.ChangeState(Owner.WallSlideState);
        //             }
        //         }
        //     }
        // }
        // Owner.EnterWallSlideStateIfThereisWallAndVelocityYisNegative();
    }

    public override void FixedTick()
    {
        if (Owner.CheckGround())
        {
            Owner.Rb.AddForce(new Vector2(0, Owner.JumpSpeed), ForceMode2D.Impulse);       
        }
        else
        {
            Owner.MovementStateMachine.ChangeState(Owner.JumpLaunchingState);
        }
        
        // if (Owner.CheckGround() == false)
        // {
        //     if (Owner.Rb.velocity.y < 0)
        //     {
        //         Owner.MovementStateMachine.ChangeState(Owner.FallState);
        //     }
        // }
        
        // if(Owner.JumpInput && Owner.CheckGround())
        // {
        //     Jump();
        // }

        // else if(Owner.JumpInput == false)
        // {
        //     if(Owner.Rb.velocity.y > 0)
        //     {
        //         Owner.Rb.velocity = new Vector2(Owner.Rb.velocity.x, 0);
        //     }
        // }
        //
        // else if(Owner.CheckGround() == false)
        // {
        //     if(Owner.WallCheck() == false)
        //     {
        //         if (Owner.MoveInput.magnitude != 0) // don't do anything if no input is given
        //         {
        //             Owner.MoveHorizontally();
        //         }
        //     }
        //
        //     if (Owner.Rb.velocity.y < 0)
        //     {
        //         Owner.MovementStateMachine.ChangeState(Owner.FallState);
        //     }
        //     else
        //     {
        //         Owner.PlayAnimation("jump_continue");
        //     }
        // }
        //
        // else
        // {
        //     Owner.EnterIdleStateIfThereIsGroundAndVelocityYisNegative();
        // }

    }
    
    public void Jump()
    {
        Owner.Rb.velocity = new Vector2(Owner.Rb.velocity.x, Owner.JumpSpeed);
        // var force = new Vector2(0, Owner.JumpSpeed);
        // Owner.Rb.AddForce(force, ForceMode2D.Impulse);
    }
}
