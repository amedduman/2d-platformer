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

    public override void FixedTick()
    {
        _player.Rb.velocity = new Vector2(_player.MoveInputVec.x * _player.MovementSpeed, _player.Rb.velocity.y);
    }
}
