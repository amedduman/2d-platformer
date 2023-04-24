using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : State<Player>
{
    public PlayerWallJumpState(Player sm) : base(sm) {
    }


    public override void Enter()
    {
        if (Mathf.Approximately(0, Owner.MoveInput.x))
        {
            Owner.MovementStateMachine.ChangeState(Owner.FallState);
        }
        else
        {
            Owner.PlayAnimation("jump_start");
            Owner.Rb.velocity = new Vector2(Owner.Rb.velocity.x, 16);
        }
    }

    public override void Tick()
    {
        if (Owner.GroundCheck())
        {
            Owner.MovementStateMachine.ChangeState(Owner.IdleState);
        }
    }

    public override void FixedTick()
    {
        Owner.MoveHorizontally();
    }
}
