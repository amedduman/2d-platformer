using UnityEngine;

public class PlayerLedgeClimbState : State<Player>
{
    public PlayerLedgeClimbState(Player owner) : base(owner) {
    }

    public override void Enter()
    {
        Owner.Rb.velocity = Vector2.zero;
        Owner.Rb.bodyType = RigidbodyType2D.Kinematic;

        Owner.Rb.MovePosition(Owner.Debug_Target2.position);

        Owner.PlayAnimation("ledge-climbing");
    }

    public override void FixedTick()
    {
    }


    public override void Exit()
    {
        Owner.Rb.bodyType = RigidbodyType2D.Dynamic;
    }
}