using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    Player _player;

    public PlayerIdleState(Player player) : base(player)
    {
        _player = player;
    }

    public override void Enter()
    {
        _player.PlayAnimation("Idle");
        _player.Rb.velocity = Vector2.zero;
    }

    public override void Tick()
    {
        base.Tick();

        if (Mathf.Approximately(0, _player.MoveInput.x) == false)
        {
            _player.MovementStateMachine.ChangeState(_player.MoveState);
        }
    }
}
