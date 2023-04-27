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
        if (Owner.IsMoveInputParallelWithTransformRight() == false)
        {
            Owner.MovementStateMachine.ChangeState(Owner.LeaveWallSlidingState);
            // if (Owner.transform.rotation.eulerAngles.y == 180)
            // {
            //     var original = Owner.transform.rotation.eulerAngles;
            //     Owner.transform.eulerAngles = new Vector3(original.x, 0, original.z);
            // }
            // else
            // {
            //     var original = Owner.transform.rotation.eulerAngles;
            //     Owner.transform.eulerAngles = new Vector3(original.x, 180, original.z);
            // }
            // Owner.MovementStateMachine.ChangeState(Owner.FallState);

            return;
        }
        
        if(Owner.CheckGround())
        {
            Owner.MovementStateMachine.ChangeState(Owner.IdleState);
            return;
        } 
        if(Owner.WallCheck() == false)
        {
            Owner.MovementStateMachine.ChangeState(Owner.FallState);
            return;
        }
        if (Owner.WallCheck() && Owner.JumpInput)
        {
            Owner.MovementStateMachine.ChangeState(Owner.WallJumpState);
            return;
        }

        Owner.Rb.velocity = new Vector2(Owner.Rb.velocity.x, -2);
    }
}
