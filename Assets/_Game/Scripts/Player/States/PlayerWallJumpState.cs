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
            Debug.Log("entered fall state form wall jump state");
            Owner.MovementStateMachine.ChangeState(Owner.FallState);
        }
        else
        {
            Owner.PlayAnimation("jump_start");
            Owner.Jump();
        }
    }

    public override void Tick()
    {
        Debug.Log("tick form wall jump");
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
