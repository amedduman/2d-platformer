using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerInAirState : State
{
    protected Player Player;

    public PlayerInAirState(Player player)
    {
        Player = player;
    }

    public override void Tick()
    {
        if(Player.GroundCheck() && Player.Rb.velocity.y <= 0)
        {
            Player.MovementStateMachine.ChangeState(Player.IdleState);
        }
        if (Player.CheckWall())
        {
            if (Player.Rb.velocity.y < 0)
            {
                Player.MovementStateMachine.ChangeState(Player.WallSlideState);
            }
        }
        else
        {
            
        }
    }
}
