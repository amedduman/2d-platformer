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
    }

    public override void Tick()
    {
        if(_player.GroundCheck())
        {
            _player.MovementStateMachine.ChangeState(_player.IdleState);
        }
        else
        {
            _player.Rb.velocity = new Vector2(_player.Rb.velocity.x, -2);
        }
    }
}
