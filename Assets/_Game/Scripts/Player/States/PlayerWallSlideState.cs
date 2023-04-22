using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : State
{
    Player _player;

    public PlayerWallSlideState(Player player)
    {
        _player = player;
    }

    public override void Enter()
    {
        _player.PlayAnimation("wall_sliding");
    }

    public override void Tick()
    {
        if(_player.GroundCheck())
        {
            _player.MovementStateMachine.ChangeState(_player.IdleState);
        }
        else if (_player.JumpInput)
        {
            _player.MovementStateMachine.ChangeState(_player.JumpState);
        }
        else
        {
            _player.Rb.velocity = new Vector2(_player.Rb.velocity.x, -2);
        }
    }
}
