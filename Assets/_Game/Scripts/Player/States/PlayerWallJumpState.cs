using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : State
{
    Player _player;
    public PlayerWallJumpState(Player player)
    {
        _player = player;
    }

    public override void Enter()
    {
        if (Mathf.Approximately(0, _player.MoveInput.x))
        {
            _player.MovementStateMachine.ChangeState(_player.FallState);
        }
        else
        {
            _player.PlayAnimation("jump_start");
            _player.Jump();
        }
    }

    public override void Tick()
    {
        if (_player.GroundCheck())
        {
            _player.MovementStateMachine.ChangeState(_player.IdleState);
        }
        
    }

    public override void FixedTick()
    {
        _player.MoveHorizontally();
    }
}
