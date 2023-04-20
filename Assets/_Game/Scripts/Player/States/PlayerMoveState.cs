using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : State
{
    Player _player;

    public PlayerMoveState(Player player)
    {
        _player = player;
    }

    public override void Enter()
    {
        _player.MyAnimator.Play("Walking");
    }

    public override void Tick()
    {
        if (_player.JumpInput)
        {
            _player.MovementStateMachine.ChangeState(_player.JumpState);
        }

        if(_player.IsMovingHorizontally())
            _player.MovementStateMachine.ChangeState(_player.IdleState);

        _player.ChangeBodyRotataionAccordingToMovementDirection();
    }

    public override void FixedTick()
    {
        _player.MoveHorizontally();
    }

    

    
}
