using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State
{
    Player _player;

    public PlayerIdleState(Player player)
    {
        _player = player;
    }

    public override void Enter()
    {
        _player.MyAnimator.Play("Idle");
    }

    public override void Tick()
    {
        if (Mathf.Approximately(0, _player.MoveInput.x) == false)
            _player.MovementStateMachine.ChangeState(_player.MoveState);

        if (_player.JumpInput)
            _player.MovementStateMachine.ChangeState(_player.JumpState);
    }
}
