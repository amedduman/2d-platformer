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
        _currentTime = 0; // rest current time
    }

    public override void Tick()
    {
        if (Owner.CheckGround())
        {
            Owner.MovementStateMachine.ChangeState(Owner.IdleState);
            return;
        }
        
        if (_currentTime > _wallJumpTime)
        {
            LeaveWall();
            return;
        }

        if (Owner.IsJumpButtonPressed())
        {
            Owner.MovementStateMachine.ChangeState(Owner.WallJumpState);
        }

        _currentTime += Time.deltaTime;
    }

    void LeaveWall()
    {
        if (Owner.transform.rotation.eulerAngles.y == 180)
        {
            var original = Owner.transform.rotation.eulerAngles;
            Owner.transform.eulerAngles = new Vector3(original.x, 0, original.z);
        }
        else
        {
            var original = Owner.transform.rotation.eulerAngles;
            Owner.transform.eulerAngles = new Vector3(original.x, 180, original.z);
        }
        Owner.MovementStateMachine.ChangeState(Owner.FallState);
    }
}
