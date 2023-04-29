using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeaveWallSlidingState : State<Player>
{
    public PlayerLeaveWallSlidingState(Player owner) : base(owner) {
    }

    float _wallJumpTime = .5f;
    float _currentTime = 0;

    public override void Enter()
    {
        _currentTime = 0; // reset current time
        var oppositeDirToWall = GetOppositeDirectionToCurrentWall();
        ChangeRotationAccordingToVector(oppositeDirToWall);
        Owner.Rb.velocity = new Vector2(oppositeDirToWall.x * 2, 2);
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

    public override void Tick()
    {
        if (Owner.CheckGround())
        {
            Owner.MovementStateMachine.ChangeState(Owner.IdleState);
            return;
        }

        if (_currentTime < _wallJumpTime)
        {
            if (Owner.IsJumpButtonPressed())
            {
                Owner.MovementStateMachine.ChangeState(Owner.WallJumpState);
            }
        }
        else
        {
            Owner.MovementStateMachine.ChangeState(Owner.FallState);
        }
        
        // if (_currentTime > _wallJumpTime)
        // {
        //     LeaveWall();
        //     return;
        // }
        //
        // if (Owner.IsJumpButtonPressed())
        // {
        //     Owner.MovementStateMachine.ChangeState(Owner.WallJumpState);
        // }

        _currentTime += Time.deltaTime;
    }

    void LeaveWall()
    {
        if (Math.Abs(Owner.transform.rotation.eulerAngles.y - 180) < 1)
        {
            var original = Owner.transform.rotation.eulerAngles;
            Owner.transform.eulerAngles = new Vector3(original.x, 0, original.z);
            Owner.Rb.velocity = new Vector2(Owner.transform.right.x * -2, Owner.Rb.velocity.y);
        }
        else
        {
            var original = Owner.transform.rotation.eulerAngles;
            Owner.transform.eulerAngles = new Vector3(original.x, 180, original.z);
            Owner.Rb.velocity = new Vector2(Owner.transform.right.x * 2, Owner.Rb.velocity.y);
        }
        Owner.MovementStateMachine.ChangeState(Owner.FallState);
    }
}
