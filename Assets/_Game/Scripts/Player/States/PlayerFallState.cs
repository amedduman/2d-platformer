using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerInAirState
{
    Player _player;

    public PlayerFallState(Player player) : base(player)
    {
        Debug.Log("enter fall state");
        _player = player;
    }

    public override void Enter()
    {
        _player.PlayAnimation("falling");
    }
}
