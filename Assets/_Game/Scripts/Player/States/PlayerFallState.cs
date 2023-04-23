using UnityEngine;

public class PlayerFallState : State<Player>
{
    public PlayerFallState(Player sm) : base(sm) {
    }

    State<Player> _previousState;

    public override void Enter()
    {
        _previousState = Owner.MovementStateMachine.PreviousState;

        Owner.PlayAnimation("falling");
    }

    public override void Tick()
    {
        Owner.EnterIdleStateIfThereIsGroundAndVelocityYisNegative();
        if (_previousState == Owner.WallJumpState) return;
        if (Owner.CheckWall())
        {
            if (Owner.Rb.velocity.y < 0)
            {
                Owner.MovementStateMachine.ChangeState(Owner.WallSlideState);
            }
        }
    }
}
