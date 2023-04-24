using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State<Player>
{
    public PlayerIdleState(Player sm) : base(sm) {
    }


    public override void Enter()
    {
        Owner.PlayAnimation("Idle");
        Owner.Rb.velocity = Vector2.zero;
    }

    public override void Tick()
    {
        Owner.EnterFallStateIfNoGroundAndVelocityYisNegative();
        Owner.EnterJumpStateIfJumpButtonPressed();

        if (Mathf.Approximately(0, Owner.MoveInput.x) == false)
        {
            Owner.MovementStateMachine.ChangeState(Owner.MoveState);
        }
    }
}
