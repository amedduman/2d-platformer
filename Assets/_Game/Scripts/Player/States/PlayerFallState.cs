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
        if (Owner.Rb.velocity.y < -25)
            Owner.Rb.velocity = new Vector2(Owner.Rb.velocity.x, -25);// limit the y velocity when falling
        
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
        
        if (Owner.CheckWall())
        {
            if (Owner.MoveInput.magnitude != 0)
            {
                if (Owner.IsMoveInputParallelWithTransformRight())
                {
                    if (Owner.Rb.velocity.y < 0)
                    {
                        Owner.MovementStateMachine.ChangeState(Owner.WallSlideState);
                    }
                }
            }
        }
        // Owner.EnterWallSlideStateIfThereisWallAndVelocityYisNegative();
    }
}
