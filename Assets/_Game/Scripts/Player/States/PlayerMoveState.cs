using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : State<Player>
{
    public PlayerMoveState(Player sm) : base(sm) {
    }


    public override void Enter()
    {
        Owner.MyAnimator.Play("Walking");
    }

    public override void Tick()
    {
        Owner.EnterJumpStateIfJumpButtonPressed();
        Owner.EnterFallStateIfNoGroundAndVelocityYisNegative();

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
