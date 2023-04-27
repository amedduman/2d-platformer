using DG.Tweening;
using UnityEngine;

public class PlayerLedgeClimbState : State<Player>
{
    public PlayerLedgeClimbState(Player owner) : base(owner) {
    }

    public override void Enter()
    {
        Owner.Rb.velocity = Vector2.zero;
        Owner.Rb.bodyType = RigidbodyType2D.Kinematic;

        // Owner.Rb.MovePosition(Owner.Debug_Target.position);
        Owner.PlayAnimation("ledge-climbing");
        Owner.Rb.DOMoveY(Owner.Rb.position.y + 1.6f, .4f).OnComplete(() =>
        {
            Owner.PlayAnimation("Walking");
            if (Owner.transform.right.x < 0)
            {
                Owner.Rb.DOMoveX(Owner.Rb.position.x - 1, .2f).OnComplete(() =>
                {
                    Owner.MovementStateMachine.ChangeState(Owner.IdleState);
                });
            }
            else
            {
                Owner.Rb.DOMoveX(Owner.Rb.position.x + 1, .2f).OnComplete(() =>
                {
                    Owner.MovementStateMachine.ChangeState(Owner.IdleState);
                });
            }
           
        });
    }

    public override void FixedTick()
    {
    }


    public override void Exit()
    {
        Owner.Rb.bodyType = RigidbodyType2D.Dynamic;
    }
}