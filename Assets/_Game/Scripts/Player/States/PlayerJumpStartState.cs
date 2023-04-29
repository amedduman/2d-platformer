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
        Owner.Rb.velocity = new Vector2(Owner.Rb.velocity.x, 25);
    }

    public override void FixedTick()
    {
        if (Owner.CheckGround() == false)
        {
            Owner.MovementStateMachine.ChangeState(Owner.JumpLaunchingState);
        }
    }
}
