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
        // limit the y velocity when falling
        if (Owner.Rb.velocity.y < -25)
            Owner.Rb.velocity = new Vector2(Owner.Rb.velocity.x, -25);
        
        // if(Owner.MoveInput.x != 0 || Owner.MoveInput.y != 0) // because it will change the rotation of the player is facing
        //     Owner.MoveHorizontally();
        
        Owner.MoveHorizontally();
        
        if(Owner.CheckGround())
        {
            Owner.MovementStateMachine.ChangeState(Owner.IdleState);
            return;
        }
        
        if(Owner.CheckLedge())
        {
            Owner.MovementStateMachine.ChangeState(Owner.LedgeHangingState);
            return;
        }
        
        if (Owner.CheckWall())
        {
            if (Owner.IsMoveInputParallelWithTransformRight())
            {
                Owner.MovementStateMachine.ChangeState(Owner.WallSlideState);
                // if (Owner.Rb.velocity.y < 0)
                // {
                //     
                // }
            }
            // if (Owner.MoveInput.magnitude != 0)
            // {
            //     
            // }
        }
    }
}
