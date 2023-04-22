using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerGroundedState : State
{
    Player Player;

    public PlayerGroundedState(Player player)
    {
        Player = player;
    }

    public override void Tick()
    {
        if (Player.JumpInput)
        {
            Player.MovementStateMachine.ChangeState(Player.JumpState);
        }
        else if(Player.GroundCheck() == false)
        {
            if(Player.Rb.velocity.y < 0)
            {
                Player.MovementStateMachine.ChangeState(Player.PlayerFallState);
            }
        }
    }
}
