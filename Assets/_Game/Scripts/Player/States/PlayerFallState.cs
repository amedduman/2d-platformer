using UnityEngine;

public class PlayerFallState : State<Player>
{
    public PlayerFallState(Player sm) : base(sm) {
    }

    public override void Enter()
    {
        Owner.PlayAnimation("falling");
//        Owner.MyAnimator.CrossFade("falling",.4f,0);
    }

    public override void FixedTick()
    {
        if(Owner.MoveInput.x != 0 || Owner.MoveInput.y != 0)
            Owner.MoveHorizontally();

        if(Owner.CheckGround())
        {
            Owner.MovementStateMachine.ChangeState(Owner.IdleState);
        }
        if(Owner.CheckLedge())
        {
            Owner.MovementStateMachine.ChangeState(Owner.LedgeClimbState);
        }

        Owner.EnterWallSlideStateIfThereisWallAndVelocityYisNegative();
    }
}
