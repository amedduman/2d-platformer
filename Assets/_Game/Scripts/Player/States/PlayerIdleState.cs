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
        if (Mathf.Approximately(0, Owner.MoveInput.x) == false)
        {
            Owner.MovementStateMachine.ChangeState(Owner.MoveState);
            return;
        }
        
        if (Owner.CheckGround() == false)
        {
            if (Owner.Rb.velocity.y < 0)
            {
                Owner.MovementStateMachine.ChangeState(Owner.FallState);
                return;
            }
        }
        
        // if(Owner._previousJumpInput) return; // we don't want players jump again in case they are still holding the jump button. 
        if (Owner.IsJumpButtonPressed())
        {
            Owner.MovementStateMachine.ChangeState(Owner.JumpStartState);
            return;
        }
    }
}
