using UnityEngine;
using DG.Tweening;

public class PlayerLedgeClimbState : State<Player>
{
    public PlayerLedgeClimbState(Player owner) : base(owner) {
    }

    bool _canState;

    public override void Enter()
    {
        Owner.Rb.velocity = Vector2.zero;
        Owner.Rb.bodyType = RigidbodyType2D.Kinematic;

        Owner.Rb.MovePosition(Owner.Debug_Target2.position);

        Owner.PlayAnimation("ledge-climbing");

        DOVirtual.DelayedCall(.8f, () =>
        {
            _canState = true;
        });
    }

    public override void FixedTick()
    {
        if(_canState)
        {
            Owner.Rb.MovePosition(Owner.Debug_Target.position);
            //Owner.MovementStateMachine.ChangeState(Owner.IdleState);
        }
    }


    public override void Exit()
    {
        Owner.Rb.bodyType = RigidbodyType2D.Dynamic;
    }
}