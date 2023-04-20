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
        //_player.PlayAnimation("Idle");
        _player.MyAnimator.CrossFade("Idle", 0, 0);
        _player.Rb.velocity = Vector2.zero;
    }

    public override void Tick()
    {
        //_player.PlayAnimation("Idle");

        if (Mathf.Approximately(0, _player.MoveInput.x) == false)
        {
            Debug.Log("transition to move state");
            _player.MovementStateMachine.ChangeState(_player.MoveState);
        }

        if (_player.JumpInput)
        {
            Debug.Log("transition to jump state");
            _player.MovementStateMachine.ChangeState(_player.JumpState);
        }
    }
}
