using UnityEngine;

public class PlayerFallState : State<Player>
{
    public PlayerFallState(Player sm) : base(sm) {
    }

    public override void Enter()
    {
        Owner.PlayAnimation("falling");
    }

    public override void FixedTick()
    {
        if(Owner.MoveInput.x != 0 || Owner.MoveInput.y != 0) // because it will change the rotation of the player is facing
            Owner.MoveHorizontally();

        if(Owner.CheckGround())
        {
            Owner.MovementStateMachine.ChangeState(Owner.IdleState);
        }
        if(Owner.CheckLedge())
        {
            Owner.MovementStateMachine.ChangeState(Owner.LedgeHangingState);
        }

        Owner.EnterWallSlideStateIfThereisWallAndVelocityYisNegative();
    }
}
