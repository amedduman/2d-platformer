using UnityEngine;

public class PlayerJumpLaunchingState : State<Player>
{
    public PlayerJumpLaunchingState(Player owner) : base(owner) {
    }

    public override void Enter()
    {
        Owner.PlayAnimation("jump_continue");
    }

    public override void FixedTick()
    {
        if (Owner.CheckGround())
        {
            Owner.MovementStateMachine.ChangeState(Owner.IdleState);
            return;
        }
        
        if (Owner.Rb.velocity.y < 0)
        {
            Owner.MovementStateMachine.ChangeState(Owner.FallState);
            return;
        }

        // reset y velocity if jump button pressed to make variable jump height.
        if (Owner.JumpInput == false)
        {
            Owner.Rb.velocity = new Vector2(Owner.Rb.velocity.x, 0);
        }
        
        Owner.MoveHorizontally();
    }
}