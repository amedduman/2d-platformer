using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeaveWallSlidingState : State<Player>
{
    public PlayerLeaveWallSlidingState(Player owner) : base(owner) {
    }

    float _wallJumpTime = .2f;
    float _currentTime = 0;

    public override void Enter()
    {
        Owner.PlayAnimation("falling");
        
        _currentTime = 0; // reset current time
        
        // make player jump and face opposite direction to wall.
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

    public override void FixedTick()
    {
        Owner.Fall(20);
    }
}
