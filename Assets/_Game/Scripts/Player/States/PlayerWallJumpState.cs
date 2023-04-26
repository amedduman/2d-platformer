using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : State<Player>
{
    public PlayerWallJumpState(Player sm) : base(sm) {
    }

    int dir; // this is the opposite dir to the wall which player is sliding
    public override void Enter()
    {
        Owner.PlayAnimation("jump_start");
        dir = Owner.transform.rotation.eulerAngles.y == 0 ? -1 : 1;
        var degree = dir == 1 ? 0 : 180;
        var original = Owner.transform.rotation.eulerAngles;
        Owner.transform.localEulerAngles = new Vector3(original.x, degree, original.z);
//        Owner.Rb.velocity = new Vector2(Owner.Rb.velocity.x, 20);
        Owner.Rb.velocity = new Vector2(dir * 2, 20);
    }

    public override void FixedTick()
    {
        // allow player to move the dir
        if(Owner.MoveInput.x == dir)
            Owner.MoveHorizontally();

        if(Owner.WallCheck())
        {
            Owner.MovementStateMachine.ChangeState( Owner.WallSlideState);
        }
        if (Owner.CheckGround())
        {
            Owner.MovementStateMachine.ChangeState(Owner.IdleState);
        }
        if (Owner.CheckLedge())
        {
            Owner.MovementStateMachine.ChangeState(Owner.LedgeHangingState);
        }
        if (Owner.WallCheck() == false && Owner.CheckGround() == false && Owner.Rb.velocity.y < 0)
        {
//            Owner.MovementStateMachine.ChangeState(Owner.FallState);
        }

//        Owner.MoveHorizontally();
    }
}
