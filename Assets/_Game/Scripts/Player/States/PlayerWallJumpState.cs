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
        var oppositeDirToWall = GetOppositeDirectionToCurrentWall();
        ChangeRotationAccordingToVector(oppositeDirToWall);
        Owner.Rb.velocity = new Vector2(oppositeDirToWall.x * 6, 20);
    }
    
    Vector2 GetOppositeDirectionToCurrentWall()
    {
        if (Physics2D.Raycast(Owner.WallCheckRayOrigin.position, Owner.transform.right, Owner.WallCheckRayLegth, Owner.WallLayer))
        {
            return Owner.transform.right * -1;
        }
        return Owner.transform.right;
    }

    void ChangeRotationAccordingToVector(Vector2 vec)
    {
        float degree = 0;
        var original = Owner.transform.rotation.eulerAngles;
        if ((int)vec.x == 1)
        {
            degree = 0;
        }
        else if ((int)vec.x == -1)
        {
            degree = 180;
        }
        else
        {
            degree = original.y;
        }
        
        Owner.transform.localEulerAngles = new Vector3(original.x, degree, original.z);

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
