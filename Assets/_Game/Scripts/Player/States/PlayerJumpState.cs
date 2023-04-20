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
        if (GroundCheck())
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
        _player.ChangeBodyRotataionAccordingToMovementDirection();
    }

    public override void FixedTick()
    {
        if (GroundCheck())
        {
            if(_player.IsMovingHorizontally())
            {
                _player.MovementStateMachine.ChangeState(_player.MoveState);
            }
            else
            {
                _player.MovementStateMachine.ChangeState(_player.IdleState);
            }
        }    
        else
        {
            _player.MoveHorizontally();
        }

        if(IsPlayerFalling())
        {
            _player.PlayAnimation("falling");
        }
        else
        {
            _player.PlayAnimation("jump_continue");
        }
    }

    void Jump()
    {
        _player.Rb.velocity = new Vector2(_player.Rb.velocity.x, _player.JumpSpeed);
    }

    bool GroundCheck()
    {
        if(Physics2D.OverlapBox(_player.transform.position,
            new Vector2(.3f, .3f), 0, _player.JumpableLayers) != null)
        {
            return true;
        }

        return false;
    }

    bool IsPlayerFalling()
    {
        return _player.Rb.velocity.y < 0;
    }
}
