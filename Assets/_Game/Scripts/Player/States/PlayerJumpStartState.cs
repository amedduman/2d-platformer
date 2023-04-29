using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpStartState : State<Player>
{
    public PlayerJumpStartState(Player sm) : base(sm) {
    }

    public override void Enter()
    {
        Owner.PlayAnimation("jump_start");
    }

    public override void FixedTick()
    {
        if (Owner.CheckGround())
        {
            Owner.Rb.AddForce(new Vector2(0, Owner.JumpSpeed), ForceMode2D.Impulse);       
        }
        else
        {
            Owner.MovementStateMachine.ChangeState(Owner.JumpLaunchingState);
        }
    }
}
