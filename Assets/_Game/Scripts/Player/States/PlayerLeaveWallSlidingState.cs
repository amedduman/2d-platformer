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
        var oppositeDirToWall = Owner.GetOppositeDirectionToCurrentWall();
        Owner.ChangeRotationAccordingToVector(oppositeDirToWall);
        Owner.Rb.velocity = new Vector2(oppositeDirToWall.x * 2, 2);
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
