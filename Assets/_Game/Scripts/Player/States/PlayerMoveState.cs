using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : State<Player>
{
    public PlayerMoveState(Player sm) : base(sm) {
    }


    public override void Enter()
    {
        Owner.CanDoubleJump = true;
        Owner.MyAnimator.Play("Walking");
    }

    public override void Tick()
    {
        // Owner.EnterJumpStateIfJumpButtonPressed();
        if (Owner.IsJumpButtonPressed())
        {
            Owner.MovementStateMachine.ChangeState(Owner.JumpStartState);
        }
        // Owner.EnterFallStateIfNoGroundAndVelocityYisNegative();
        if (Owner.CheckGround() == false)
        {
            if (Owner.Rb.velocity.y < 0)
            {
                Owner.MovementStateMachine.ChangeState(Owner.FallState);
            }
        }
        if(Mathf.Approximately(0, Owner.MoveInput.x))
        {
            Owner.MovementStateMachine.ChangeState(Owner.IdleState);
        }
    }

    public override void FixedTick()
    {
        Owner.MoveHorizontally();
    }
}
