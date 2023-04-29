using UnityEngine;

public class PlayerDoubleJumpState : State<Player>
{
    public PlayerDoubleJumpState(Player owner) : base(owner) {
    }

    public override void Enter()
    {
        Owner.CanDoubleJump = false;
        Owner.Rb.velocity = new Vector2(Owner.Rb.velocity.x, 25);
    }

    public override void FixedTick()
    {
        if (Owner.Rb.velocity.y < 0 )
        {
            Owner.MovementStateMachine.ChangeState(Owner.FallState);
        }
    }
}