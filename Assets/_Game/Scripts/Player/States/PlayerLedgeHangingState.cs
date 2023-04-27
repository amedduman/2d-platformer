using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLedgeHangingState : State<Player>
{
    public PlayerLedgeHangingState(Player player) : base(player){
    }


    public override void Enter()
    {
        Owner.Rb.velocity = Vector2.zero;
        Owner.Rb.bodyType = RigidbodyType2D.Kinematic;

        Owner.Rb.MovePosition(Owner.Debug_Target2.position);

        Owner.PlayAnimation("ledge-hanging");
    }

    public override void Tick()
    {
        if (Owner.MoveInput.x != 0)
        {
            Owner.MovementStateMachine.ChangeState(Owner.LedgeClimbState);
        }
    }

    public override void Exit()
    {
        Owner.Rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
