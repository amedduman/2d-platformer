using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    Player _player;

    public PlayerMoveState(Player player) : base(player)
    {
        _player = player;
    }

    public override void Enter()
    {
        _player.MyAnimator.Play("Walking");
    }

    public override void Tick()
    {
        base.Tick();

        //if (_player.JumpInput)
        //{
        //    _player.MovementStateMachine.ChangeState(_player.JumpState);
        //}

        if(Mathf.Approximately(0, _player.MoveInput.x))
        {
            _player.MovementStateMachine.ChangeState(_player.IdleState);
        }
    }

    public override void FixedTick()
    {
        _player.MoveHorizontally();
    }

    

    
}
