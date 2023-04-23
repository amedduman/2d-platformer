using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : State<Player>
{
    public PlayerWallSlideState(Player sm) : base(sm) {
    }

    //    Player _player;
//
//    public PlayerWallSlideState(Player player)
//    {
//        _player = player;
//    }

    public override void Enter()
    {
        Owner.PlayAnimation("wall_sliding");
    }

    public override void Tick()
    {
        if(Owner.GroundCheck())
        {
            Owner.MovementStateMachine.ChangeState(Owner.IdleState);
        }
        else if(Owner.CheckWall() == false)
        {
            Owner.MovementStateMachine.ChangeState(Owner.FallState);
        }
        else if (Owner.CheckWall() && Owner.JumpInput)
        {
            Owner.MovementStateMachine.ChangeState(Owner.WallJumpState);
        }
        //else if (_player.JumpInput)
        //{
        //    _player.MovementStateMachine.ChangeState(_player.JumpState);
        //}
        else
        {
            Owner.Rb.velocity = new Vector2(Owner.Rb.velocity.x, -2);
        }

        //_player.MoveHorizontally();
    }
}
