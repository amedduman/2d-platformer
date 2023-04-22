using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : State
{
    Player _player;

    public PlayerJumpState(Player player)
    {
        _player = player;
    }

    public override void Enter()
    {
        if (_player.GroundCheck())
        {
            _player.PlayAnimation("jump_start");
            Jump();
        }
        else
        {
            _player.MovementStateMachine.ChangeState(_player.MoveState);
        }
    }

    public override void Tick()
    {
        if(_player.GroundCheck() == false)
        {
            if(_player.CheckWall())
            {
                if(_player.Rb.velocity.y < 0)
                {
                    _player.MovementStateMachine.ChangeState(_player.WallSlideState);
                }
            }
        }
    }

    public override void FixedTick()
    {
        if (_player.GroundCheck())
        {
            if(_player.IsMovingHorizontally())
            {
                _player.MovementStateMachine.ChangeState(_player.MoveState);
            }
            else if (_player.Rb.velocity.y <= 0)
            {
                _player.MovementStateMachine.ChangeState(_player.IdleState);
            }
        }    
        else
        {
            _player.MoveHorizontally();

            if (IsPlayerFalling())
            {
                _player.PlayAnimation("falling");
            }
            else
            {
                _player.PlayAnimation("jump_continue");
            }
        }
    }

    void Jump()
    {
        _player.Rb.velocity = new Vector2(_player.Rb.velocity.x, _player.JumpSpeed);
    }

    

    bool IsPlayerFalling()
    {
        return _player.Rb.velocity.y < 0;
    }
}
